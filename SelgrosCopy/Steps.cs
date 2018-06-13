using SelgrosCopy.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SelgrosCopy
{
    public class Steps
    {
        public static void CreateSchema2(SelgorsCopyModel model)
        {
            Console.Write($"CreateSchema");

            var schema2 = File.ReadAllLines(@"D:\Projects\Selgros\PG\trunk\DBScripts\SelgrosPG_Schema2.sql").Skip(int.Parse(model.LinesCut));

            File.WriteAllLines(Path.Combine(model.DirInfo.FullName, "SelgrosPG_Schema2.sql"), schema2);
        }

        public static void CreateTranslations(SelgorsCopyModel model)
        {
            Console.Write($"Copy translations");

            File.Copy(@"D:\Projects\Selgros\PG\trunk\DBScripts\SelgrosPG_Translations.sql", Path.Combine(model.DirInfo.FullName, "SelgrosPG_Translations.sql"), true);
        }

        public static void CopyApp(SelgorsCopyModel model)
        {
            Console.Write($"Copy {model.File.Name}");

            File.Copy(model.File.FullName, Path.Combine(model.DirInfo.FullName, model.File.Name), true);
            
        }

        public static void CreateUpdateScript(SelgorsCopyModel model)
        {
            Console.Write($"Create update script");

            File.WriteAllText(Path.Combine(model.DirInfo.FullName, "update.bat"), model.UpdateScript);
        }

        public static void CreateAppsettings(SelgorsCopyModel model)
        {
            Console.Write($"Create appsettings.json");

            File.WriteAllText(Path.Combine(model.DirInfo.FullName, "appsettings.json"), "{\"CheckVersionProcess.ExcludeDirectories\": [\"App_Data\",\"UploadImages\" ]}");
        }

        public static void Stop(SelgorsCopyModel model)
        {
            Console.Write($"Update package create on {model.DirInfo.FullName}");

            File.WriteAllText(Path.Combine(model.DirInfo.FullName, "appsettings.json"), "{\"CheckVersionProcess.ExcludeDirectories\": [\"App_Data\",\"UploadImages\" ]}");
        }
    }
}
