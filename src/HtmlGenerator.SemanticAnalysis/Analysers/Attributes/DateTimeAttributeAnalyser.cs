using System;

namespace HtmlGenerator.SemanticAnalysis.Analysers
{
    public class DateTimeAttributeAnalyser : IAttributeAnalyser
    {
        public bool IsValid(string name, string value)
        {
            // 2.4.5. Dates and times
            DateTime result;
            return DateTime.TryParse(value, out result);
        }
    }
}
