using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SharpML.Recurrent.Util
{
    public static class Binary
    {
        public static T ReadFromBinary<T>(string filePath)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException(filePath);
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                var formatter = new BinaryFormatter
                {
                    AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
                };
                if (fs.Length == 0)
                    return default(T);

                return (T)formatter.Deserialize(fs);
            }

        }

        public static void WriteToBinary<T>(object dataToWrite, string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Create))
            {
                var formatter = new BinaryFormatter
                {
                    AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
                };
                var filesToWrite = (T)dataToWrite;
                formatter.Serialize(stream, filesToWrite);
            }
        }
    }
}
