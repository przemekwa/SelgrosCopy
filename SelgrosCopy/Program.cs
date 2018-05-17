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

            switch (args[0])
            {
                case "RO":
                    dirInfo = Directory.CreateDirectory(Path.Combine(@"D:\OneDrive - Business Consulting Center Sp. z o.o\Komputer Rosja-Rumunia\Rumunia\", DateTime.Now.ToString("dd.MM.yyyy")));
                    break;
                default:
                    dirInfo = Directory.CreateDirectory(Path.Combine(@"D:\OneDrive - Business Consulting Center Sp. z o.o\Komputer Rosja-Rumunia\Rosja\", DateTime.Now.ToString("dd.MM.yyyy")));
                    break;
            }


            File.Copy(@"D:\Projects\Selgros\PG\trunk\DBScripts\SelgrosPG_Schema2.sql", Path.Combine(dirInfo.FullName, "SelgrosPG_Schema2.sql") );

            Console.WriteLine($"Copy file SelgrosPG_Schema2.sql to {dirInfo.FullName} successful");

            File.Copy(@"D:\Projects\Selgros\PG\trunk\DBScripts\SelgrosPG_Translations.sql", Path.Combine(dirInfo.FullName, "SelgrosPG_Translations.sql") );
            
            Console.WriteLine($"Copy file SelgrosPG_Translations.sql to {dirInfo.FullName} successful");

            File.Copy(file.FullName, Path.Combine(dirInfo.FullName, file.Name));


            Console.WriteLine($"Copy file {file.Name} to {dirInfo.FullName} successful");

        }
    }
}
