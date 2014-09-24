using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CollectionLibrary
{
    public interface IDebugerLoger
    {
        void WriteLog(object log, DebugType type);
        void RegistLoger();
    }
    public enum DebugType { Error, Warning, AuditSuccess, AuditFalue, Info, Debug, FuncationCall }
    public class ConsoleLoger : IDebugerLoger
    {
        private ConsoleColor Back { get; set; }
        private ConsoleColor Fore { get; set; }
        public void WriteLog(object log, DebugType type)
        {
            switch (type)
            {
                case DebugType.Error:
                    Back = ConsoleColor.Red;
                    Fore = ConsoleColor.White;
                    break;
                case DebugType.Warning:
                    Back = ConsoleColor.Yellow;
                    Fore = ConsoleColor.Red;
                    break;
                case DebugType.AuditSuccess:
                    Back = ConsoleColor.DarkGray;
                    Fore = ConsoleColor.White;
                    break;
                case DebugType.AuditFalue:
                    Back = ConsoleColor.DarkRed;
                    Fore = ConsoleColor.DarkYellow;
                    break;
                case DebugType.Info:
                    Console.ResetColor();
                    Back = Console.BackgroundColor;
                    Fore = Console.ForegroundColor;
                    break;
                case DebugType.Debug:
                    Back = ConsoleColor.Gray;
                    Fore = ConsoleColor.DarkGreen;
                    break;
                case DebugType.FuncationCall:
                    Back = ConsoleColor.Gray;
                    Fore = ConsoleColor.Yellow;
                    break;
                default:
                    break;
            }
            Console.BackgroundColor = Back;
            Console.ForegroundColor = Fore;
            string[] s = log.ToString().Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string l in s)
            {
                Console.WriteLine(string.Format("{0}\t{1}\t{2}", DateTime.Now, type, l));
            }
            Console.ResetColor();
        }
        public void RegistLoger()
        {
            Debuger.Loger.AddLoger("Console",this);
        }
    }
    public class EventsLoger : IDebugerLoger
    {
        private static Dictionary<string, System.Diagnostics.EventLog> mbrEvents;
        private System.Diagnostics.EventLog mbrLoger;
        public EventsLoger(string source, string name)
        {
            if (mbrEvents == null)
            {
                mbrEvents = new Dictionary<string, System.Diagnostics.EventLog>();
            }
            if (!mbrEvents.ContainsKey(name))
            {
                if (!System.Diagnostics.EventLog.SourceExists(name))
                {
                    System.Diagnostics.EventLog.CreateEventSource(source, name);
                }
                mbrLoger = new System.Diagnostics.EventLog();
                mbrLoger.Source = source;
                mbrLoger.Log = name;
                mbrEvents.Add(source, mbrLoger);
            }
            else
            {
                mbrLoger = mbrEvents[source];
            }
        }
        public void WriteLog(object log, DebugType type)
        {
            mbrLoger.WriteEntry(string.Format("{0}{1}{2}", DateTime.Now, Environment.NewLine, log.ToString()));
        }
        public void RegistLoger()
        {
            Debuger.Loger.AddLoger("Events", this);
        }
    }
    public class NetworkLoger : IDebugerLoger
    {
        private System.Net.Sockets.Socket mbrSocket;
        public NetworkLoger(System.Net.IPAddress ip, int port, System.Net.Sockets.ProtocolType type)
        {
        }
        public void WriteLog(object log, DebugType type)
        {
            mbrSocket.Send(Encoding.UTF8.GetBytes(string.Format("{0}{1}{2}", DateTime.Now, Environment.NewLine, log.ToString())));
        }
        public void RegistLoger()
        {
            Debuger.Loger.AddLoger("Network", this);
        }
    }
    public class Debuger
    {
        private static object mbrDebugHandler = new object();
        private static Debuger mbrInstance;
        private Dictionary<string, IDebugerLoger> mbrLogers;
        private Debuger()
        {
            mbrLogers = new Dictionary<string, IDebugerLoger>();
        }
        public void AddLoger(string key, IDebugerLoger loger)
        {
            if (mbrLogers.ContainsKey(key))
            {
                throw new ArgumentException(string.Format("[{0}] is already added", key));
            }
            mbrLogers.Add(key, loger);
        }
        public static Debuger Loger
        {
            get
            {
                if (mbrInstance == null)
                {
                    mbrInstance = new Debuger();
                }
                return mbrInstance;
            }
        }
        public void DebugInfo(object o, DebugType type)
        {
#if DEBUG
            lock (mbrDebugHandler)
            {
                if (o == null)
                {
                    DebugInfo("====]> NULL <[====", type);
                }
                else if (o.GetType() == typeof(byte[]))
                {
                    DebugInfo(BiteArray.FormatArrayMatrix(o as byte[]), type);
                }
                else
                {
                    foreach (IDebugerLoger l in mbrLogers.Values)
                    {
                        l.WriteLog(o, type);
                    }
                }
            }
#endif
        }
        public void DebugInfo(object o) { DebugInfo(o, DebugType.Info); }
    }
}
