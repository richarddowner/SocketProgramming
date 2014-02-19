using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace EmployeeTCPServer
{
    [RunInstaller(true)]
    public class EmployeeTCPService : Installer
    {
        private ServiceProcessInstaller processInstaller;
        private ServiceInstaller serviceInstaller;

        public EmployeeTCPService()
        {
            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();
            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.ServiceName = "EmployeeTCPService";
            Installers.Add(serviceInstaller);
            Installers.Add(processInstaller);
        }
    }
}