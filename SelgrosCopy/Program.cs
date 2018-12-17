using SelgrosCopy.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SelgrosCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!ValidateArgs(args))
            {
                return;
            }

            var model = new SelgorsCopyModel
            {
                Country = args[0],
                Version = args[1],
                LinesCut = args[2],
                LineEnd= args[3],
                RealeseNotes= GetNullParam(4, args, string.Empty),
                WebConfigNotes = GetNullParam(5, args, "Zmiana numeru wersji na {0}")
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
            Console.WriteLine(@"|_______/    |_______||_______| \______| | _| `._____| \______/  |_______/   ");
            Console.WriteLine();


            MakeStep(Steps.GetArtifacts, model);
            MakeStep(Steps.CreateDir, model);
            MakeStep(Steps.CreateSchema2, model);
            MakeStep(Steps.CreateTranslations, model);
            MakeStep(Steps.CopyApp, model);
            MakeStep(Steps.CreateUpdateScript, model);
            MakeStep(Steps.CreateUpdateScriptTestEnv, model);
            MakeStep(Steps.CreateAppsettings, model);
            MakeStepWithQuestion(Steps.CreatePage, model);
            MakeStep(Steps.Stop, model);
        }

        private static string GetNullParam(int v, string[] args, string @default)
        {
            if (args.Length >= v+1)
            {
                return args[v];
            }
            else
            {
                return @default;
            }
        }

        private static void MakeStepWithQuestion(Action<SelgorsCopyModel> action, SelgorsCopyModel model)
        {
            try
            {
                var q = $"Do you want to continue with  {action.Method.Name } step [Y/n]?";

                Console.Write(q);

                
                ConsoleKey key = ConsoleKey.Escape;

                while((key = Console.ReadKey(true).Key) != ConsoleKey.Escape )
                {

                    if (key == ConsoleKey.Y)
                    {
                        action(model);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("...successful");
                    }
                    else if (key == ConsoleKey.N)
                    {
                        break;
                    }
                }

            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("...FAILD");
                //Console.WriteLine(e);
                throw;
            }
            finally
            {
                Console.ResetColor();
            }
        }

        private static bool ValidateArgs(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Param 1: country");
                Console.WriteLine("Param 2: version");
                Console.WriteLine("Param 2: line number");
                Console.WriteLine("Param 3: new line number");
                Console.WriteLine("Param 4: realese notes");
                Console.WriteLine("Param 5: web config notes");

                return false;
            }

            return true;
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
                //Console.WriteLine(e);
                throw;
            }
            finally
            {
                Console.ResetColor();
            }
        }
    }
}
