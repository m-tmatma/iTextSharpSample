using System;
using System.Text;
using System.IO;
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

        public static void SetPassword(string srcFile, string dstFile, string ownerPassword, string userPassword)
        {
            byte[] byteUSER = Encoding.ASCII.GetBytes(userPassword);   // user password
            byte[] byteOWNER = Encoding.ASCII.GetBytes(ownerPassword); // owner password

            using (var reader = new PdfReader(srcFile))
            {
                using (var streamPDF = new FileStream(dstFile, FileMode.Create))
                {
                    PdfEncryptor.Encrypt(reader,
                     streamPDF,
                     byteUSER,
                     byteOWNER,
                     PdfWriter.ALLOW_COPY | PdfWriter.ALLOW_PRINTING,
                     PdfWriter.STRENGTH128BITS);
                }
            }
        }
    }
}
