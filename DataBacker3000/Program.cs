using System;

namespace DataBacker3000
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=.;Integrated Security=SSPI;Pooling=False";
            string pathFolder1 = $@"C:\DataBacker3000\DBBackups\Folder1";
            string pathFolder2 = $@"C:\DataBacker3000\DBBackups\Folder2";
            Configuration configuration = new Configuration();
            BackupService backupFolder1 = new BackupService(connectionString, pathFolder1);
            BackupService backupFolder2 = new BackupService(connectionString, pathFolder2);

            if (configuration.configDictionary["folder1"] == false && configuration.configDictionary["folder2"] == false)
            {
                backupFolder1.BackupAllUserDatabases();
                backupFolder2.BackupAllUserDatabases();
                configuration.configDictionary["folder2"] = true;
            }
            else
            {
                if (configuration.configDictionary["folder1"] == false)
                {
                    backupFolder1.BackupAllUserDatabases();
                    configuration.configDictionary["folder1"] = true;
                    configuration.configDictionary["folder2"] = false;
                }
                else
                {
                    backupFolder2.BackupAllUserDatabases();
                    configuration.configDictionary["folder1"] = false;
                    configuration.configDictionary["folder2"] = true;
                }
            }
            configuration.ExportConfig();

            Console.WriteLine(DateTime.Now);
            Console.WriteLine("DatabaseBacker3000 success!!!!");
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();

        }
    }

}
