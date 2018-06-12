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

            sb.Append(@"D:\Przemek\ApplicationUpdater\ApplicationUpdater.exe ");
            sb.Append($"D:\\aktualizacje\\{fileName} ");
            sb.Append(@"D:\aktualizacje\ ");
            sb.Append(@"D:\inetpub\selgrospg ");
            sb.Append($"{version} ");
            sb.Append("false");

            return sb.ToString();
        }
    }
}
