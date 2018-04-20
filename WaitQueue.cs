﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TEArts.Etc.CollectionLibrary
{
    public class WaitQueue<T> : ConcurrentQueue<T>
    {
        private CancellationToken mbrCancelToken;
        private EventHandler<QueueFullEventArgs<T>> OnQueueFull;

        public WaitQueue(CancellationToken cancelToken) : this(-1, cancelToken) { }
        public WaitQueue(int max, CancellationToken cancelToken) // : base(max > 0 ? max : 4)
        {
            MaxSize = max;
            CancelToken = cancelToken;
            WaitHandle = new AutoResetEvent(true);
        }

        public CancellationToken CancelToken
        {
            get { return mbrCancelToken; }
            set
            {
                if (value == mbrCancelToken) { return; }
                mbrCancelToken = value;
                mbrCancelToken.Register(() => WaitHandle.Set());
            }
        }
        public int MaxSize { get; private set; }
        public AutoResetEvent WaitHandle { get; private set; }
        public int MaxCountOfDequeue { get; private set; } = 100;

        public T Dequeue() { return Dequeue(-1); }
        public T Dequeue(int millisecondsTimeout)
        {
            T t = default(T);
            if (CancelToken.IsCancellationRequested)
            {
                return t;
            }
            if (Count == 0)
            {
                WaitHandle.WaitOne(millisecondsTimeout);
            }
            if (CancelToken.IsCancellationRequested)
            {
                return t;
            }
            TryDequeue(out t);
            return t;
        }

        public new void Enqueue(T item)
        {
            if (MaxSize > 0 && Count == MaxSize)
            {
                T value = Dequeue();
                OnQueueFull?.Invoke(this, new QueueFullEventArgs<T>() { Dequeued = value, Max = MaxSize });
            }
            base.Enqueue(item);
            if (Count <= 5 || 0 == Count % (MaxCountOfDequeue / 20))
            {
                WaitHandle.Set();
            }
            WaitHandle.Set();
>>>>>>> 5f8c8f281847836c2098b1c1859678c4fea72660
        }
        public Task<List<T>> Dequeue(int count = -1, int millisecondsTimeout = -1)
        {
            return this.Dequeue(null, count, -1);
        }
        public Task<List<T>> Dequeue(List<T> value, int count, int millisecondsTimeout)
        {
            return Task.Factory.StartNew(() =>
            {
                if (value == null)
                {
                    value = new List<T>();
                }
                if (count <= 0)
                {
                    count = MaxCountOfDequeue;
                }
                T t = default(T);
                millisecondsTimeout = Wait(millisecondsTimeout);
                int c = 0;
                while (c < count)
                {
                    if (CancelToken.IsCancellationRequested)
                    {
                        break;
                    }
                    Wait(millisecondsTimeout);
                    if (CancelToken.IsCancellationRequested)
                    {
                        break;
                    }
                    if (TryDequeue(out t))
                    {
                        value.Add(t);
                    }
                    else
                    {
                        break;
                    }
                    c++;
                }
                return value;
            });
        }

        private int Wait(int millisecondsTimeout)
        {
            DateTime waitStart = DateTime.Now;
            if (Count == 0)
            {
                WaitHandle.WaitOne(millisecondsTimeout);
            }
            DateTime waitEnd = DateTime.Now;
            if (millisecondsTimeout == -1)
            {
                millisecondsTimeout = 0;
            }
            else if (millisecondsTimeout > 0)
            {
                if (
                    ((waitEnd - waitStart).TotalMilliseconds) > int.MaxValue ||
                    -1 == (millisecondsTimeout - (int)((waitEnd - waitStart).TotalMilliseconds)))
                {
                    millisecondsTimeout = 0;
                }
                else
                {
                    millisecondsTimeout -= (int)((waitEnd - waitStart).TotalMilliseconds);
                }
            }

            return millisecondsTimeout;
        }
        public void Reset()
        {
            while (Count > 0)
            {
                TryDequeue(out T t);
            }
            WaitHandle.Reset();
        }
    }

    public class QueueFullEventArgs<T> : EventArgs
    {
        public int Max { get; set; }
        public T Dequeued { get; set; }
    }

}
