using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BidirectionalShare
{
    public delegate void NotifyCallback(string s);

    public interface ICallsToServer
    {
        string SomeSimpleFunction(int n);

        event NotifyCallback Notify;
    }

    public abstract class NotifyCallbackSink : MarshalByRefObject
    {
        public void FireNotifyCallback(string s)
        {
            Console.WriteLine("Activating callback");
            OnNotifyCallback(s);
        }

        protected abstract void OnNotifyCallback(string s);
    }
}
