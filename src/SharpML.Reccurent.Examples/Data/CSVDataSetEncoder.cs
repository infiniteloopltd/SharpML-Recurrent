using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using SharpML.Recurrent.Activations;
using SharpML.Recurrent.DataStructs;
using SharpML.Recurrent.Loss;
using SharpML.Recurrent.Networks;

namespace SharpML.Reccurent.Examples.Data
{
    public class CSVDataSetEncoder : DataSet
    {
        public static List<double[]> CSVInputs = new List<double[]>();
        public static List<double[]> CSVOutputs = new List<double[]>();

        public CSVDataSetEncoder( string CSVFile, bool hasHeader)
        {   
            var csvLines = File.ReadAllLines(CSVFile).Skip(hasHeader ? 1 : 0).ToList();
            foreach (var line in csvLines)
            {
                var idxComma = line.IndexOf(",", StringComparison.Ordinal);
                var input = line.Substring(0, idxComma);
                var output = line.Substring( idxComma+1);
                CSVInputs.Add(StringToDoubleArray(input));
                CSVOutputs.Add(StringToDoubleArray(output));
            }
            InputDimension = CSVInputs.Max(inp => inp.Length);
            OutputDimension = CSVOutputs.Max(outp => outp.Length);
            LossTraining = new LossSumOfSquares();
            LossReporting = new LossSumOfSquares();
            Training = GetTrainingData();
            Validation = GetTrainingData();
            Testing = GetTrainingData();
        }

        public static double[] StringToDoubleArray(string text)
        {
            var asciiBytes = Encoding.ASCII.GetBytes(text.ToUpper());
            if (asciiBytes.Any(b => b<32 || b>90)) throw new Exception("Text must be in range 32 to 90:" + text);
            // 32 - 90 standard range.
            var dAscii = asciiBytes.Select(a => 1.0/58.0 * (a - 32)).ToArray();
            return dAscii;
        }

        public static string DoubleArrayToString(double[] ascii)
        {
            var dAscii = ascii.Select(a => a * 58 + 32);
            var bAscii = dAscii.Select(d => Convert.ToByte(d)).ToArray();
            return Encoding.ASCII.GetString(bAscii);
        }

        private static List<DataSequence> GetTrainingData()
        {

            var result = new List<DataSequence>();

            for(var a=0; a< CSVInputs.Count; a++)
            {
                var input = CSVInputs[a];
                var output = CSVOutputs[a];
                result.Add(new DataSequence(new List<DataStep> { new DataStep(input, output) }));
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
