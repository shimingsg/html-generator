namespace HtmlGenerator.SemanticAnalysis.Analysers
{
    public class CssDeclarationsAttributeAnalyser : IAttributeAnalyser
    {
        public bool IsValid(string name, string value)
        {
            return new TextAttributeAnalyser(minLength: 0, maxLength: -1).IsValid(name, value);
        }
    }
}
