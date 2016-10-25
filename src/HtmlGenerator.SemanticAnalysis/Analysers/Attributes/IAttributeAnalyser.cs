namespace HtmlGenerator.SemanticAnalysis
{
    public interface IAttributeAnalyser
    {
        bool IsValid(string name, string value);
    }
}
