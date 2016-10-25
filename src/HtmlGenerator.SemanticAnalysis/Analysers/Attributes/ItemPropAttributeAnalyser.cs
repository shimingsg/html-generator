namespace HtmlGenerator.SemanticAnalysis.Analysers
{
    public class ItemPropAttributeAnalyser : IAttributeAnalyser
    {
        public bool IsValid(string name, string value)
        {
            // Names: the itemprop attribute
            if (value.Length == 0)
            {
                return false;
            }
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                if (c == '.' || c == ':')
                {
                    return false;
                }
            }
            return true;
        }
    }
}
