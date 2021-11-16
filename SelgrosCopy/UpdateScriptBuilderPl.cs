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

            sb.Append(@"C:\_DEVFOLDER\ApplicationUpdater\ApplicationUpdater.exe ");
            sb.Append(@"--strategy Selgros ");
            sb.Append($"--zipFile {fileName} ");
            sb.Append(@"--backup C:\_DEVFOLDER\backup\ ");
            sb.Append(@"--inetpub C:\inetpub\wwwroot\pcb\ ");
            sb.Append($"--appversion {version} ");

            return sb.ToString();
        }

        public string BuildTest(string fileName, string version)
        {
            var sb = new StringBuilder();

            sb.Append(@"C:\_DEVFOLDER\ApplicationUpdater\ApplicationUpdater.exe ");
            sb.Append(@"--strategy Selgros ");
            sb.Append($"--zipFile {fileName} ");
            sb.Append(@"--backup C:\_DEVFOLDER\backup_test\ ");
            sb.Append(@"--inetpub C:\inetpub\wwwroot\pcb_test\ ");
            sb.Append($"--appversion {version} ");

            return sb.ToString();
        }
    }
}
