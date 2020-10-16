using System.Text;

namespace SelgrosCopy
{
    public class UpdateScriptBuilderRU :IUpdateScriptBuilder
    {
        public UpdateScriptBuilderRU()
        {
        }

        public string Build(string fileName, string version)
        {
            var sb = new StringBuilder();

            sb.Append(@"d:\Przemek\ApplicationUpdate\ApplicationUpdater.exe ");
            sb.Append(@"--strategy Selgros ");
            sb.Append($"--zipFile {fileName} ");
            sb.Append(@"--backup D:\Przemek\Aktualizacje\ ");
            sb.Append(@"--inetpub d:\inetpub\wwwroot\pcb ");
            sb.Append($"--appversion {version} ");
            sb.Append("--undo false");

            return sb.ToString();
        }

        public string BuildTest(string fileName, string version)
        {
           var sb = new StringBuilder();

             sb.Append(@"d:\Przemek\ApplicationUpdate\ApplicationUpdater.exe ");
            sb.Append(@"--strategy Selgros ");
            sb.Append($"--zipFile {fileName} ");
            sb.Append(@"--backup D:\Przemek\Aktualizacje-Test\ ");
            sb.Append(@"--inetpub d:\inetpub\pcb_test\ ");
            sb.Append($"--appversion {version} ");
            sb.Append("--undo false");

            return sb.ToString();
        }
    }
}