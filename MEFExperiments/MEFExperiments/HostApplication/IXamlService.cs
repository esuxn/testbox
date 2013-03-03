using HostWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Windows.Interop;

namespace HostApplication
{

    public interface IWindowCallback
    {
        [OperationContract]
        void SetChildWindow(int child, int parent);
    }

   [ServiceContract(SessionMode=SessionMode.Required, CallbackContract=typeof(IWindowCallback))]
    public interface IXamlService
    {
        [OperationContract]
        void ShowChild(int parent);
    }

   public class XamlService : IXamlService
   {
       public void ShowChild(int parent)
       {

           IWindowCallback callbacks = OperationContext.Current.GetCallbackChannel<IWindowCallback>();
           var wthread = new Thread(() => NewWindow(parent,callbacks));
           wthread.IsBackground = true;
           wthread.SetApartmentState(ApartmentState.STA);
           wthread.Start();


       }
       public void NewWindow(int parent, IWindowCallback callbacks)
       {
           var window = new MainWindow();
           WindowInteropHelper helper = new WindowInteropHelper(window);
           helper.Owner = new IntPtr(parent);
           window.Show();
           callbacks.SetChildWindow(helper.Handle.ToInt32(), parent);
           System.Windows.Threading.Dispatcher.Run();
       }
   }
}