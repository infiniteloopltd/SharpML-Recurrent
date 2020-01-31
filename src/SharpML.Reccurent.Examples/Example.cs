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
            DataSet data = new ReverserDataSetGenerator(); //new XorDataSetGenerator();

            const int inputDimension = 2;
            const int hiddenDimension = 5;
            const int outputDimension = 2;
            const int hiddenLayers = 1;
            const double learningRate = 0.001;
            const double initParamsStdDev = 0.08;
            INetwork nn;
            const int reportEveryNthEpoch = 10;
            const int trainingEpochs = 10000;
            var strModelFile = AppDomain.CurrentDomain.BaseDirectory + @"model.bin";
            if (File.Exists(strModelFile))
            {
                nn = Binary.ReadFromBinary<NeuralNetwork>(strModelFile);
            }
            else
            {
                nn = NetworkBuilder.MakeFeedForward(inputDimension,
                    hiddenDimension,
                    hiddenLayers,
                    outputDimension,
                    data.GetModelOutputUnitToUse(),
                    data.GetModelOutputUnitToUse(),
                    initParamsStdDev, rng);
                Trainer.train<NeuralNetwork>(trainingEpochs, learningRate, nn, data, reportEveryNthEpoch, true, true, strModelFile, rng);
                Console.WriteLine("Training Completed.");
            }
            Console.WriteLine("Test: 0.4 + 0.3");

            var input = new Matrix(new[] {0.4, 0.3});
            var g = new Graph(false);
            var output = nn.Activate(input, g);

            Console.WriteLine("Output:" + output.W[0] + " - " + output.W[1]);

            Console.WriteLine("Test: 0.1 + 0.2");
            var input1 = new Matrix(new[] { 0.1, 0.2 });
            var g1 = new Graph(false);
            var output1 = nn.Activate(input1, g1);

            Console.WriteLine("Output:" + output1.W[0] + " - " + output1.W[1]);

            Console.WriteLine("done.");
        }
    }
}
