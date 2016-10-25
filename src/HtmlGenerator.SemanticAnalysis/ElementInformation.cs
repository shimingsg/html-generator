using System.Collections.Generic;

namespace HtmlGenerator.SemanticAnalysis
{
    public class ElementInformation
    {
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
