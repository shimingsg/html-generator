namespace HtmlGenerator.SemanticAnalysis.Analysers
{
    public class FloatingPointAttributeAnalyser : IAttributeAnalyser
    {
        public bool AllowNegative { get; set; }
        public bool AllowZero { get; set; }

        public FloatingPointAttributeAnalyser(bool allowNegative, bool allowZero)
        {
            AllowNegative = allowNegative;
            AllowZero = allowZero;
        }

        public bool IsValid(string name, string value)
        {
            // 2.4.4.3 Floating-point numbers
            float result;
            if (!float.TryParse(value, out result))
            {
                return false;
            }
            if (result == 0 && !AllowZero)
            {
                return false;
            }
            if (result < 0 && !AllowNegative)
            {
                return false;
            }
            return true;
        }
    }
}
