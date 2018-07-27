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
            sb.Append($"{fileName} ");
            sb.Append(@"D:\Przemek\Aktualizacje\ ");
            sb.Append(@"d:\inetpub\pcb\ ");
            sb.Append($"{version} ");
            sb.Append("false");

            return sb.ToString();
        }

        public string BuildTest(string fileName, string version)
        {
            throw new System.NotImplementedException();
        }
    }
}