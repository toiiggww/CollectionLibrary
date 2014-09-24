using System;
using System.Data;
using System.Data.Sql;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;

namespace CollectionLibrary
{
    public delegate void ReaderRecall(IDataReader R);
    public delegate object ReaderRecallReturn(IDataReader R);
    public class DBHelper
    {
        public DBHelper()
        {
            if (mbrConnections == null)
            {
                mbrConnections = new Dictionary<string, IDbConnection>();
            }
            if (mbrQueryPooler == null)
            {
                mbrQueryPooler = new Pooler<QueryStateObject>(65536);
            }
            Cancel = false;
            mbrQuery = new Thread(new ThreadStart(Run));
        }
        public DBHelper(string db)
            : this()
        {
            InitialCatalog = db;
        }
        public void Start()
        {
            mbrQuery.Start();
        }
        private Thread mbrQuery { get; set; }
        private static Dictionary<string, IDbConnection> mbrConnections;
        private Pooler<QueryStateObject> mbrQueryPooler;
        public string InitialCatalog { get; private set; }
        private void Exec(QueryStateObject state)
        {
            if (mbrCancel)
            {
                return;
            }
            if (mbrConnections.ContainsKey(state.Connection))
            {
                IDbConnection c = mbrConnections[state.Connection];
                if (c.State != ConnectionState.Open)
                {
                    c.Open();
                }
                using (IDbCommand cmd = c.CreateCommand())
                {
                    cmd.CommandText = state.Sql;
                    if (state.Params != null)
                    {
                        cmd.Parameters.Clear();
                        foreach (StringPaire s in state.Params)
                        {
                            IDbDataParameter p = cmd.CreateParameter();
                            p.ParameterName = s.Name;
                            p.Value = s.Value;
                            cmd.Parameters.Add(p);
                        }
                    }
                    try
                    {
                        IDataReader r = cmd.ExecuteReader();
                        //if (r.Read())
                        //{
                        if (state.Callback != null)
                        {
                            state.Callback(r);
                        }
                        //}
                        try { r.Close(); }
                        catch { }
                    }
                    catch { }
                }
            }
            else
            {
                throw new Exception("Connection : [" + state.Connection + "] not exist.");
            }
        }
        public string AddConnection(string con, IDbConnection instan)
        {
            if (!mbrConnections.ContainsKey(con))
            {
                mbrConnections.Add(con, instan);
                return "_OK_";
            }
            else
            {
                return mbrConnections[con].ConnectionString;
            }
        }
        public int AddTask(string Connection, string Sql, ReaderRecall Callback, params StringPaire[] Params)
        {
            QueryStateObject q = new QueryStateObject(Connection,Sql,Callback,Params);
            q.IDentity = mbrQueryPooler.NextIndex;
            return mbrQueryPooler.Pushin(q);
        }
        public int AddTask(string Connection, string Sql, params StringPaire[] Params)
        {
            return AddTask(Connection, Sql, null, Params);
        }
        private bool mbrCancel = false;
        public bool Cancel { get { return mbrCancel; } set { mbrCancel = value; if (value) { mbrQueryPooler.AbortWait(); } } }
        private void Run()
        {
            while (!mbrCancel)
            {
                Exec(mbrQueryPooler.Popup());
            }
        }
    }
    public class StringPaire
    {
        public string Name { get; private set; }
        public object Value { get; private set; }
        public StringPaire(string n, object v) { Name = n; Value = v; }
    }
    public class QueryStateObject : IDentity
    {
        public QueryStateObject(string con, string sql, ReaderRecall Call, params StringPaire[] arams)
        {
            Connection = con;
            Sql = sql;
            Callback = Call;
            Params = arams;
        }
        public string Connection { get; private set; }
        public string Sql { get; private set; }
        public ReaderRecall Callback { get; private set; }
        public StringPaire[] Params { get; private set; }
        public int IDentity { get; set; }
    }
}
