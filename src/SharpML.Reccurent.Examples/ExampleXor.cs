using System;
using SharpML.Reccurent.Examples.Data;
using SharpML.Recurrent.DataStructs;
using SharpML.Recurrent.Models;
using SharpML.Recurrent.Networks;
using SharpML.Recurrent.Trainer;
using SharpML.Recurrent.Util;

namespace SharpML.Reccurent.Examples
{
    public class ExampleXor
    {
        public static void Run()
        {
            Random rng = new Random();
            DataSet data = new AdditionDataSetGenerator(); //new XorDataSetGenerator();

            int inputDimension = 2;
            int hiddenDimension = 3;
            int outputDimension = 1;
            int hiddenLayers = 1;
            double learningRate = 0.001;
            double initParamsStdDev = 0.08;

            INetwork nn = NetworkBuilder.MakeFeedForward(inputDimension,
                hiddenDimension,
                hiddenLayers,
                outputDimension,
                data.GetModelOutputUnitToUse(),
                data.GetModelOutputUnitToUse(),
                initParamsStdDev, rng);


            int reportEveryNthEpoch = 10;
            int trainingEpochs = 100000;

            Trainer.train<NeuralNetwork>(trainingEpochs, learningRate, nn, data, reportEveryNthEpoch, rng);

            Console.WriteLine("Training Completed.");
            Console.WriteLine("Test: 0.4 + 0.4");

            Matrix input = new Matrix(new double[] {0.4, 0.4});
            Graph g = new Graph(false);
            Matrix output = nn.Activate(input, g);

            Console.WriteLine("Output:" + output.W[0]);

            Console.WriteLine("Test: 0.1 + 0.2");
            Matrix input1 = new Matrix(new double[] { 0.1, 0.2 });
            Graph g1 = new Graph(false);
            Matrix output1 = nn.Activate(input1, g1);

            Console.WriteLine("Output:" + output1.W[0]);

            Console.WriteLine("done.");
        }
    }
}
