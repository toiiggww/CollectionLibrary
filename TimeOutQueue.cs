using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace TEArts.Etc.CollectionLibrary
{
    public class TimeOutQueue<TEArtsType> : IDisposable
    {
        private SortedList<DateTime, List<TimedItem<TEArtsType>>> List = new SortedList<DateTime, List<TimedItem<TEArtsType>>>();
        private Timer Timer;
        //public EventHandler<TEArtsType> OnTimeOut;
        public long Millisecond { get; private set; }
        public DateTime Next { get; private set; }
        public TimeOutQueue() : base()
        {
            Timer = new Timer(x =>
            {
                DateTime dt = ((DateTime)(x));
                if (!List.ContainsKey(dt))
                {
                    return;
                }
                List<TimedItem<TEArtsType>> t = List[dt];
                foreach (TimedItem<TEArtsType> tt in t)
                {
                    tt.Callback(tt.Item);
                }
                List.Remove(dt);
                nextTimer();
            }, Next, Timeout.Infinite, Timeout.Infinite);
            Next = DateTime.MaxValue;
        }
        public TimeOutQueue(long mill) : this()
        {
            Millisecond = mill;
        }
        public TimedItem<TEArtsType> Add(TEArtsType item, Action<TEArtsType> onTimeOut)
        {
            return Add(item, Millisecond, onTimeOut);
        }
        public TimedItem<TEArtsType> Add(TEArtsType item, long mill, Action<TEArtsType> onTimeOut)
        {
            TimedItem<TEArtsType> i = new TimedItem<TEArtsType>(Millisecond);
            List<TimedItem<TEArtsType>> ts = null;
            lock (List)
            {
                if (List.ContainsKey(i.TimeOut))
                {
                    ts = List[i.TimeOut];
                }
                else
                {
                    ts = new List<TimedItem<TEArtsType>>();
                }
                ts.Add(i);
                if (Next > i.TimeOut)
                {
                    Timer.Change(mill, Timeout.Infinite);
                }
            }
            return i;
        }
        public void Remove(TimedItem<TEArtsType> item)
        {
            lock (List)
            {
                if (List.ContainsKey(item.TimeOut))
                {
                    List[item.TimeOut].Remove(item);
                }
            }
        }
        private void nextTimer()
        {
            if (List.Keys.Count > 0)
            {
                Next = List.Keys.Min();
                Timer.Change(((long)((Next - DateTime.Now).TotalMilliseconds)), Timeout.Infinite);
            }
            else
            {
                Timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
        }
        public void Dispose()
        {
            Timer.Change(Timeout.Infinite, Timeout.Infinite);
            Timer.Dispose();
            List.Clear();
        }
    }
    public class TimedItem<TEArtsType>
    {
        public TimedItem(double millisecond)
        {
            TimeOut = DateTime.Now.AddMilliseconds(millisecond);
        }
        public TEArtsType Item { get; private set; }
        public DateTime TimeOut { get; private set; }
        public Action<TEArtsType> Callback { get; private set; }
    }
}
