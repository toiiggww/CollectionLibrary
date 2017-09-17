using System;
using System.Linq;
using System.Collections.Generic;

namespace TEArts.Etc.CollectionLibrary
{
    #region Tree
    public interface IItem
    {
        string Text { get; }
    }
    public class ItemBase : IItem
    {
        public ItemBase() { }
        public ItemBase(string i) { Text = i; }
        public virtual string Text { get; internal set; }
    }
    public interface ILeaf<T, Q> : IItem
        where T : IItem
        where Q : IItem
    {
        INode<T, Q> Parent { get; }
        Q Object { get; }
        INode<T, Q> Move(INode<T, Q> newNode);
    }
    public class LeafBase<T, Q> : ItemBase, ILeaf<T, Q>
        where T : IItem
        where Q : IItem
    {
        public INode<T, Q> Parent { get; private set; }
        internal LeafBase() { }
        public LeafBase(Q q) { Text = q.Text; Object = q; }
        public virtual Q Object { get; internal set; }
        public INode<T, Q> Move(INode<T, Q> newNode)
        {
            Parent.RemoveLeaf(Object);
            Parent = newNode;
            Parent.AddLeaf(Object);
            return Parent;
        }
        public override string ToString()
        {
            return string.Format("{0}-:>{1}", (Parent == null ? "+-" : Parent.ToString()), Text);
        }
    }
    public interface INode<T, Q> : IItem
        where T : IItem
        where Q : IItem
    {
        INode<T, Q> Parent { get; set; }
        INode<T, Q> this[string key] { get; }
        INode<T, Q> this[int index] { get; }
        IDictionary<string, INode<T, Q>> Child { get; }
        IDictionary<string, ILeaf<T, Q>> Leafs { get; }
        string Perfix { get; }
        ILeaf<T, Q> Values(string key);
        ILeaf<T, Q> Values(int index);
        INode<T, Q> AddChild(T t);
        bool RemoveChild(T t);
        ILeaf<T, Q> AddLeaf(Q q);
        bool RemoveLeaf(Q q);
        INode<T, Q> Create(IItem i);
    }
    public class NodeBase<T, Q> : ItemBase, INode<T, Q>
        where T : IItem
        where Q : IItem
    {
        public NodeBase()
        {
            Child = new Dictionary<string, INode<T, Q>>();
            Leafs = new Dictionary<string, ILeaf<T, Q>>();

        }
        public NodeBase(IItem i)
            : this()
        {
            Text = i.Text;
        }
        public INode<T, Q> this[string key] { get { if (Child.ContainsKey(key))return Child[key]; else return null; } }
        public INode<T, Q> this[int index] { get { if (Child.Count > index)return Child[(Child.Keys.ToArray<string>())[index]]; else return null; } }
        public virtual IDictionary<string, INode<T, Q>> Child { get; internal set; }
        public virtual IDictionary<string, ILeaf<T, Q>> Leafs { get; internal set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual INode<T, Q> Parent { get; set; }
        public virtual string Perfix { get { return (Parent == null ? "+-+" : Parent.Perfix + "--+") + Text; } }
        public virtual ILeaf<T, Q> Values(string key) { if (Leafs.ContainsKey(key))return Leafs[key]; else return null; }
        public virtual ILeaf<T, Q> Values(int index) { if (Leafs.Count > index) { return Leafs[(Leafs.Keys.ToArray<string>())[index]]; } else return null; }
        public virtual INode<T, Q> AddChild(T t) { if (Child.ContainsKey(t.Text))return null; else if (t is INode<T, Q>) { Child.Add(t.Text, (t as INode<T, Q>)); (t as INode<T, Q>).Parent = this; return (t as INode<T, Q>); } else { INode<T, Q> r = Create(t); if (r != null) { Child.Add(t.Text, r); r.Parent = this; return r; } else return null; } }
        public virtual bool RemoveChild(T t) { if (Child.ContainsKey(t.Text)) { Child.Remove(t.Text); return true; } else return false; }
        public virtual ILeaf<T, Q> AddLeaf(Q q) { if (Leafs.ContainsKey(q.Text))return null; else { ILeaf<T, Q> r = new LeafBase<T, Q>(q); Leafs.Add(q.Text, r); return r; } }
        public virtual bool RemoveLeaf(Q q) { if (Leafs.ContainsKey(q.Text)) { Leafs.Remove(q.Text); return true; } else return false; }
        public virtual INode<T, Q> Create(IItem i) { throw new NotImplementedException(); }
        public override string ToString()
        {
            string s = string.Empty;
            //r = (Parent == null ? "+-" : Parent.ToString());
            foreach (var n in Child.Values)
            {
                s = string.Format("{0}{1}{2}-+-{4}", s, Environment.NewLine, Perfix, Text, n.ToString());
            }
            foreach (var l in Leafs.Values)
            {
                s = string.Format("{0}{1}{2}-=>{4}", s, Environment.NewLine, Perfix, Text, l.Text);
            }
            return s;
        }
    }
    public interface ITree<T, Q>
        where T : IItem
        where Q : IItem
    {
        INode<T, Q> Root { get; }
    }
    public class Tree<T, Q> : ItemBase, ITree<T, Q>
        where T : IItem
        where Q : IItem
    {
        public Tree() { }
        public Tree(INode<T,Q> root) : this() { Root = root; }
        public virtual INode<T, Q> Root { get; internal set; }
        public override string ToString()
        {
            return Text + Environment.NewLine + Root.ToString();
        }
    }
    #endregion
}
