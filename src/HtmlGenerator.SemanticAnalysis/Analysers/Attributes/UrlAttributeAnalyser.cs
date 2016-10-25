using System;

namespace HtmlGenerator.SemanticAnalysis.Analysers
{
    internal class UrlAttributeAnalyser : IAttributeAnalyser
    {
        public UriKind AllowedKind { get; set; }
        public bool AllowEmpty { get; set; }
        public bool AllowWhitespace { get; set; }

        public UrlAttributeAnalyser(UriKind allowedKind = UriKind.RelativeOrAbsolute, bool allowEmpty = false, bool allowWhitespace = true)
        {
            AllowedKind = allowedKind;
            AllowEmpty = allowEmpty;
            AllowWhitespace = allowWhitespace;
        }

        public bool IsValid(string name, string value)
        {
            if (AllowWhitespace)
            {
                value = value.Trim();
            }
            if (AllowEmpty && value.Length == 0)
            {
                return true;
            }

            Uri dummy;
            return Uri.TryCreate(value, AllowedKind, out dummy);
        }
    }
}
