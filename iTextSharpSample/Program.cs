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
            string dstFile = srcFile.Replace(".pdf", ".out.pdf");
            PdfOperation.ShowPdfInfo(srcFile);
            PdfOperation.SetPassword(srcFile, dstFile, "owner", "user");
        }
    }
}
