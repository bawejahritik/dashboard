using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using OpenNLP.Tools.NameFind;
namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            string file = @"C:\\Users\\hritik\\OneDrive\\Desktop\\Resume.pdf";
            using (PdfReader pdfReader = new PdfReader(file))
            {
                for(int i = 1; i <= pdfReader.NumberOfPages; i++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string text = PdfTextExtractor.GetTextFromPage(pdfReader, i, strategy);
                    text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));
                    sb.Append(text);
          
                }
            }

            var nameFinder = new EnglishNameFinder("models/en-ner-person.bin");
            string[] models = new string[] { "date", "location", "money", "organization", "percentage", "person", "time" };

            var names = nameFinder.GetNames(models, sb.ToString());

            List<string> namesList = new List<string>();

            foreach (var name in names)
            {
                namesList.Add(name.ToString());
            }


            Console.WriteLine(sb.ToString() + '\n' + "names: ");
            for(int i = 0; i < namesList.Count; i++)
            {
                Console.WriteLine(namesList[i] + '\n');    
            }

            Console.ReadKey();

        }
    }
}
