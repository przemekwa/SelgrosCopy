using System;
using System.Collections.Generic;
using System.Text;

namespace SelgrosCopy
{
    public class UpdateScriptBuilderRO : IUpdateScriptBuilder
    {
        public UpdateScriptBuilderRO()
        {

        }

        public string Build(string fileName, string version)
        {
            var sb = new StringBuilder();

            sb.Append(@"d:\_DEVFOLDERS\ApplicationUpdater\ApplicationUpdater.exe ");
            sb.Append(@"--strategy Selgros ");
            sb.Append($"--zipFile {fileName} ");
            sb.Append(@"--backup D:\Przemek\Aktualizacje\ ");
            sb.Append(@"--inetpub d:\inetpub\SelgrosPG\ ");
            sb.Append($"--appversion {version} ");

            return sb.ToString();
        }

        public string BuildTest(string fileName, string version)
        {
           var sb = new StringBuilder();

            sb.Append(@"d:\_DEVFOLDERS\ApplicationUpdater\ApplicationUpdater.exe ");
            sb.Append(@"--strategy Selgros ");
            sb.Append($"--zipFile {fileName} ");
            sb.Append(@"--backup D:\Przemek\Aktualizacje-Test\ ");
            sb.Append(@"--inetpub d:\inetpub\SelgrosPG_Test\ ");
            sb.Append($"--appversion {version} ");

            return sb.ToString();
        }
    }
}
