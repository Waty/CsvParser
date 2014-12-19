using System;
using System.Linq;

namespace CsvParser
{
    internal class IngData
    {
        public string AfBij { get; set; }
        public double Bedrag { get; set; } //(EUR)
        public string Code { get; set; }
        public DateTime Datum { get; set; }

        public string MededelingenString { get; set; }

        public string MutatieSoort { get; set; }
        public string Naam { get; set; }
        public string Rekening { get; set; }
        public string Tegenrekening { get; set; }

        public MededelingenData Mededelingen { get; set; }


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