using RestSharp;
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
            Console.Write($"Create directory ");

            switch (model.Country)
            {
                case "PL":
                    model.DirInfo = Directory.CreateDirectory(Path.Combine(Consts.PolandDestinationPath, DateTime.Now.ToString("dd.MM.yyyy")));
                    break;
                case "RO":
                    model.DirInfo = Directory.CreateDirectory(Path.Combine(Consts.RomaniaDestinationPath, DateTime.Now.ToString("dd.MM.yyyy")));
                    break;
                case "RU":
                    model.DirInfo = Directory.CreateDirectory(Path.Combine(Consts.RussiaDestinationPath, DateTime.Now.ToString("dd.MM.yyyy")));
                    break;
                default:
                    throw new Exception($"{model.Country} is not valid country");
            }

            foreach (var item in Directory.GetFiles(model.DirInfo.FullName))
            {
                File.Delete(item);
            }

            Console.Write($"{model.DirInfo.FullName}");
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
            Console.Write($"Copy");

            File.Copy(model.File.FullName, Path.Combine(model.DirInfo.FullName, model.File.Name), true);

            Console.Write($" {model.File.Name}");
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
                     case "RU":
                    model.UpdateScript = new UpdateScriptBuilderRU().Build(model.File.Name, model.Version);
                    break;
                default:
                    throw new Exception($"{model.Country} is not valid country");
            }

            File.WriteAllText(Path.Combine(model.DirInfo.FullName, "update.bat"), model.UpdateScript);
        }

        public static void CreateUpdateScriptTestEnv(SelgorsCopyModel model)
        {
            Console.Write($"Create update script for test environment");

            switch (model.Country)
            {
                case "PL":
                    model.UpdateScript = new UpdateScriptBuilderPL().BuildTest(model.File.Name, model.Version);
                    break;
                case "RO":
                    model.UpdateScript = new UpdateScriptBuilderRO().BuildTest(model.File.Name, model.Version);
                    break;
                case "RU":
                    model.UpdateScript = new UpdateScriptBuilderRU().BuildTest(model.File.Name, model.Version);
                    break;
                default:
                     throw new Exception($"{model.Country} is not valid country");
            }

            File.WriteAllText(Path.Combine(model.DirInfo.FullName, "update-test-environment.bat"), model.UpdateScript);
        }

        public static void CreateAppsettings(SelgorsCopyModel model)
        {
            Console.Write($"Create appsettings.json");

            File.WriteAllText(Path.Combine(model.DirInfo.FullName, "appsettings.json"), "{\"CheckVersionProcess.ExcludeDirectories\": [\"App_Data\",\"UploadImages\",\"logs\" ]}");
        }

        public static void Stop(SelgorsCopyModel model)
        {
            Console.Write($"Update package create on {model.DirInfo.FullName}");
        }

        public static void GetArtifacts(SelgorsCopyModel model)
        {
            Console.Write($"Get artifacts file");

            model.File = new DirectoryInfo(Path.Combine(@"D:\Pobrane\"))
               .GetFiles("Selgros_PG_SPG_*_artifacts.zip")
               .OrderBy(s => s.CreationTime)
               .Last();

            Console.Write($" {model.File.Name}");
        }

        //public static void CreatePage(SelgorsCopyModel model)
        //{
        //    const int PAGE_UPDATE_POLAND_ID = 7112172;
        //    const int PAGE_UPDATE_RUSIA_ID = 7112174;
        //    const int PAGE_UPDATE_ROMANIA_ID = 7131214;
           

        //        var rC = new RestClient("https://confluence.snpgroup.com/rest/api/content/");

        //    var rq = new RestRequest(Method.POST);

          
        //      var pass = Base64Encode($"walkowskip:*#polak2");
          
        //    rq.RequestFormat = DataFormat.Json;
        //    rq.AddHeader("Content-Type","application/json");
        //    rq.AddHeader("Authorization", $"Basic {pass}");

        //    var body = "{ 	\"type\": \"page\", 	\"title\": \"2018-12-14 17:30 (v99.647) (test) \", 	\"ancestors\":[{\"id\":7112172}], 	\"space\": { 		\"key\": \"SPDT\" 	}, 	\"body\": { 		\"storage\": { 			\"value\": \"<p>This is a new page</p>\", 			\"representation\": \"storage\" 		} 	} }";

        //    //rq.AddJsonBody(body);
          
        //    rq.AddParameter("application/json", body, ParameterType.RequestBody);

        //    var result = rC.Execute(rq,Method.POST);

        //}

        public static string Base64Encode(string plainText) {
          var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
          return Convert.ToBase64String(plainTextBytes);
        }
    }
}
