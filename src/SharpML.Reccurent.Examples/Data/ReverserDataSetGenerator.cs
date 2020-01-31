using System;
using System.Collections.Generic;
using System.Text;
using SharpML.Recurrent.Activations;
using SharpML.Recurrent.DataStructs;
using SharpML.Recurrent.Loss;
using SharpML.Recurrent.Networks;

namespace SharpML.Reccurent.Examples.Data
{
    public class ReverserDataSetGenerator : DataSet
    {
        public ReverserDataSetGenerator()
        {
            InputDimension = 2;
            OutputDimension = 2;
            LossTraining = new LossSumOfSquares();
            LossReporting = new LossSumOfSquares();
            Training = GetTrainingData();
            Validation = GetTrainingData();
            Testing = GetTrainingData();
        }

        private static List<DataSequence> GetTrainingData()
        {

            var result = new List<DataSequence>();


            for (var a = 0.0; a < 1; a += 0.1)
            {
                for (var b = 0.0; b < 1; b += 0.1)
                {
                    var sum = a + b;
                    result.Add(new DataSequence(new List<DataStep>() { new DataStep(new[] { a, b }, new[] { b, a }) }));
                }
            }

            return result;
        }


        public override void DisplayReport(INetwork network, Random rng)
        {
            // TODO Auto-generated method stub

        }

        public override INonlinearity GetModelOutputUnitToUse()
        {
            return new SigmoidUnit();
        }

    }
}
