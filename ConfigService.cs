using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;

namespace CollectionLibrary
{
    public class ConfigService
    {
        internal static string getFile(string node, string file)
        {
            string s = ConfigurationManager.AppSettings.Get(node);
            if (string.IsNullOrEmpty(s))
            {
                //ConfigurationManager.AppSettings.Set(node, file);
                //s = file;
                Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ExeConfigurationFileMap cfm = new ExeConfigurationFileMap();
                cfm.ExeConfigFilename = cfg.FilePath;
                Configuration cfl = ConfigurationManager.OpenMappedExeConfiguration(cfm, ConfigurationUserLevel.None);
                //ConfigurationSection cfs = cfg.GetSection("Configure");
                cfl.AppSettings.Settings.Add(node, file);
                cfl.Save(ConfigurationSaveMode.Modified);
            }
            return s;
        }
        public static T readConfig<T>(string node, string file)
        {
            try
            {
                string f = getFile(node, file);
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
                using (TextWriter tr = new StreamWriter(getFile(node, file)))
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
