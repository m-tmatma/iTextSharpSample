using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
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

        public static void SetPDFInfoAndEncrypt(string srcFile, string dstFile, string ownerPassword, string userPassword)
        {
            byte[] byteUSER = Encoding.ASCII.GetBytes(userPassword);   // user password
            byte[] byteOWNER = Encoding.ASCII.GetBytes(ownerPassword); // owner password

            using (var reader = new PdfReader(srcFile, byteOWNER))
            {
                using (var streamPDF = new FileStream(dstFile, FileMode.Create))
                {
                    using (var stamper = new PdfStamper(reader, streamPDF))
                    {
                        Dictionary<String, String> info = reader.Info;
                        info["Title"] = "title";
                        info["Subject"] = "subject";
                        info["Keywords"] = "keyword";
                        info["Creator"] = "creator";
                        info["Author"] = "author";
                        stamper.SetEncryption(byteUSER, byteOWNER, PdfWriter.AllowPrinting, PdfWriter.ENCRYPTION_AES_128);
                        stamper.MoreInfo = info;
                    }
                }
            }
        }
    }
}
