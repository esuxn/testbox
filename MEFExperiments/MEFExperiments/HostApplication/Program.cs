using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HostApplication
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(
    typeof(XamlService),
    new Uri[] { new Uri("net.pipe://localhost") }))
            {

                host.AddServiceEndpoint(typeof(IXamlService),
                  new NetNamedPipeBinding(), "XamlShow");

                host.Open();

                Console.WriteLine("Service is available. " +
                  "Press <ENTER> to exit.");
                Console.ReadLine();

                host.Close();
            }
        }
    }
}
