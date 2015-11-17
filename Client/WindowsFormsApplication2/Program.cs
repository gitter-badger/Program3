using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using BidirectionalShare;

namespace WindowsFormsApplication2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TcpChannel m_TcpChan = new TcpChannel(0);
            ChannelServices.RegisterChannel(m_TcpChan, false);

            ICallsToServer m_RemoteObject = (ICallsToServer)
                Activator.GetObject(typeof(ICallsToServer),
                "tcp://127.0.0.1:8392/RemoteServer");      //  Must match IP and port on server

            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(NotifySink),
                "ServerEvents",
                WellKnownObjectMode.Singleton);
            NotifySink sink = new NotifySink();

            m_RemoteObject.Notify += new NotifyCallback(sink.FireNotifyCallback);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        class NotifySink : NotifyCallbackSink
        {
            protected override void OnNotifyCallback(string s)
            {
                if (Form1._textBox1.InvokeRequired)
                {
                    Form1._textBox1.Invoke(new MethodInvoker(delegate { Form1._textBox1.Text = s; }));
                }                
            }
        }

    }
}
