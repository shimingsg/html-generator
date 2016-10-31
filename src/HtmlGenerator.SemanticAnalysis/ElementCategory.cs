namespace HtmlGenerator.SemanticAnalysis
{
    public enum ElementCategory
    {
        None = 0,
        Metadata = 1,
        Interactive = 2,
        Heading = 4,
        Palpable = 8,
        Sectioning = 16,
        Phrasing = 32,
        Flow = 64,
        Embedded = 128,
        SectioningRoot = 256,
        Listed = 512,
        Labelable = 1024,
        Submittable = 2048,
        FormAssociated = 4096,
        ScriptSupporting = 8192
    }
}
