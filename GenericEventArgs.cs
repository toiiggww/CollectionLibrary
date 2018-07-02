using System;

namespace TEArts.Etc.CollectionLibrary
{
    public class GenericEventArgs<O> : EventArgs
    {
        public O Value1 { get; set; }
    }
    public class GenericEventArgs<O, P> : GenericEventArgs<O>
    {
        public P Value2 { get; set; }
    }
    public class GenericEventArgs<O, P, Q> : GenericEventArgs<O, P>
    {
        public Q Value3 { get; set; }
    }
    public class GenericEventArgs<O, P, Q, R> : GenericEventArgs<O, P, Q>
    {
        public R Value4 { get; set; }
    }
    public class GenericEventArgs<O, P, Q, R, S> : GenericEventArgs<O, P, Q, R>
    {
        public S Value5 { get; set; }
    }
    public class GenericEventArgs<O, P, Q, R, S, T> : GenericEventArgs<O, P, Q, R, S>
    {
        public T Value6 { get; set; }
    }
    public class GenericEventArgs<O, P, Q, R, S, T, U> : GenericEventArgs<O, P, Q, R, S, T>
    {
        public U Value7 { get; set; }
    }
    public class GenericEventArgs<O, P, Q, R, S, T, U, V> : GenericEventArgs<O, P, Q, R, S, T, U>
    {
        public V Value8 { get; set; }
    }
    public class GenericEventArgs<O, P, Q, R, S, T, U, V, W> : GenericEventArgs<O, P, Q, R, S, T, U, V>
    {
        public W Value9 { get; set; }
    }
    public class GenericEventArgs<O, P, Q, R, S, T, U, V, W, X> : GenericEventArgs<O, P, Q, R, S, T, U, V, W>
    {
        public X Value10 { get; set; }
    }
    public class GenericEventArgs<O, P, Q, R, S, T, U, V, W, X, Y> : GenericEventArgs<O, P, Q, R, S, T, U, V, W, X>
    {
        public Y Value11 { get; set; }
    }
    public class GenericEventArgs<O, P, Q, R, S, T, U, V, W, X, Y, Z> : GenericEventArgs<O, P, Q, R, S, T, U, V, W, X, Y>
    {
        public Z Value12 { get; set; }
    }
}
