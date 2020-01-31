using System;
using System.Collections.Generic;
using System.Text;
using SharpML.Recurrent.Activations;
using SharpML.Recurrent.DataStructs;
using SharpML.Recurrent.Loss;
using SharpML.Recurrent.Networks;

namespace SharpML.Reccurent.Examples.Data
{
    public class XorDataSetGenerator : DataSet
    {
        public XorDataSetGenerator()
        {
            InputDimension = 2;
            OutputDimension = 1;
            LossTraining = new LossSumOfSquares();
            LossReporting = new LossSumOfSquares();
            Training = GetTrainingData();
            Validation = GetTrainingData();
            Testing = GetTrainingData();
        }

        private static List<DataSequence> GetTrainingData()
        {

            var result = new List<DataSequence>
            {
                new DataSequence(new List<DataStep> {new DataStep(new double[] {1, 0}, new double[] {1})}),
                new DataSequence(new List<DataStep> {new DataStep(new double[] {0, 1}, new double[] {1})}),
                new DataSequence(new List<DataStep> {new DataStep(new double[] {0, 0}, new double[] {0})}),
                new DataSequence(new List<DataStep> {new DataStep(new double[] {1, 1}, new double[] {0})})
            };

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
