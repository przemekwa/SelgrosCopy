using CommandLine;
using Microsoft.Extensions.Configuration;
using SelgrosCopy.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SelgrosCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            SelgorsCopyModel model = null;

            var result = Parser.Default.ParseArguments<CommandLineOptions>(args)
                    .WithParsed<CommandLineOptions>(o =>
                    {
                        model = o;
                        model.Configuration = new ConfigurationBuilder()
                                            .AddUserSecrets<UserSecret>()
                                            .Build();
                    })
                    .WithNotParsed(errorList =>
                    {
                        Environment.Exit(100);
                    });

            model.SchemaFilePath = configuration[nameof(model.SchemaFilePath)];
            model.PolandDestinationPath = configuration[nameof(model.PolandDestinationPath)];
            model.RomaniaDestinationPath = configuration[nameof(model.RomaniaDestinationPath)];
            model.RussiaDestinationPath = configuration[nameof(model.RussiaDestinationPath)];
            model.TranslationsFilePath = configuration[nameof(model.TranslationsFilePath)];
            model.ArtifactsZipPath = configuration[nameof(model.ArtifactsZipPath)];

            var exePath = Path.GetDirectoryName(System.Reflection
                  .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;

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
            if (args.Length >= v + 1)
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

                while ((key = Console.ReadKey(true).Key) != ConsoleKey.Escape)
                {

                    if (key == ConsoleKey.Y)
                    {
                        action(model);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("...successful");
                        break;
                    }
                    else if (key == ConsoleKey.N)
                    {

                        Console.WriteLine("...skip");
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
