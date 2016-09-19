using System;
using System.Collections.Generic;
using System.Threading;

namespace TEArts.Etc.CollectionLibrary
{
    public interface IDentity
    {
        int IDentity { get; }
    }
    public class Pooler<TEArtsType> where TEArtsType : IDentity
    {
        private Queue<TEArtsType> mbrPooler;
        private AutoResetEvent mbrEmptyLocker;
        private int mbrIndexer;
        private int mbrStarter;
        private int mbrMaxpean;
        private int waiteTime { get; set; }
        private bool mbrForAbort;
        public TEArtsType Popup()
        {
            while (mbrPooler.Count == 0 && waiteTime <= 0)
            {
                mbrEmptyLocker.Reset();
                if (waiteTime <= 0)
                {
                    mbrEmptyLocker.WaitOne();
                }
                else
                {
                    mbrEmptyLocker.WaitOne(waiteTime);
                }
                if (mbrForAbort)
                {
                    Thread.CurrentThread.Abort();
                }
            }
            waiteTime = -1;
            try { return mbrPooler.Dequeue(); }
            catch { return default(TEArtsType); }
        }
        public TEArtsType PopupNow( ) { waiteTime = 10; mbrEmptyLocker.Set(); return Popup(); }
        public int Pushin(TEArtsType tt)
        {
            mbrPooler.Enqueue(tt);
            if (mbrPooler.Count >= 1)
            {
                mbrEmptyLocker.Set();
            }
            return tt.IDentity;
        }
        public void AbortWait()
        {
            mbrForAbort = true;
            mbrEmptyLocker.Set();
        }
        public int NextIndex
        {
            get
            {
                Interlocked.CompareExchange(ref mbrIndexer, mbrStarter, mbrMaxpean);
                return Interlocked.Increment(ref mbrIndexer);
            }
        }
        public int CurrentSize { get { return mbrPooler.Count; } }
        public Pooler(int size, int index, int max)
        {
            mbrPooler = new Queue<TEArtsType>(size);
            mbrIndexer = index;
            mbrMaxpean = max;
            mbrStarter = index;
            mbrEmptyLocker = new AutoResetEvent(false);
        }
        public Pooler(int size, int index) : this(size, index, int.MaxValue) { }
        public Pooler(int size) : this(size, int.MinValue + 1, int.MaxValue) { }

        public TEArtsType[] CopyTo()
        {
            TEArtsType[] r = new TEArtsType[mbrPooler.Count];
            mbrPooler.CopyTo(r, 0);
            return r;
        }
    }
}
