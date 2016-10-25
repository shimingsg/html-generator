namespace HtmlGenerator.SemanticAnalysis
{
    public enum ElementCategory
    {
        MetadataContent = 1,
        Interactive = 2,
        HeadingContent = 4,
        Palpable = 8,
        SectioningContent = 16,
        Phrasing = 32,
        Flow = 64,
    }
}
