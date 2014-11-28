using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TEArts.Etc.CollectionLibrary
{
    public class ActivatorHelper
    {
        public static T CreateRemoteObject<T>(AppDomain dom, string file, string type)
        {
            if (!File.Exists(file))
            {
                try
                {
                    DirectoryInfo d = new DirectoryInfo(".");
                    FileInfo[] fs = d.GetFiles(file + "*", SearchOption.AllDirectories);
                    foreach (FileInfo f in fs)
                    {
                        T t = CreateRemoteObject<T>(dom, f.FullName, type);
                        if (!t.Equals(default(T)))
                        {
                            return t;
                        }
                    }
                }
                catch (Exception e)
                {
                }
            }
            else
            {
                Assembly a;
                Type t;
                a = Assembly.LoadFile(file);
                t = (from ts in a.GetTypes() where (ts.Name.ToString()) == type select ts).First<Type>();
                return ((T)(Activator.CreateInstance(dom, a.FullName, t.FullName).Unwrap()));

            }
            return default(T);
        }
    }
}
