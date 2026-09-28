using System.Xml;

namespace BulletMLLib
{
    /// <summary>
    /// An XmlDocument that remembers the line and column of each element, so errors can say where they are in the file.
    /// XmlDocument throws that information away, but it creates each element while the reader is sitting on it, so we grab it then.
    /// </summary>
    internal class LineInfoXmlDocument : XmlDocument
    {
        private IXmlLineInfo _lineInfo;

        /// <summary>
        /// Load the document from a reader, recording the location of each element.
        /// </summary>
        /// <param name="reader">The reader to load from.</param>
        public override void Load(XmlReader reader)
        {
            _lineInfo = reader as IXmlLineInfo;
            try
            {
                base.Load(reader);
            }
            finally
            {
                _lineInfo = null;
            }
        }

        public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
        {
            var element = new LineInfoXmlElement(prefix, localName, namespaceURI, this);
            if (null != _lineInfo && _lineInfo.HasLineInfo())
            {
                element.LineNumber = _lineInfo.LineNumber;
                element.LinePosition = _lineInfo.LinePosition;
            }
            return element;
        }
    }

    /// <summary>
    /// An XmlElement that knows where it was in the file.
    /// </summary>
    internal class LineInfoXmlElement : XmlElement, IXmlLineInfo
    {
        public int LineNumber { get; set; }

        public int LinePosition { get; set; }

        public LineInfoXmlElement(string prefix, string localName, string namespaceURI, XmlDocument doc)
            : base(prefix, localName, namespaceURI, doc)
        {
        }

        public bool HasLineInfo()
        {
            return LineNumber > 0;
        }
    }
}
