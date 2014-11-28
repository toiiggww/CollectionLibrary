using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace TEArts.Etc.CollectionLibrary
{
    public interface IDebugerLoger
    {
        DebugType DebugType { get; set; }
        void WriteLog(object log, DebugType type);
        void RegistLoger();
    }
    public enum DebugType { Error, AuditSuccess, AuditFalue, Warning, Info, Debug, FuncationCall }
    public abstract class DebugerLoger : MarshalByRefObject,IDebugerLoger
    {
        public virtual DebugType DebugType { get; set; }
        public virtual void WriteLog(object log, DebugType type) { }
        public virtual void RegistLoger() { }
    }
    public class ConsoleLoger : DebugerLoger
    {
        private ConsoleColor Back { get; set; }
        private ConsoleColor Fore { get; set; }
        public override void WriteLog(object log, DebugType type)
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
                if (type <= DebugType)
                {
                    Console.WriteLine(string.Format("{0}\t{1}\t{2}", DateTime.Now, type, l));
                }
            }
            Console.ResetColor();
        }
        public override void RegistLoger()
        {
            Debuger.Loger.AddLoger("Console",this);
        }
    }
    public class EventsLoger : DebugerLoger
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
        public override void WriteLog(object log, DebugType type)
        {
            if (type <= DebugType)
            {
                mbrLoger.WriteEntry(string.Format("{0}{1}{2}", DateTime.Now, Environment.NewLine, log.ToString()));
            }
        }
        public override void RegistLoger()
        {
            Debuger.Loger.AddLoger("Events", this);
        }
    }
    public class NetworkLoger : DebugerLoger
    {
        private System.Net.Sockets.Socket mbrSocket;
        public NetworkLoger(System.Net.IPAddress ip, int port, System.Net.Sockets.ProtocolType type)
        {
        }
        public override void WriteLog(object log, DebugType type)
        {
            if (type <= DebugType)
            {
                mbrSocket.Send(Encoding.UTF8.GetBytes(string.Format("{0}{1}{2}", DateTime.Now, Environment.NewLine, log.ToString())));
            }
        }
        public override void RegistLoger()
        {
            Debuger.Loger.AddLoger("Network", this);
        }
    }
    public class TextFileLoger : DebugerLoger
    {
        public TextFileLoger()
            : this("Application.log")
        {
        }
        public TextFileLoger(string file)
        {
            FileName = file;
            Writer = new StreamWriter(FileName, true, Encoding.UTF8);
            Writer.WriteLine(string.Format("{0}\t{1}\t{2}", DateTime.Now, DebugType.Info, "Logger ready for writting."));
            Writer.Flush();
        }
        public string FileName { get; set; }
        private TextWriter Writer { get; set; }
        public override void WriteLog(object log, DebugType type)
        {
            if (type <= DebugType)
            {
                Writer.WriteLine(string.Format("{0}\t{1}\t{2}", DateTime.Now, type, log));
                Writer.Flush();
            }
        }
        public override void RegistLoger()
        {
            Debuger.Loger.AddLoger("TextLoger", this);
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
            if (!mbrLogers.ContainsKey(key))
            {
                mbrLogers.Add(key, loger);
            }
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
        private void DebugInfoInternal(object o, DebugType type)
        {
            lock (mbrDebugHandler)
            {
                if (o == null)
                {
                    DebugInfoInternal("====]> NULL <[====", type);
                }
                else
                {
                    foreach (IDebugerLoger l in mbrLogers.Values)
                    {
                        l.WriteLog(o, type);
                    }
                }
            }
        }
        public void DebugInfo(DebugType type, byte[] message)
        {
            DebugInfoInternal(BiteArray.FormatArrayMatrix(message) as object, type);
        }
        public void DebugInfo(DebugType type, string message)
        {
            DebugInfoInternal(message, type);
        }
        public void DebugInfo(string formater, params object[] args)
        {
            DebugInfo(DebugType.Info, formater, args);
        }
        public void DebugInfo(DebugType type,string formater, params object [] args)
        {
            DebugInfoInternal(string.Format(formater, args), type);
        }
        public void DebugInfo(object o) { DebugInfoInternal(o, DebugType.Info); }
    }
}
