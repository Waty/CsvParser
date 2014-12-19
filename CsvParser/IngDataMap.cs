using System;
using System.Globalization;
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
            Map(m => m.Datum).ConvertUsing(x =>
            {
                string s = x.CurrentRecord[0];

                DateTime date;
                if (DateTime.TryParse(s, out date) ||
                    DateTime.TryParseExact(s, DateTimeFormats, CultureInfo.CurrentCulture, DateTimeStyles.None, out date))
                {
                    return date;
                }

                Console.WriteLine(s +  " is a invalid string format");

                throw new InvalidOperationException("Couldn't convert '" + s + "' to a DateTime");
            });
            Map(m => m.Naam).Index(1);
            Map(m => m.Rekening).Index(2);
            Map(m => m.Tegenrekening).Index(3);
            Map(m => m.Code).Index(4);
            Map(m => m.AfBij).Index(5);
            Map(m => m.Bedrag).Index(6);
            Map(m => m.MutatieSoort).Index(7);
            Map(m => m.MededelingenString).Index(8);
        }
    }
}