using System;
using System.Linq;

namespace CsvParser
{
    internal class IngData
    {
        private string mededelingenString;
        public string AfBij { get; set; }
        public double Bedrag { get; set; } //(EUR)
        public string Code { get; set; }
        public DateTime Datum { get; set; }

        public string MededelingenString
        {
            get { return mededelingenString; }
            set
            {
                mededelingenString = value;
                Mededelingen = ParseMededelingen(value);
            }
        }

        public string MutatieSoort { get; set; }
        public string Naam { get; set; }
        public string Rekening { get; set; }
        public string Tegenrekening { get; set; }

        public MededelingenData Mededelingen { get; set; }

        private static MededelingenData ParseMededelingen(string mededelingen)
        {
            var options = new[] { "BIC", "Crediteur", "IBAN", "Kenmerk", "Mandaat", "Naam", "Omschrijving" };

            var results = options.Select(
                o => new { Option = o, Index = mededelingen.IndexOf(o, StringComparison.CurrentCultureIgnoreCase) })
                .Where(obj => obj.Index > -1)
                .OrderByDescending(obj => obj.Index).ToList();

            var data = new MededelingenData();
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

        public class MededelingenData
        {
            public string BIC;
            public string Crediteur;
            public string IBAN;
            public string Kenmerk;
            public string Mandaat;
            public string Naam;
            public string Omschrijving;


            public void SetOption(string option, string value)
            {
                switch (option)
                {
                    case "BIC": BIC = value; break;
                    case "Crediteur": Crediteur = value; break;
                    case "IBAN": IBAN = value; break;
                    case "Kenmerk": Kenmerk = value; break;
                    case "Mandaat": Mandaat = value; break;
                    case "Naam": Naam = value; break;
                    case "Omschrijving": Omschrijving = value; break;

                    default: throw new ArgumentException("the option " + option + " doesn't exist", "option");
                }
            }
        }
    }
}