using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvParser.Properties;

namespace CsvParser
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            int argCount = args.GetLength(0);
            CsvReader reader = argCount <= 0
                ? new CsvReader(new StringReader(Resources.TestData))
                : new CsvReader(new StreamReader(args[0]));

            StreamWriter writer = argCount <= 0
                ? new StreamWriter("output.mt940")
                : new StreamWriter(Path.ChangeExtension(args[0], "mt940"));

            using (reader)
            using (writer)
            {
                reader.Configuration.RegisterClassMap<IngDataMap>();
                IEnumerable<IngData> results = reader.GetRecords<IngData>();

                foreach (IngData csvData in results)
                {
                    
                }
            }
            Console.ReadKey();
        }
    }
}