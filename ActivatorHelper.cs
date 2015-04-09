using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TEArts.Etc.CollectionLibrary
{
    public class ActivatorHelper
    {
        public static T CreateRemoteObject<T>(AppDomain dom, string file, string ext, string type)
        {
            if (!File.Exists(file))
            {
                try
                {
                    DirectoryInfo d = new DirectoryInfo(".");
                    FileInfo[] fs = d.GetFiles(file + "*." + ext.Trim('.'), SearchOption.AllDirectories);
                    foreach (FileInfo f in fs)
                    {
                        T t = CreateRemoteObject<T>(dom, f.FullName, ext, type);
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
                T v;
                a = Assembly.LoadFile(file);
                t = (from ts in a.GetTypes() where (ts.Name.ToString()) == type select ts).First<Type>();
#if DEBUG
                v = ((T)(Activator.CreateInstance(t)));
#else
                v = ((T)(Activator.CreateInstance(dom, a.FullName, t.FullName).Unwrap()));
#endif
                return v;

            }
            return default(T);
        }
    }
}
