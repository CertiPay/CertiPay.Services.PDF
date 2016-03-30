using CertiPay.PDF;
using Nancy;
using Nancy.TinyIoc;
using System;
using System.Configuration;

namespace CertiPay.Services.PDF
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private static String ABCPdfLicense
        {
            get
            {
                // Check the environment variables for the license key

                String key = "ABCPDF-License";

                if (!String.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.User)))
                {
                    return Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.User);
                }

                return ConfigurationManager.AppSettings[key];
            }
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register<IPDFService>(new PDFService(ABCPdfLicense));
        }
    }
}