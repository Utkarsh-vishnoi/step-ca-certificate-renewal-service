using System;
using Topshelf;
using log4net.Config;

namespace Certificate_Renewal_Service
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            HostFactory.Run(hostConfigurator =>
            {
                hostConfigurator.SetServiceName("Certificate Renewal Service");
                hostConfigurator.SetDisplayName("Certificate Renewal Service");
                hostConfigurator.SetDescription("Renews client certificate if expired and loads them into the system store");
                hostConfigurator.RunAsLocalSystem();
                hostConfigurator.StartAutomatically();
                hostConfigurator.UseLog4Net();

                hostConfigurator.Service<CertificateRenewalService>(serviceConfigurator => {
                    serviceConfigurator.ConstructUsing(() => new CertificateRenewalService());
                    serviceConfigurator.WhenStarted(service => service.OnStart());
                    serviceConfigurator.WhenStopped(service => service.OnStop());
                });
            });
        }
    }
}
