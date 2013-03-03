using HostApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using WindowManager;

namespace WinFormApp
{
    public class WindowCallback : IWindowCallback
    {
        private readonly CustomTabPage _control;
        public WindowCallback(Control control)
        {
            _control = control as CustomTabPage;
        }
        public void SetChildWindow(int child, int parent)
        {
            var pHandle = new IntPtr(parent);
            var childPtr = new IntPtr(child);
            _control.ChildWindow = childPtr;
            WinApi.SetParent(childPtr, pHandle);
            WinApi.ShowWindow(childPtr, WindowShowStyle.Maximize);
            var lStyle = WinApi.GetWindowLongPtr(childPtr, (int)WindowLongFlags.GWL_STYLE).ToInt32();
            lStyle &= ~(int)(WindowStyles.WS_CAPTION |WindowStyles.WS_THICKFRAME|  WindowStyles.WS_MINIMIZE | WindowStyles.WS_MAXIMIZE | WindowStyles.WS_SYSMENU);
            lStyle |= (int)(WindowStyles.WS_CHILD);
            WinApi.SetWindowLongPtr(new HandleRef(null, childPtr), (int)WindowLongFlags.GWL_STYLE, new IntPtr(lStyle));
            //WinApi.SetWindowPos(childPtr, new IntPtr(0), 0, 0, _control.Width, _control.Height, 0);
        }
    }
}
