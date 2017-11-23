using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTextSharpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            string srcFile = args[0];
            string dstFile1 = srcFile.Replace(".pdf", ".out1.pdf");
            string dstFile2 = srcFile.Replace(".pdf", ".out2.pdf");
            PdfOperation.ShowPdfInfo(srcFile);
            PdfOperation.SetPassword(srcFile, dstFile1, "owner", "user");
            PdfOperation.SetPDFInfoAndEncrypt(srcFile, dstFile2, "owner", "user");
        }
    }
}
