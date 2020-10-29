using System;
using System.Collections.Generic;
using System.Text;

namespace SelgrosCopy
{
    public class UpdateScriptBuilderPL : IUpdateScriptBuilder
    {
        public string Build(string fileName, string version)
        {
            var sb = new StringBuilder();

            sb.Append(@"D:\_DEV_FOLDER\ApplicationUpdater\ApplicationUpdater.exe ");
            sb.Append(@"--strategy Selgros ");
            sb.Append($"--zipFile {fileName} ");
            sb.Append(@"--backup D:\_DEV_FOLDER\backup\ ");
            sb.Append(@"--inetpub D:\inetpub\selgrospg\ ");
            sb.Append($"--appversion {version} ");

            return sb.ToString();
        }

        public string BuildTest(string fileName, string version)
        {
            var sb = new StringBuilder();

            sb.Append(@"D:\_DEV_FOLDER\ApplicationUpdater\ApplicationUpdater.exe ");
            sb.Append(@"--strategy Selgros ");
            sb.Append($"--zipFile {fileName} ");
            sb.Append(@"--backup D:\_DEV_FOLDER\backup_test\ ");
            sb.Append(@"--inetpub D:\inetpub\selgrospg_test\ ");
            sb.Append($"--appversion {version} ");

            return sb.ToString();
        }
    }
}
