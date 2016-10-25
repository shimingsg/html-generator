using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

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
                    Stream elementsStream = typeof(Sema).GetTypeInfo().Assembly.GetManifestResourceStream("HtmlGenerator.SemanticAnalysis.elements.json");

                    JsonSerializer serializer = new JsonSerializer();
                    using (StreamReader streamReader = new StreamReader(elementsStream))
                    using (JsonTextReader jsonTextReader = new JsonTextReader(streamReader))
                    {
                        s_elementInformation = serializer.Deserialize<Dictionary<string, ElementInformation>>(jsonTextReader);
                    }
                }
                return s_elementInformation;
            }
        }

        private static void RegisterElement(ElementInformation elementInformation)
        {
            s_elementInformation.Add(elementInformation.Tag, elementInformation);
        }
    }
}
