using System.Collections.Generic;
using System.Linq;

namespace HtmlGenerator.SemanticAnalysis
{
    public class ElementInformation
    {
        internal ElementInformation(string tag,
            ElementCategory category, ElementCategory parentCategory, ElementCategory childrenCategory,
            IEnumerable<string> requiredAttributes = null, IEnumerable<string> requiredElements = null,
            IEnumerable<string> attributes = null, IEnumerable<string> ariaRoleAttributes = null,
            bool isVoid = false)
        {
            Tag = tag;

            Category = category;
            ParentCategory = parentCategory;
            ChildrenCategory = childrenCategory;

            RequiredAttributes = requiredAttributes ?? Enumerable.Empty<string>();
            RequiredElements = requiredElements ?? Enumerable.Empty<string>();

            Attributes = attributes ?? Enumerable.Empty<string>();
            AriaRoleAttributes = ariaRoleAttributes ?? Enumerable.Empty<string>();

            IsVoid = isVoid;
        }

        public string Tag { get; set; }

        public ElementCategory Category { get; set; }
        public ElementCategory ParentCategory { get; set; }
        public ElementCategory ChildrenCategory { get; set; }

        public IEnumerable<string> RequiredAttributes { get; set; }
        public IEnumerable<string> RequiredElements { get; set; }
        
        public IEnumerable<string> Attributes { get; set; }
        public IEnumerable<string> AriaRoleAttributes { get; set; }

        public bool IsVoid { get; set; }
    }
}
