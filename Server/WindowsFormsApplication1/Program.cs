using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;
using BidirectionalShare;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BinaryServerFormatterSinkProvider provider = new BinaryServerFormatterSinkProvider();
            provider.TypeFilterLevel = TypeFilterLevel.Full;

            IDictionary props = new Hashtable();
            props["port"] = 8392;                   // This must match number on client

            TcpChannel m_tcpChannel = new TcpChannel(props, null, provider);
            ChannelServices.RegisterChannel(m_tcpChannel, false);

            RemotingConfiguration.RegisterWellKnownServiceType(
                typeof(ClientComms),
                "RemoteServer",
                WellKnownObjectMode.Singleton);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class ClientComms : MarshalByRefObject, ICallsToServer
    {
        public string SomeSimpleFunction(int n)
        {
            Console.Write("  Client sent : {0}      \r", n);
            return "Server says : " + n.ToString();
        }

        private static event NotifyCallback s_notify;

        public event NotifyCallback Notify
        {
            add { s_notify = value; }
            remove { Console.WriteLine("TODO : Notify remove."); }
        }

        public static void FireNewBroadcastedMessageEvent(string s)
        {
            Console.WriteLine("Broadcasting... Sending : {0}", s);
            s_notify(s);
        }
    }
}
