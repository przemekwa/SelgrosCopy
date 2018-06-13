using SelgrosCopy.Model;
using System;
using System.IO;
using System.Linq;

namespace SelgrosCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new SelgorsCopyModel
            {
                Country = args[0],
                Version = args[1],
                LinesCut = args[2]
            };


            Console.WriteLine(@"                    ______   ______   .______   ____    ____                  ");
            Console.WriteLine(@"                   /      | /  __  \  |   _  \  \   \  /   /                  ");
            Console.WriteLine(@"                  |  ,----'|  |  |  | |  |_)  |  \   \/   /                   ");
            Console.WriteLine(@"                  |  |     |  |  |  | |   ___/    \_    _/                    ");
            Console.WriteLine(@"                  |  `----.|  `--'  | |  |          |  |                      ");
            Console.WriteLine(@"                  \______ | \______/  | _|          |__|                      ");
            Console.WriteLine(@"     _______. _______  __        _______ .______        ______        _______.");
            Console.WriteLine(@"    /       ||   ____||  |      /  _____||   _  \      /  __  \      /       |");
            Console.WriteLine(@"   |   (----`|  |__   |  |     |  |  __  |  |_)  |    |  |  |  |    |   (----`");
            Console.WriteLine(@"    \   \    |   __|  |  |     |  | |_ | |      /     |  |  |  |     \   \    ");
            Console.WriteLine(@".----)   |   |  |____ |  `----.|  |__| | |  |\  \----.|  `--'  | .----)   |   ");
            Console.WriteLine(@"| _______/    |_______||_______| \______| | _| `._____| \______/  |_______/   ");
            Console.WriteLine();






            model.File = new DirectoryInfo(Path.Combine(@"D:\Pobrane\"))
               .GetFiles("Selgros_PG_SPG_QAS_PL_*")
               .OrderBy(s => s.CreationTime)
               .Last();

            switch (model.Country)
            {
                case "PL":
                    model.DirInfo = Directory.CreateDirectory(Path.Combine(@"D:\Selgros-Aktualizacja", DateTime.Now.ToString("dd.MM.yyyy")));
                    model.UpdateScript = new UpdateScriptBuilderPL().Build(model.File.Name, model.Version);
                    break;
                case "RO":
                    model.DirInfo = Directory.CreateDirectory(Path.Combine(@"D:\OneDrive - Business Consulting Center Sp. z o.o\Komputer Rosja-Rumunia\Rumunia\", DateTime.Now.ToString("dd.MM.yyyy")));
                    model.UpdateScript = new UpdateScriptBuilderRO().Build(model.File.Name, model.Version);
                    break;
                default:
                    model.DirInfo = Directory.CreateDirectory(Path.Combine(@"D:\OneDrive - Business Consulting Center Sp. z o.o\Komputer Rosja-Rumunia\Rosja\", DateTime.Now.ToString("dd.MM.yyyy")));
                    break;
            }

            MakeStep(Steps.CreateSchema2, model);
            MakeStep(Steps.CreateTranslations, model);
            MakeStep(Steps.CopyApp, model);
            MakeStep(Steps.CreateUpdateScript, model);
            MakeStep(Steps.CreateAppsettings, model);
            MakeStep(Steps.Stop, model);
        }

        private static void MakeStep(Action<SelgorsCopyModel> action, SelgorsCopyModel model)
        {
            try
            {
                action(model);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("...successful");

            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("...FAILD");
            }
            finally
            {
                Console.ResetColor();
            }
        }
    }
}
