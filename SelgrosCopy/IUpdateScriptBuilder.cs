namespace SelgrosCopy
{
    public interface IUpdateScriptBuilder
    {
        string Build(string fileName, string version);
        string BuildTest(string fileName, string version);
    }
}