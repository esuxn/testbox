using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using HostApplication;
using System.ServiceModel;
using WindowManager;

namespace WinFormApp
{
    public partial class HostForm : Form
    {
        public HostForm()
        {
            InitializeComponent();
        }

        private void HostForm_Load(object sender, EventArgs e)
        {

        }

        private void NewWpfWindow_Click(object sender, EventArgs e)
        {

            var page = new CustomTabPage();
            tabControl1.Controls.Add(page);
            tabControl1.SelectedTab = page;
            IWindowCallback myCallbacks = new WindowCallback(page);

            DuplexChannelFactory<IXamlService> pipeFactory =
               new DuplexChannelFactory<IXamlService>(
                  myCallbacks,
                  new NetNamedPipeBinding(),
                  new EndpointAddress(
                     "net.pipe://localhost/XamlShow"));

            IXamlService pipeProxy = pipeFactory.CreateChannel();

            pipeProxy.ShowChild(page.Handle.ToInt32());
        }

    }
    public class CustomTabPage : TabPage
    {
        public IntPtr ChildWindow { get; set; }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            if (ChildWindow != IntPtr.Zero)
            {
                /*WinApi.ShowWindow(ChildWindow, WindowShowStyle.Restore);
                WinApi.ShowWindow(ChildWindow, WindowShowStyle.Maximize);*/
                //WinApi.MoveWindow(ChildWindow, 0, 0, Width, Height, true);
                WinApi.SetWindowPos(ChildWindow, new IntPtr(0), 0, 0, Width, Height,(int) SetWindowPosFlags.SWP_ASYNCWINDOWPOS);
            }
        }
    }
}
