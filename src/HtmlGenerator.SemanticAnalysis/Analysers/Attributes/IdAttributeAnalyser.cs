namespace HtmlGenerator.SemanticAnalysis.Analysers
{
    public class IdAttributeAnalyser : IAttributeAnalyser
    {
        public bool IsValid(string name, string value)
        {
            if (value.Length == 0)
            {
                return false;
            }
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsWhiteSpace(value[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
