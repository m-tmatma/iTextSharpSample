using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using iTextSharp.text.pdf;

namespace iTextSharpSample
{
    public class PdfOperation
    {
        /// <summary>
        /// Dump PDF Information
        /// </summary>
        /// <param name="srcFile">source PDF file path</param>
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

        /// <summary>
        /// Set Password to a PDF file
        /// </summary>
        /// <param name="srcFile">source PDF file path</param>
        /// <param name="dstFile">destination PDF file path</param>
        /// <param name="ownerPassword">owner password</param>
        /// <param name="userPassword">user password</param>
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

        /// <summary>
        /// set PDF information and set password to a PDF file
        /// </summary>
        /// <param name="srcFile">source PDF file path</param>
        /// <param name="dstFile">destination PDF file path</param>
        /// <param name="ownerPassword">owner password</param>
        /// <param name="userPassword">user password</param>
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
