using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataBacker3000
{
    internal class Configuration
    {
        public Dictionary<String, bool> configDictionary = new Dictionary<String, bool>();


        public Configuration()
        {
            ImportConfig();
            if (IsFirstRun())
            {
                CreateTask();
                configDictionary["firstRun"] = false;
                ExportConfig();
            }
        }

        public void ImportConfig()
        {
            var file = new StreamReader("config.txt");
            string line;
            while ((line = file.ReadLine()) != null)
            {
                var keyValue = line.Split('=');
                bool value = keyValue[1].CompareTo("True") == 0 ? true : false;
                configDictionary.Add(keyValue[0], value);
            }
            file.Close();
        }

        public void ExportConfig()
        {
            File.WriteAllLines("config.txt", configDictionary.Select(x => x.Key + "=" + x.Value.ToString()).ToArray());
        }

        public bool IsFirstRun()
        {
            return configDictionary["firstRun"];
        }

        public void CreateTask()
        {
            string cmdText;
            //cmdText = "/K SCHTASKS /CREATE /SC DAILY /TN \"DataBacker3000\\DatabaseBackup\" /TR \"C: \\Users\\p129fb7\\source\\repos\\DataBacker3000\\DataBacker3000\\bin\\Release\\DataBacker3000.exe\" /ST 00:42";
            cmdText = "/C SCHTASKS /CREATE /SC DAILY /TN \"DataBacker3000\\DatabaseBackup\" /TR \"C: \\DataBacker3000\\DataBacker3000.exe\" /ST %TIME:~-11,5%";
            System.Diagnostics.Process.Start(@"C:\Windows\system32\cmd.exe", cmdText);
        }


    }
}
