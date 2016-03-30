using CertiPay.Common;
using CertiPay.PDF;
using Nancy;
using Nancy.Responses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace CertiPay.Services.PDF.Modules
{
    public class PdfModule : NancyModule
    {
        private static String AppName { get { return ConfigurationManager.AppSettings["ApplicationName"]; } }

        public PdfModule(IPDFService pdfSvc)
        {
            Get["/"] = _ =>
            {
                return Response.AsJson(new
                {
                    Application = AppName,
                    Version = CertiPay.Common.Utilities.Version<PdfModule>(),
                    Environment = EnvUtil.Current.DisplayName(),
                    Server = Environment.MachineName
                });
            };

            Get["/Pdf/GenerateDocument"] = p =>
            {
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