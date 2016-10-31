using System.Collections.Generic;

namespace HtmlGenerator.SemanticAnalysis
{
    public static partial class Sema
    {
        public static ElementInformation GetElementInformation(string tag)
        {
            Requires.NotNullOrWhitespace(tag, nameof(tag));
            ElementInformation value;
            ElementInformationDictionary.TryGetValue(tag, out value);
            return value;
        }

        private static Dictionary<string, ElementInformation> s_elementInformation;
        private static Dictionary<string, ElementInformation> ElementInformationDictionary
        {
            get
            {
                if (s_elementInformation == null)
                {
                    s_elementInformation = new Dictionary<string, ElementInformation>();
                    InitializeElementInformation();
                }
                return s_elementInformation;
            }
        }

        private static void RegisterElement(ElementInformation elementInformation)
        {
            s_elementInformation.Add(elementInformation.Tag, elementInformation);
        }

        private static void InitializeElementInformation()
        {
            // 4.5.1 The a element
            RegisterElement(new ElementInformation(
                tag: "a",
                category: ElementCategory.Flow | ElementCategory.Phrasing | ElementCategory.Interactive | ElementCategory.Palpable,
                parentCategory: ElementCategory.Phrasing,
                childrenCategory: ElementCategory.Phrasing,
                requiredAttributes: new string[] { "href" },
                attributes: new string[] { "href", "target", "download", "rel", "rev", "hreflang", "type" },
                ariaRoleAttributes: new string[] { "link", "button", "checkbox", "menuitem", "menuitemcheckbox", "menuitemradio", "radio", "switch", "tab", "treeitem" }
            ));

            // 4.5.9 The abbr element
            RegisterElement(new ElementInformation(
                tag: "abbr",
                category: ElementCategory.Flow | ElementCategory.Phrasing | ElementCategory.Palpable,
                parentCategory: ElementCategory.Phrasing,
                childrenCategory: ElementCategory.Phrasing
            ));

            // 4.3.10 The address element
            RegisterElement(new ElementInformation(
                tag: "address",
                category: ElementCategory.Flow | ElementCategory.Palpable,
                parentCategory: ElementCategory.Flow,
                childrenCategory: ElementCategory.Flow
            ));

            // 4.8.14 The area element
            RegisterElement(new ElementInformation(
                tag: "area",
                category: ElementCategory.Flow | ElementCategory.Palpable,
                parentCategory: ElementCategory.Flow,
                childrenCategory: ElementCategory.Flow,
                attributes: new string[] { "alt", "coords", "shape", "href", "target", "download", "ping", "rel", "referrerpolicy" }
            ));

            // 4.3.2 The article element
            RegisterElement(new ElementInformation(
                tag: "article",
                category: ElementCategory.Flow | ElementCategory.Sectioning | ElementCategory.Palpable,
                parentCategory: ElementCategory.Flow,
                childrenCategory: ElementCategory.Flow
            ));

            // TODO: add
            // 4.3.5 The aside element
            RegisterElement(new ElementInformation(
                tag: "aside",
                category: ElementCategory.Flow | ElementCategory.Sectioning | ElementCategory.Palpable,
                parentCategory: ElementCategory.Flow,
                childrenCategory: ElementCategory.Flow
            ));

            // 4.8.10 The audio element
            RegisterElement(new ElementInformation(
                tag: "audio",
                category: ElementCategory.Flow | ElementCategory.Phrasing | ElementCategory.Embedded | ElementCategory.Interactive | ElementCategory.Palpable,
                parentCategory: ElementCategory.Phrasing,
                childrenCategory: ElementCategory.Flow
            ));

            // 4.5.21 The b element
            RegisterElement(new ElementInformation(
                tag: "b",
                category: ElementCategory.Flow | ElementCategory.Phrasing | ElementCategory.Palpable,
                parentCategory: ElementCategory.Phrasing,
                childrenCategory: ElementCategory.Flow
            ));

            // 4.2.3 The base element
            RegisterElement(new ElementInformation(
                tag: "base",
                category: ElementCategory.Metadata,
                parentCategory: ElementCategory.Flow, // TODO: Head
                childrenCategory: ElementCategory.None,
                attributes: new string[] { "href", "target" }
            ));

            // 4.5.24 The bdi element
            RegisterElement(new ElementInformation(
                tag: "bdi",
                category: ElementCategory.Flow | ElementCategory.Phrasing | ElementCategory.Palpable,
                parentCategory: ElementCategory.Phrasing,
                childrenCategory: ElementCategory.Phrasing
            ));

            // TODO: add
            // 4.5.25 The bdo element
            RegisterElement(new ElementInformation(
                tag: "bdo",
                category: ElementCategory.Flow | ElementCategory.Phrasing | ElementCategory.Palpable,
                parentCategory: ElementCategory.Phrasing,
                childrenCategory: ElementCategory.Phrasing
            ));

            // TODO: add
            // 4.4.4 The blockquote element
            RegisterElement(new ElementInformation(
                tag: "blockquote",
                category: ElementCategory.Flow | ElementCategory.SectioningRoot | ElementCategory.Palpable,
                parentCategory: ElementCategory.Flow,
                childrenCategory: ElementCategory.Flow,
                attributes: new string[] { "cite" }
            ));

            // 4.3.1 The body element
            RegisterElement(new ElementInformation(
                tag: "body",
                category: ElementCategory.SectioningRoot,
                parentCategory: ElementCategory.Phrasing, // TODO: html
                childrenCategory: ElementCategory.Flow,
                attributes: new string[] { "onafterprint", "onbeforeprint", "onebeforeunload", "onhashchange", "onlanguagechange", "onmessage", "onoffline", "ononline", "onpagehide", "onpageshow", "onpopstate", "onrejectionhandled", "onstorage", "onunhandledrejection", "onunload" }
            ));

            // 4.5.27 The br element
            RegisterElement(new ElementInformation(
                tag: "br",
                category: ElementCategory.Flow | ElementCategory.Phrasing,
                parentCategory: ElementCategory.Phrasing,
                childrenCategory: ElementCategory.None,
                attributes: new string[] { "clear" },
                isVoid: true
            ));

            // 4.10.6 The button element
            RegisterElement(new ElementInformation(
                tag: "button",
                category: ElementCategory.Flow | ElementCategory.Phrasing | ElementCategory.Interactive | ElementCategory.Listed | ElementCategory.Labelable | ElementCategory.Submittable | ElementCategory.FormAssociated | ElementCategory.Palpable,
                parentCategory: ElementCategory.Phrasing,
                childrenCategory: ElementCategory.Phrasing,
                attributes: new string[] { "autofocus", "disabled", "form", "formaction", "formenctype", "formmethod", "formnovalidate", "formtarget", "menu", "name", "type", "value" }
            ));

            // 4.12.5 The canvas element
            RegisterElement(new ElementInformation(
                tag: "canvas",
                category: ElementCategory.Flow | ElementCategory.Phrasing | ElementCategory.Embedded | ElementCategory.Palpable,
                parentCategory: ElementCategory.Phrasing,
                childrenCategory: ElementCategory.Flow,
                attributes: new string[] { "width", "height" }
            ));

            // 4.9.2 The caption element
            RegisterElement(new ElementInformation(
                tag: "caption",
                category: ElementCategory.None,
                parentCategory: ElementCategory.Phrasing, // TODO: table
                childrenCategory: ElementCategory.Flow
            ));

            // 4.5.6 The cite element
            RegisterElement(new ElementInformation(
                tag: "cite",
                category: ElementCategory.Flow | ElementCategory.Phrasing | ElementCategory.Palpable,
                parentCategory: ElementCategory.Phrasing,
                childrenCategory: ElementCategory.Phrasing
            ));

            // 4.5.15 The code element
            RegisterElement(new ElementInformation(
                tag: "code",
                category: ElementCategory.Flow | ElementCategory.Phrasing | ElementCategory.Palpable,
                parentCategory: ElementCategory.Phrasing,
                childrenCategory: ElementCategory.Phrasing
            ));

            // 4.9.4 The col element
            RegisterElement(new ElementInformation(
                tag: "col",
                category: ElementCategory.None,
                parentCategory: ElementCategory.Phrasing, // TODO: colgroup
                childrenCategory: ElementCategory.None,
                attributes: new string[] { "span" },
                isVoid: true
            ));

            // 4.9.3 The colgroup element
            RegisterElement(new ElementInformation(
                tag: "colgroup",
                category: ElementCategory.None,
                parentCategory: ElementCategory.Phrasing, // TODO: table
                childrenCategory: ElementCategory.Phrasing, // TODO: col, template
                attributes: new string[] { "span" }
            ));

            // 4.5.13 The data element
            RegisterElement(new ElementInformation(
                tag: "data",
                category: ElementCategory.Flow | ElementCategory.Phrasing | ElementCategory.Palpable,
                parentCategory: ElementCategory.Phrasing,
                childrenCategory: ElementCategory.Phrasing,
                attributes: new string[] { "value" }
            ));

            // 4.10.8 The datalist element
            RegisterElement(new ElementInformation(
                tag: "datalist",
                category: ElementCategory.Flow | ElementCategory.Phrasing,
                parentCategory: ElementCategory.Phrasing,
                childrenCategory: ElementCategory.Phrasing | ElementCategory.ScriptSupporting // TODO: option
            ));

            // 4.4.10 The dd element
            RegisterElement(new ElementInformation(
                tag: "dd",
                category: ElementCategory.None,
                parentCategory: ElementCategory.Phrasing, // TODO: dl, div
                childrenCategory: ElementCategory.Flow
            ));

            // 4.7.2 The del element
            RegisterElement(new ElementInformation(
                tag: "del",
                category: ElementCategory.Flow | ElementCategory.Phrasing,
                parentCategory: ElementCategory.Phrasing,
                childrenCategory: ElementCategory.Flow,
                attributes: new string[] { "cite", "datetime" }
            ));

            // 4.11.1 The details element
            RegisterElement(new ElementInformation(
                tag: "details",
                category: ElementCategory.Flow | ElementCategory.SectioningRoot | ElementCategory.Interactive | ElementCategory.Palpable,
                parentCategory: ElementCategory.Flow,
                childrenCategory: ElementCategory.Flow, // TODO: summary
                attributes: new string[] { "open" }
            ));

            // 4.5.8 The dfn element
            RegisterElement(new ElementInformation(
                tag: "dfn",
                category: ElementCategory.Flow | ElementCategory.Phrasing | ElementCategory.Palpable,
                parentCategory: ElementCategory.Phrasing,
                childrenCategory: ElementCategory.Phrasing
            ));

            // 4.11.7 The dialog element
            RegisterElement(new ElementInformation(
                tag: "dialog",
                category: ElementCategory.Flow | ElementCategory.SectioningRoot,
                parentCategory: ElementCategory.Flow,
                childrenCategory: ElementCategory.Flow,
                attributes: new string[] { "open" }
            ));

            // 4.4.14 The div element
            RegisterElement(new ElementInformation(
                tag: "div",
                category: ElementCategory.Flow | ElementCategory.Palpable,
                parentCategory: ElementCategory.Flow, // TODO: dl
                childrenCategory: ElementCategory.Flow
            ));

            // 4.4.8 The dl element
            RegisterElement(new ElementInformation(
                tag: "dl",
                category: ElementCategory.Flow | ElementCategory.Palpable,
                parentCategory: ElementCategory.Flow,
                childrenCategory: ElementCategory.ScriptSupporting // TODO: dt, dd, div
            ));

            // 4.4.9 The dt element
            RegisterElement(new ElementInformation(
                tag: "dt",
                category: ElementCategory.None,
                parentCategory: ElementCategory.Flow, // TODO: div, dl
                childrenCategory: ElementCategory.Flow
            ));
        }
    }
}
