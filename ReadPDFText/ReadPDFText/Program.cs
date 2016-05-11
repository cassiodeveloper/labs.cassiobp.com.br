
using System;
namespace ReadPDFText
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PDFParser pdfParser = new PDFParser();

            string fileNameOut = @"C:\Users\mtzcpd1053\Downloads\Code\80586.txt";

            if (pdfParser.ExtractText(@"C:\Users\mtzcpd1053\Downloads\Code\80586.pdf", fileNameOut))
            {
                Console.WriteLine("Texto extraído");
                Console.WriteLine(fileNameOut);
            }
            else
                Console.WriteLine("Erro");
            
            Console.Read();
        }
    }
}