﻿using SelgrosCopy.Model;
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

            Steps.CreatePage(null);
            return;

            if (!ValidateArgs(args))
            {
                return;
            }

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


            MakeStep(Steps.GetArtifacts, model);
            MakeStep(Steps.CreateDir, model);
            MakeStep(Steps.CreateSchema2, model);
            MakeStep(Steps.CreateTranslations, model);
            MakeStep(Steps.CopyApp, model);
            MakeStep(Steps.CreateUpdateScript, model);
            MakeStep(Steps.CreateUpdateScriptTestEnv, model);
            MakeStep(Steps.CreateAppsettings, model);
            MakeStep(Steps.Stop, model);
        }

        private static bool ValidateArgs(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Param 1: country");
                Console.WriteLine("Param 2: version");
                Console.WriteLine("Param 2: line number");

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
