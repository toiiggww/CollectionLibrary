using System.Configuration;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TEArts.Etc.CollectionLibrary
{
    public class ConfigService
    {
        public static string ReadAppConfig(string node, string file)
        {
            //string s = ConfigurationManager.AppSettings.Get(node);
            string s="";
            if (ConfigurationManager.AppSettings.AllKeys.Contains<string>(node))
            {
                s = ConfigurationManager.AppSettings[node];
            }
            else
            {
                //ConfigurationManager.AppSettings.Set(node, file);
                //s = file;
                //ConfigurationSection cfs = cfg.GetSection("Configure");
                Configuration cfl = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                cfl.AppSettings.Settings.Add(node, file);
                cfl.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                s = file;
            }
            return s;
        }
        public static T readConfig<T>(string node, string file)
        {
            try
            {
                string f = ReadAppConfig(node, file);
                using (TextReader tr = ((TextReader)(new StreamReader(f))))
                {
                    f = tr.ReadToEnd();
                    tr.Close();
                    return JsonConvert.DeserializeObject<T>(f);
                    //return null;
                }
            }
            catch
            {
                return default(T);
            }
        }
        public static bool saveConfig<T>(T config, string node, string file)
        {
            try
            {
                using (TextWriter tr = new StreamWriter(ReadAppConfig(node, file)))
                {
                    tr.Write(JsonConvert.SerializeObject(config));
                    tr.Flush();
                    tr.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
