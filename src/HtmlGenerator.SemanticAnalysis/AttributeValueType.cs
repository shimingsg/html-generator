﻿namespace HtmlGenerator.SemanticAnalysis
{
    public enum AttributeValueType
    {
        Boolean,
        Enumerated,
        Integer,
        PositiveInteger,
        NonNegativeInteger,
        Url,
        NonEmptyUrl,
        Text,
        NonEmptyText,

        IdSpaceDelimitedArray,
        SpaceDelimitedArray,
        AsciiCompatibleEncodingSpaceDelimitedArray,
        UniqueOneUnicodeCodePointLengthSpaceDelimitedArray,
        DropzoneSpaceDelimitedArray,
        UniqueAbsoluteUrlSpaceDelimitedArray,
        NonEmptyUrlSpaceDelimitedArray,
        EnumeratedCaseInsensitiveSpaceDelimitedArray,
        SizeCaseInsensitiveSpaceDelimitedArray,
        MimeTypeCommaDelimitedArray,
        FloatingPointCommaDelimitedArray,
        ImageCandidateCommaDelimitedArray,
        FloatingPointNumber,
        Color,
        EncodingLabel,
        Id,
        DateTime,
        BrowsingContextNameOrKeyword,
        Bcp47LanguageTag,
        Bcp47LanguageTagOrEmpty,
        MediaQueryList,
        RegularExpression,
        Html,
        PositiveFloatingPointNumberOrAny,
        ContextSensitive,
        HashNameReference,
        UniqueSpaceSeparatedCaseSensitiveAbsoluteUrlsDefinedPropertyNamesOrTextSpaceDelimitedArray,
        CssDeclarations
    }
}
