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

            sb.Append(@"d:\Przemek\ApplicationUpdate\ApplicationUpdater.exe ");
            sb.Append($"{fileName} ");
            sb.Append(@"D:\Przemek\Aktualizacje\ ");
            sb.Append(@"d:\inetpub\SelgrosPG\ ");
            sb.Append($"{version} ");
            sb.Append("false");

            return sb.ToString();
        }
    }
}
