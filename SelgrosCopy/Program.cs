using System;
using System.IO;
using System.Linq;

namespace SelgrosCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = new DirectoryInfo(Path.Combine(@"D:\Pobrane\"))
                .GetFiles("Selgros_PG_SPG_QAS_PL_*")
                .OrderBy(s => s.CreationTime)
                .Last();


            DirectoryInfo dirInfo;

            string updateScript = string.Empty;

            switch (args[0])
            {
                case "PL":
                    dirInfo = Directory.CreateDirectory(Path.Combine(@"D:\Selgros-Aktualizacja", DateTime.Now.ToString("dd.MM.yyyy")));
                    updateScript = new UpdateScriptBuilderPL().Build(file.Name, args[1]);
                    break;
                case "RO":
                    dirInfo = Directory.CreateDirectory(Path.Combine(@"D:\OneDrive - Business Consulting Center Sp. z o.o\Komputer Rosja-Rumunia\Rumunia\", DateTime.Now.ToString("dd.MM.yyyy")));
                    updateScript = new UpdateScriptBuilderRO().Build(file.Name, args[1]);
                    break;
                default:
                    dirInfo = Directory.CreateDirectory(Path.Combine(@"D:\OneDrive - Business Consulting Center Sp. z o.o\Komputer Rosja-Rumunia\Rosja\", DateTime.Now.ToString("dd.MM.yyyy")));
                    break;
            }


            var schema2 = File.ReadAllLines(@"D:\Projects\Selgros\PG\trunk\DBScripts\SelgrosPG_Schema2.sql").Skip(int.Parse(args[2]));

            File.WriteAllLines(Path.Combine(dirInfo.FullName, "SelgrosPG_Schema2.sql"),schema2);

            Console.WriteLine($"Copy file SelgrosPG_Schema2.sql to {dirInfo.FullName} successful");

            File.Copy(@"D:\Projects\Selgros\PG\trunk\DBScripts\SelgrosPG_Translations.sql", Path.Combine(dirInfo.FullName, "SelgrosPG_Translations.sql"),true );
            
            Console.WriteLine($"Copy file SelgrosPG_Translations.sql to {dirInfo.FullName} successful");

            File.Copy(file.FullName, Path.Combine(dirInfo.FullName, file.Name),true);

            Console.WriteLine($"Copy file {file.Name} to {dirInfo.FullName} successful");

            File.WriteAllText(Path.Combine(dirInfo.FullName, "update.bat"),updateScript);

            File.WriteAllText(Path.Combine(dirInfo.FullName, "appsettings.json"), "{\"CheckVersionProcess.ExcludeDirectories\": [\"App_Data\",\"UploadImages\" ]}");

            Console.WriteLine($"Update script generated successful");


        }
    }
}
