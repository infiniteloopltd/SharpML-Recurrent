using System;
using System.IO;
using SharpML.Reccurent.Examples.Data;
using SharpML.Recurrent.DataStructs;
using SharpML.Recurrent.Models;
using SharpML.Recurrent.Networks;
using SharpML.Recurrent.Trainer;
using SharpML.Recurrent.Util;

namespace SharpML.Reccurent.Examples
{
    public class Example
    {
        public static void Run()
        {
          

            var rng = new Random();
            DataSet data = new CSVDataSetEncoder("6-back-10k.csv", false);
            int hiddenDimension = data.InputDimension + data.OutputDimension;
            const int hiddenLayers = 1;
            const double learningRate = 0.001;
            const double initParamsStdDev = 0.08;
            INetwork nn;
            const int reportEveryNthEpoch = 10;
            const int trainingEpochs = 500;
            var strModelFile = AppDomain.CurrentDomain.BaseDirectory + @"6-back-10k.bin";
            if (File.Exists(strModelFile))
            {
                nn = Binary.ReadFromBinary<NeuralNetwork>(strModelFile);
            }
            else
            {
                nn = NetworkBuilder.MakeFeedForward(data.InputDimension,
                    hiddenDimension,
                    hiddenLayers,
                    data.OutputDimension,
                    data.GetModelOutputUnitToUse(),
                    data.GetModelOutputUnitToUse(),
                    initParamsStdDev, rng);
                Trainer.train<NeuralNetwork>(trainingEpochs, learningRate, nn, data, reportEveryNthEpoch, true, true, strModelFile, rng);
                Console.WriteLine("Training Completed.");
            }
            Console.WriteLine("Test: 4400VW"); // In training set

            var input = new Matrix(CSVDataSetEncoder.StringToDoubleArray("4400VW"));
            var g = new Graph(false);
            var output = nn.Activate(input, g);

            Console.WriteLine("Output:" + CSVDataSetEncoder.DoubleArrayToString(output.W));

            Console.WriteLine("Test: A400V4"); // not in training set
            var input1 = new Matrix(CSVDataSetEncoder.StringToDoubleArray("A400V4"));
            var g1 = new Graph(false);
            var output1 = nn.Activate(input1, g1);

            Console.WriteLine("Output:" + CSVDataSetEncoder.DoubleArrayToString(output1.W));

            Console.WriteLine("done.");
        }
    }
}
