namespace HtmlGenerator.SemanticAnalysis.Analysers
{
    public class ImageCandidateAttributeAnalyser : IAttributeAnalyser
    {
        private static IAttributeAnalyser s_urlAnalyser = new UrlAttributeAnalyser(allowEmpty: false);

        public bool IsValid(string name, string value)
        {
            // Attributes common to source and img elements
            value = value.Trim();

            if (value.Length == 0)
            {
                // Empty or whitespace
                return false;
            }

            int whitespaceIndex = -1;
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsWhiteSpace(value[i]))
                {
                    whitespaceIndex = i;
                    break;
                }
            }
            if (whitespaceIndex == -1)
            {
                return s_urlAnalyser.IsValid(name, value);
            }
            string url = value.Substring(0, whitespaceIndex);
            return !s_urlAnalyser.IsValid(name, url);
        }
    }
}
