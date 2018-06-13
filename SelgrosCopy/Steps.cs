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
        public static void CreateDir(SelgorsCopyModel model)
        {
            switch (model.Country)
            {
                case "PL":
                    model.DirInfo = Directory.CreateDirectory(Path.Combine(Consts.PolandDestinationPath, DateTime.Now.ToString("dd.MM.yyyy")));
                    break;
                case "RO":
                    model.DirInfo = Directory.CreateDirectory(Path.Combine(Consts.RomaniaDestinationPath, DateTime.Now.ToString("dd.MM.yyyy")));
                    break;
                default:
                    model.DirInfo = Directory.CreateDirectory(Path.Combine(Consts.RussiaDestinationPath, DateTime.Now.ToString("dd.MM.yyyy")));
                    break;
            }

            foreach (var item in Directory.GetFiles(model.DirInfo.FullName))
            {
                File.Delete(item);
            }

            Console.Write($"Create directory {model.DirInfo.FullName}");
        }

        public static void CreateSchema2(SelgorsCopyModel model)
        {
            Console.Write($"CreateSchema");

            var schema2 = File.ReadAllLines(Consts.Schema2FilePath).Skip(int.Parse(model.LinesCut));

            File.WriteAllLines(Path.Combine(model.DirInfo.FullName, "SelgrosPG_Schema2.sql"), schema2);
        }

        public static void CreateTranslations(SelgorsCopyModel model)
        {
            Console.Write($"Copy translations");

            File.Copy(Consts.TranslationsFilePath, Path.Combine(model.DirInfo.FullName, "SelgrosPG_Translations.sql"), true);
        }

        public static void CopyApp(SelgorsCopyModel model)
        {
            Console.Write($"Copy {model.File.Name}");

            File.Copy(model.File.FullName, Path.Combine(model.DirInfo.FullName, model.File.Name), true);
        }

        public static void CreateUpdateScript(SelgorsCopyModel model)
        {
            Console.Write($"Create update script");

            switch (model.Country)
            {
                case "PL":
                    model.UpdateScript = new UpdateScriptBuilderPL().Build(model.File.Name, model.Version);
                    break;
                case "RO":
                    model.UpdateScript = new UpdateScriptBuilderRO().Build(model.File.Name, model.Version);
                    break;
                default:
                    break;
            }

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
        }

        public static void GetArtifacts(SelgorsCopyModel model)
        {
            Console.Write($"Get artifacts file");

            model.File = new DirectoryInfo(Path.Combine(@"D:\Pobrane\"))
               .GetFiles("Selgros_PG_SPG_QAS_PL_*")
               .OrderBy(s => s.CreationTime)
               .Last();

            Console.Write($" {model.File.Name}");
        }
    }
}
