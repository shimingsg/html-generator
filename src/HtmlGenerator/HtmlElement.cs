﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlGenerator
{
    public class HtmlElement : SerializableHtmlObject
    {
        internal HtmlObjectLinkedList<HtmlElement> _elements = new HtmlObjectLinkedList<HtmlElement>();
        internal HtmlObjectLinkedList<HtmlAttribute> _attributes = new HtmlObjectLinkedList<HtmlAttribute>();

        public HtmlElement(string tag)
        {
            Requires.NotNullOrWhitespace(tag, nameof(tag));
            Tag = tag;
        }

        public HtmlElement(string tag, bool isVoid) : this(tag)
        {
            IsVoid = isVoid;
        }

        public HtmlElement(string tag, string innerText) : this(tag)
        {
            InnerText = innerText;
        }

        public HtmlElement(string tag, string innerText, params HtmlObject[] content) : this(tag, innerText)
        {
            Add(content);
        }

        public HtmlElement(string tag, params HtmlObject[] content) : this(tag)
        {
            Add(content);
        }

        public override HtmlObjectType ObjectType => HtmlObjectType.Element;

        public void Add(HtmlObject content)
        {
            Requires.NotNull(content, nameof(content));
            if (content.Parent == this)
            {
                throw new InvalidOperationException("Cannot have a duplicate element or attribute");
            }

            if (content.ObjectType == HtmlObjectType.Element)
            {
                if (content == this)
                {
                    throw new InvalidOperationException("Cannot add an object as a child to itself.");
                }
                ThrowIfVoid();
                AddElement((HtmlElement)content);
            }
            else
            {
                AddAttribute((HtmlAttribute)content);
            }
        }
        
        public void AddFirst(params HtmlObject[] content) => AddFirst((IEnumerable<HtmlObject>)content);

        public void AddFirst(IEnumerable<HtmlObject> content)
        {
            Requires.NotNull(content, nameof(content));
            foreach (HtmlObject obj in content)
            {
                AddFirst(obj);
            }
        }

        private void AddAttribute(HtmlAttribute attribute)
        {
            attribute.RemoveFromParent();
            attribute.Parent = this;
            _attributes.AddLast(attribute);
        }

        public void AddFirst(HtmlObject content)
        {
            Requires.NotNull(content, nameof(content));
            if (content.Parent == this)
            {
                throw new InvalidOperationException("Cannot have a duplicate element or attribute");
            }

            if (content.ObjectType == HtmlObjectType.Element)
            {
                if (content == this)
                {
                    throw new InvalidOperationException("Cannot add an object as a child to itself.");
                }
                ThrowIfVoid();
                AddElementFirst((HtmlElement)content);
            }
            else
            {
                AddAttributeFirst((HtmlAttribute)content);
            }
        }

        public void Add(params HtmlObject[] content) => Add((IEnumerable<HtmlObject>)content);

        public void Add(IEnumerable<HtmlObject> content)
        {
            Requires.NotNull(content, nameof(content));
            foreach (HtmlObject obj in content)
            {
                Add(obj);
            }
        }

        private void AddElementFirst(HtmlElement element)
        {
            element.RemoveFromParent();
            element.Parent = this;
            _elements.AddFirst(element);
        }

        private void AddAttributeFirst(HtmlAttribute attribute)
        {
            attribute.RemoveFromParent();
            attribute.Parent = this;
            _attributes.AddFirst(attribute);
        }

        public void ReplaceAll(params HtmlObject[] content) => ReplaceAll((IEnumerable<HtmlObject>)content);

        public void ReplaceAll(IEnumerable<HtmlObject> content)
        {
            Requires.NotNull(content, nameof(content));

            _elements.Clear();
            _attributes.Clear();
            foreach (HtmlObject obj in content)
            {
                Add(obj);
            }
        }

        public void ReplaceAttributes(params HtmlAttribute[] attributes) => ReplaceAttributes((IEnumerable<HtmlAttribute>)attributes);

        public void ReplaceAttributes(IEnumerable<HtmlAttribute> attributes)
        {
            Requires.NotNull(attributes, nameof(attributes));

            _attributes.Clear();
            foreach (HtmlAttribute attribute in attributes)
            {
                Add(attribute);
            }
        }

        public void ReplaceElements(params HtmlElement[] elements) => ReplaceElements((IEnumerable<HtmlElement>)elements);

        public void ReplaceElements(IEnumerable<HtmlElement> elements)
        {
            Requires.NotNull(elements, nameof(elements));
            ThrowIfVoid();

            _elements.Clear();
            foreach (HtmlElement element in elements)
            {
                Add(element);
            }
        }

        public void RemoveAll()
        {
            ThrowIfVoid();
            _elements.Clear();
            _attributes.Clear();
        }

        public void RemoveElements()
        {
            ThrowIfVoid();
            _elements.Clear();
        }

        public void RemoveAttributes()
        {
            ThrowIfVoid();
            _attributes.Clear();
        }

        public void RemoveFromParent()
        {
            if (Parent == null)
            {
                return;
            }
            Parent._elements.Remove(this);
            Parent = null;
        }

        public string Tag { get; }
        public bool IsVoid { get; }
        public string InnerText { get; private set; }

        public void SetInnerText(string value)
        {
            ThrowIfVoid();
            InnerText = value;
        }

        public HtmlElement FirstElement => _elements._first;
        public HtmlElement LastElement => _elements._last;

        public bool HasElements => _elements._count != 0;

        public IEnumerable<HtmlElement> Elements() => Elements(null);

        public IEnumerable<HtmlElement> Elements(string tag)
        {
            bool isDefaultTag = string.IsNullOrEmpty(tag);

            HtmlElement element = _elements._first;
            while (element != null)
            {
                if (isDefaultTag || element.Tag == tag)
                {
                    yield return element;
                }
                element = (HtmlElement)element._next;
            }
        }

        public HtmlAttribute FirstAttribute => _attributes._first;
        public HtmlAttribute LastAttribute => _attributes._last;

        public bool HasAttributes => _attributes._count != 0;

        public IEnumerable<HtmlAttribute> Attributes()
        {
            HtmlAttribute attributeNode = _attributes._first;
            while (attributeNode != null)
            {
                yield return attributeNode;
                attributeNode = (HtmlAttribute)attributeNode._next;
            }
        }

        public bool IsEmpty => !HasElements && !HasAttributes;

        public IEnumerable<HtmlObject> ElementsAndAttributes()
        {
            foreach (HtmlElement element in Elements())
            {
                yield return element;
            }
            foreach (HtmlAttribute attribute in Attributes())
            {
                yield return attribute;
            }
        }

        public bool TryGetElement(string tag, out HtmlElement element)
        {
            Requires.NotNullOrWhitespace(tag, nameof(tag));

            HtmlElement current = _elements._first;
            while (current != null)
            {
                if (current.Tag == tag)
                {
                    element = current;
                    return true;
                }
                current = (HtmlElement)current._next;
            }

            element = null;
            return false;
        }

        public bool TryGetAttribute(string name, out HtmlAttribute attribute)
        {
            Requires.NotNullOrWhitespace(name, nameof(name));

            HtmlAttribute current = _attributes._first;
            while (current != null)
            {
                if (current.Name == name)
                {
                    attribute = current;
                    return true;
                }
                current = (HtmlAttribute)current._next;
            }

            attribute = null;
            return false;
        }

        public HtmlElement NextElement => (HtmlElement)_next;
        public HtmlElement PreviousElement => (HtmlElement)_previous;

        public IEnumerable<HtmlElement> NextElements() => NextElements(null);

        public IEnumerable<HtmlElement> NextElements(string tag)
        {
            bool isDefaultTag = string.IsNullOrEmpty(tag);

            HtmlElement nextElement = (HtmlElement)_next;
            while (nextElement != null)
            {
                if (isDefaultTag || nextElement.Tag == tag)
                {
                    yield return nextElement;
                }
                nextElement = (HtmlElement)nextElement._next;
            }
        }

        public IEnumerable<HtmlElement> PreviousElements() => PreviousElements(null);

        public IEnumerable<HtmlElement> PreviousElements(string tag)
        {
            bool isDefaultTag = string.IsNullOrEmpty(tag);

            HtmlElement previousElement = (HtmlElement)_previous;
            while (previousElement != null)
            {
                if (isDefaultTag || previousElement.Tag == tag)
                {
                    yield return previousElement;
                }
                previousElement = (HtmlElement)previousElement._previous;
            }
        }

        public IEnumerable<HtmlElement> Descendants() => Descendants(null);

        public IEnumerable<HtmlElement> Descendants(string tag)
        {
            bool isDefaultTag = string.IsNullOrEmpty(tag);

            HtmlElement element = _elements._first;
            while (element != null)
            {
                if (isDefaultTag || element.Tag == tag)
                {
                    yield return element;
                }
                foreach (HtmlElement child in element.Descendants(tag))
                {
                    yield return child;
                }
                element = (HtmlElement)element._next;
            }
        }

        public IEnumerable<HtmlElement> DescendantsAndSelf() => DescendantsAndSelf(null);

        public IEnumerable<HtmlElement> DescendantsAndSelf(string tag)
        {
            bool isDefaultTag = string.IsNullOrEmpty(tag);

            if (isDefaultTag || Tag == tag)
            {
                yield return this;
            }
            foreach (HtmlElement element in Descendants(tag))
            {
                yield return element;
            }
        }

        public IEnumerable<HtmlElement> Ancestors() => Ancestors(null);

        public IEnumerable<HtmlElement> Ancestors(string tag)
        {
            bool isDefaultTag = string.IsNullOrEmpty(tag);

            HtmlElement parent = Parent;
            while (parent != null)
            {
                if (isDefaultTag || parent.Tag == tag)
                {
                    yield return parent;
                }
                parent = parent.Parent;
            }
        }

        public IEnumerable<HtmlElement> AncestorsAndSelf() => AncestorsAndSelf(null);

        public IEnumerable<HtmlElement> AncestorsAndSelf(string tag)
        {
            bool isDefaultTag = string.IsNullOrEmpty(tag);

            if (isDefaultTag || Tag == tag)
            {
                yield return this;
            }
            foreach (HtmlElement element in Ancestors(tag))
            {
                yield return element;
            }
        }

        private int _minimumIndentDepth = 1;
        public int MinimumIndentDepth
        {
            get { return _minimumIndentDepth; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The minimum indent depth cannot be negative");
                }
                if (value > _maximumIndentDepth)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The minimum indent depth cannot be larger than the maximum indent depth");
                }

                _minimumIndentDepth = value;
            }
        }

        private int _maximumIndentDepth = 9;
        public int MaximumIndentDepth
        {
            get { return _maximumIndentDepth; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The maximum indent depth cannot be negative");
                }
                if (value < _minimumIndentDepth)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "The maximum indent depth cannot be less than the minimum indent depth");
                }

                _maximumIndentDepth = value;
            }
        }

        internal override void Serialize(StringBuilder builder, HtmlSerializeOptions serializeType)
        {
            Serialize(builder, serializeType, 0);
        }

        private void Serialize(StringBuilder stringBuilder, HtmlSerializeOptions serializeType, int depth)
        {
            SerializeOpenTag(stringBuilder, serializeType);
            if (IsVoid)
            {
                return;
            }
            
            if (InnerText != null)
            {
                stringBuilder.Append(InnerText);
            }
            var shouldIndent = depth >= MinimumIndentDepth && depth <= MaximumIndentDepth;
            foreach (var child in _elements)
            {
                if (serializeType != HtmlSerializeOptions.NoFormatting)
                {
                    stringBuilder.AppendLine();
                }
                if (shouldIndent)
                {
                    stringBuilder.Append(' ', depth * 2);
                }
                if (!string.IsNullOrWhiteSpace(child.InnerText) && child._elements._count == 0)
                {
                    child.Serialize(stringBuilder, serializeType, depth);
                }
                else
                {
                    child.Serialize(stringBuilder, serializeType, depth + 1);
                }
            }
            if (_elements._count > 0 && serializeType != HtmlSerializeOptions.NoFormatting)
            {
                stringBuilder.AppendLine();
            }

            if (depth > MaximumIndentDepth)
            {
                depth = MaximumIndentDepth;
                stringBuilder.Append(' ', 2);
            }

            if (shouldIndent && depth - 2 >= 0)
            {
                stringBuilder.Append(' ', (depth - 2) * 2);
            }
            stringBuilder.Append("</");
            stringBuilder.Append(Tag);
            stringBuilder.Append('>');
        }

        private void SerializeOpenTag(StringBuilder stringBuilder, HtmlSerializeOptions serializeType)
        {
            stringBuilder.Append('<');
            stringBuilder.Append(Tag);

            if (_attributes._count != 0)
            {
                foreach (var attribute in Attributes())
                {
                    stringBuilder.Append(' ');
                    attribute.Serialize(stringBuilder, serializeType);
                }
            }
            stringBuilder.Append(IsVoid ? " />" : ">");
        }

        private void ThrowIfVoid()
        {
            if (IsVoid)
            {
                throw new InvalidOperationException("Cannot set inner text for a void element");
            }
        }
    }

    public static class HtmlElementExtensions
    {
        public static T WithElement<T>(this T self, HtmlElement element) where T : HtmlElement
        {
            self.Add(element);
            return self;
        }

        public static T WithElements<T>(this T self, IEnumerable<HtmlElement> elements) where T : HtmlElement
        {
            self.Add(elements);
            return self;
        }

        public static T WithAttribute<T>(this T self, HtmlAttribute attribute) where T : HtmlElement
        {
            self.Add(attribute);
            return self;
        }

        public static T WithAttributes<T>(this T self, IEnumerable<HtmlAttribute> attributes) where T : HtmlElement
        {
            self.Add(attributes);
            return self;
        }

        public static T WithInnerText<T>(this T self, string innerText) where T : HtmlElement
        {
            self.SetInnerText(innerText);
            return self;
        }

        public static T WithAccessKey<T>(this T self, string value) where T : HtmlElement => self.WithAttribute(Attribute.AccessKey(value));

        public static T WithClass<T>(this T self, string value) where T : HtmlElement => self.WithAttribute(Attribute.Class(value));

        public static T WithContentEditable<T>(this T self, string value) where T : HtmlElement => self.WithAttribute(Attribute.ContentEditable(value));

        public static T WithContextMenu<T>(this T self, string value) where T : HtmlElement => self.WithAttribute(Attribute.ContextMenu(value));

        public static T WithDir<T>(this T self, string value) where T : HtmlElement => self.WithAttribute(Attribute.Dir(value));

        public static T WithHidden<T>(this T self, string value) where T : HtmlElement => self.WithAttribute(Attribute.Hidden(value));

        public static T WithId<T>(this T self, string value) where T : HtmlElement => self.WithAttribute(Attribute.Id(value));

        public static T WithLang<T>(this T self, string value) where T : HtmlElement => self.WithAttribute(Attribute.Lang(value));

        public static T WithSpellCheck<T>(this T self, string value) where T : HtmlElement => self.WithAttribute(Attribute.SpellCheck(value));

        public static T WithStyle<T>(this T self, string value) where T : HtmlElement => self.WithAttribute(Attribute.Style(value));

        public static T WithTabIndex<T>(this T self, string value) where T : HtmlElement => self.WithAttribute(Attribute.TabIndex(value));
    }
}
