using System;
using System.Timers;
using System.IO;
using System.Text.Json;
using log4net;
using System.Collections.Generic;
using System.Text;

namespace Certificate_Renewal_Service
{
    class CertificateRenewalService
    {
        private Timer serviceTimer = null;

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Configuration configuration = new Configuration
        {
            check_interval = 5000 // Certificate check interval in nicorseconds. Default 5 Minutes
        };

        public void OnStart()
        {
            log.Info(String.Format("Certificate Renewal Service starts on {0} {1}", System.DateTime.Now.ToString("dd-MMM-yyyy"), DateTime.Now.ToString("hh:mm:ss tt")));
            this.load_configuration();
            this.serviceTimer = new System.Timers.Timer(configuration.check_interval);
            this.serviceTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);
            this.serviceTimer.Start();
        }

        public void OnStop()
        {
            this.serviceTimer.Stop();
            this.serviceTimer.Dispose();
            this.serviceTimer = null;
            log.Info(String.Format("Certificate Renewal Service stops on {0} {1}", System.DateTime.Now.ToString("dd-MMM-yyyy"), DateTime.Now.ToString("hh:mm:ss tt")));
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (check_ca_server())
                return;
        }

        private void load_configuration()
        {
            string configurationFile = AppDomain.CurrentDomain.BaseDirectory + "\\config.json";
            if (File.Exists(configurationFile))
            {
                string jsonConfig = File.ReadAllText(configurationFile);
                configuration = JsonSerializer.Deserialize<Configuration>(jsonConfig);
                log.Info("Configuration File Loaded Successfully.");
            }
            else
                log.Error("Configuration File config.json missing.");

        }

        private Boolean check_ca_server()
        {
            return true;
        }
    }
}
