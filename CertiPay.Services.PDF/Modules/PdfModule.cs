using CertiPay.PDF;
using CertiPay.Services.PDF.Extensions;
using Nancy;
using Nancy.Responses;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using WebSupergoo.ABCpdf10;

namespace CertiPay.Services.PDF.Modules
{
    public class PdfModule : NancyModule
    {
        public PdfModule(IPDFService pdfSvc)
        {
            Get["/Pdf/GenerateDocument"] = p =>
            {
                if (!XSettings.InstallLicense(ConfigurationManager.AppSettings["ABCPDF-License"])) return Response.AsError(HttpStatusCode.NotFound, "ABCPDF License is Invalid");

                var url = this.Request.Query["url"];

                var settings = new PDFService.Settings()
                {
                    UseLandscapeOrientation = true,
                    Uris = new List<string>()
                    {
                        url
                    }
                };

                var stream = new MemoryStream(pdfSvc.CreatePdf(settings));

                //Future change.  Add ability for FileName to be passed in from Caller.
                var response = new StreamResponse(() => stream, MimeTypes.GetMimeType("Generated-Document.pdf"));
                
                return response;
            };
        }
    }
}
