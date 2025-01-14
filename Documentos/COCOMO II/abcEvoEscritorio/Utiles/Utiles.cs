using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ABCEvoEscritorio.Utiles
{
    public class Utiles
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Utiles() {
            // Load configuration
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }
        public void LogMensaje(string mensaje)
        {
            log.Info(mensaje);
        }

        public void LogError(Exception mensaje)
        {
            log.Error(mensaje);
        }

        public void LogWarning(object mensaje)
        {
            log.Warn(mensaje);
        }

        public void LogFatal(string mensaje)
        {
            log.Fatal(mensaje);
        }

        public void LogDebug(object mensaje)
        {
            log.Debug(mensaje);
        }

    }
}
