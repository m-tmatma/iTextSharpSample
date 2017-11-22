using System;
using iTextSharp.text.pdf;

namespace iTextSharpSample
{
    public class PdfOperation
    {
        public static void ShowPdfInfo(string srcFile)
        {
            using (var reader = new PdfReader(srcFile))
            {
                foreach (string key in reader.Info.Keys)
                {
                    Console.WriteLine(key + "\t" + reader.Info[key]);
                }
            }
        }
    }
}
