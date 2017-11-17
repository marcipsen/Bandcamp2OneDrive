using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bandcamp2OneDriveLogic
{
    public class Configs
    {
        public string InputPath { get; set; }
        public string OutputPathRoot { get; set; }
        public string[] Genres { get; set; }
        public string[] GenrePaths { get; set; }

        public static Configs GetConfigs()
        {
            if (File.Exists("configs.json"))
            {
                Configs configs = null;

                try
                {
                    string json = File.ReadAllText("configs.json");
                    configs = JsonConvert.DeserializeObject<Configs>(json);
                    if (configs == null)
                    {
                        throw new ApplicationException("Config was empty. Please correct or enter manually");                        
                    }
                    else
                    {
                        return configs;
                    }

                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Config not readable. Please correct or enter manually: " + ex.ToString());
                }
   
            }
            else
            {
                throw new ApplicationException("No config found. Please create or enter manually");
            }
        }

    }
}
