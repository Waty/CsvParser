using System;
using System.Globalization;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace CsvParser
{
    internal sealed class IngDataMap : CsvClassMap<IngData>
    {
        public static readonly string[] DateTimeFormats =
        {
            "g", "yyyyMMdd"
        };

        public IngDataMap()
        {
            Map(m => m.Datum).Name("Datum").ConvertUsing(DateTimeParser);
            Map(m => m.Naam).Name("Naam / Omschrijving");
            Map(m => m.Rekening).Name("Rekening");
            Map(m => m.Tegenrekening).Name("Tegenrekening");
            Map(m => m.Code).Name("Code");
            Map(m => m.AfBij).Name("Af Bij");
            Map(m => m.Bedrag).Name("Bedrag (EUR)");
            Map(m => m.MutatieSoort).Name("MutatieSoort");
            Map(m => m.MededelingenString).Name("Mededelingen");
            Map(m => m.Mededelingen).Name("Mededelingen").ConvertUsing(MededelingenParser);
        }


        private static DateTime DateTimeParser(ICsvReaderRow x)
        {
            string s = x.GetField("Datum");

            DateTime date;
            if (DateTime.TryParse(s, out date) ||
                DateTime.TryParseExact(s, DateTimeFormats, CultureInfo.CurrentCulture, DateTimeStyles.None, out date))
            {
                return date;
            }

            Console.WriteLine(s + " is a invalid string format");

            throw new InvalidOperationException("Couldn't convert '" + s + "' to a DateTime");
        }

        private static IngData.MededelingenData MededelingenParser(ICsvReaderRow row)
        {
            string mededelingen = row.GetField("Mededelingen");

            var options = new[] {"BIC", "Crediteur", "IBAN", "Kenmerk", "Mandaat", "Naam", "Omschrijving"};

            var results = options.Select(
                o => new {Option = o, Index = mededelingen.IndexOf(o, StringComparison.CurrentCultureIgnoreCase)})
                .Where(obj => obj.Index > -1)
                .OrderByDescending(obj => obj.Index).ToList();

            var data = new IngData.MededelingenData();
            foreach (var result in results)
            {
                string subStr = mededelingen.Substring(result.Index + result.Option.Length);

                if (subStr.StartsWith(":"))
                {
                    subStr = subStr.Remove(0, 1);
                }
                mededelingen = mededelingen.Remove(result.Index).Trim();

                data.SetOption(result.Option, subStr);
            }

            return data;
        }
    }
}