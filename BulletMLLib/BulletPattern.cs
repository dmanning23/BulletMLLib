using Microsoft.Xna.Framework.Content;
using System;
using System.IO;
using System.Xml;

namespace BulletMLLib
{
    /// <summary>
    /// This is a complete document that describes a bullet pattern.
    /// </summary>
    public class BulletPattern
    {
        #region Members

        /// <summary>
        /// The root node of a tree structure that describes the bullet pattern
        /// </summary>
        public BulletMLNode RootNode { get; private set; }

        /// <summary>
        /// Gets the filename.
        /// This property is only set by calling the parse method
        /// </summary>
        /// <value>The filename.</value>
        public string Filename { get; private set; }

        /// <summary>
        /// The orientation of this bullet pattern: horizontal or vertical.
        /// Read from the type attribute of the root bulletml element.
        /// </summary>
        /// <value>The orientation.</value>
        public PatternType Orientation { get; private set; } = PatternType.none;

        private IBulletManager BulletManager { get; set; }

        #endregion //Members

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletMLLib.BulletPattern"/> class.
        /// </summary>
        public BulletPattern(IBulletManager manager)
        {
            if (null == manager)
            {
                throw new ArgumentNullException("manager", "A BulletPattern needs an IBulletManager");
            }

            BulletManager = manager;
            RootNode = null;
        }

        /// <summary>
        /// Parses a bulletml document into this bullet pattern
        /// </summary>
        /// <param name="xmlFileName">Xml file name.</param>
        /// <param name="content">Optional MonoGame ContentManager. If provided, loads via the content pipeline (path should be relative, no extension).</param>
        public void ParseXML(string xmlFileName, ContentManager content = null)
        {
            //grab that filename 
            Filename = xmlFileName;

#if NETFX_CORE
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.DtdProcessing = DtdProcessing.Ignore;
#else
            //The DTD isn't used for validation, that is done in code by BulletMLNode.ValidateNode so it works the same for every load path
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
#endif

            try
            {
                //LineInfoXmlDocument remembers where each element is, so errors can report the line number
                var xmlDoc = new LineInfoXmlDocument();

                //If the content manager is null, load the file as a text file.
                if (null == content)
                {
                    using (XmlReader reader = XmlReader.Create(xmlFileName, settings))
                    {
                        xmlDoc.Load(reader);
                    }
                }
                else
                {
                    //Load the document as a content resource. If you do this, the file name should be relative path with no extension
                    var data = content.Load<string>(xmlFileName);
                    using (XmlReader reader = XmlReader.Create(new StringReader(data), settings))
                    {
                        xmlDoc.Load(reader);
                    }
                }

                ReadXmlDocument(xmlDoc);

                //validate that the bullet nodes are all valid
                RootNode.ValidateNode();
            }
            catch (Exception ex)
            {
                //an error ocurred reading in the tree
                int lineNumber;
                int linePosition;
                GetErrorLocation(ex, out lineNumber, out linePosition);
                throw new BulletMLException(xmlFileName, lineNumber, linePosition, ex);
            }
        }

        /// <summary>
        /// Find where in the xml file an error happened, if the exception knows.
        /// </summary>
        /// <param name="ex">The exception thrown while loading.</param>
        /// <param name="lineNumber">The line of the error, or 0 if not known.</param>
        /// <param name="linePosition">The column of the error, or 0 if not known.</param>
        private static void GetErrorLocation(Exception ex, out int lineNumber, out int linePosition)
        {
            lineNumber = 0;
            linePosition = 0;

            XmlException xmlException = ex as XmlException;
            if (null != xmlException)
            {
                //malformed xml
                lineNumber = xmlException.LineNumber;
                linePosition = xmlException.LinePosition;
            }
            else if (ex.Data.Contains(BulletMLNode.LineNumberKey))
            {
                //an invalid pattern, thrown by BulletMLNode
                lineNumber = (int)ex.Data[BulletMLNode.LineNumberKey];
                linePosition = (int)ex.Data[BulletMLNode.LinePositionKey];
            }
        }

        private void ReadXmlDocument(XmlDocument xmlDoc)
        {
            XmlNode rootXmlNode = xmlDoc.DocumentElement;

            //make sure it is actually an xml node
            if (rootXmlNode.NodeType == XmlNodeType.Element)
            {
                //eat up the name of that xml node
                string strElementName = rootXmlNode.Name;
                if ("bulletml" != strElementName)
                {
                    //The first node HAS to be bulletml
                    throw BulletMLNode.CreateError("The root element needs to be <bulletml>, found <" + strElementName + "> instead", rootXmlNode);
                }

                //Create the root node of the bulletml tree
                RootNode = new BulletMLNode(NodeName.bulletml, BulletManager);

                //Read in the whole bulletml tree
                RootNode.Parse(rootXmlNode, null, BulletManager);

                //Find what kind of pattern this is: horizontal or vertical
                XmlNamedNodeMap mapAttributes = rootXmlNode.Attributes;
                for (int i = 0; i < mapAttributes.Count; i++)
                {
                    //will only have the name attribute
                    string strName = mapAttributes.Item(i).Name;
                    string strValue = mapAttributes.Item(i).Value;
                    if ("type" == strName)
                    {
                        //if  this is a top level node, "type" will be veritcal or horizontal
                        PatternType orientation;
                        if (!Enum.TryParse(strValue, out orientation) || (orientation.ToString() != strValue))
                        {
                            throw BulletMLNode.CreateError("\"" + strValue + "\" is not a valid type for a <bulletml> node", rootXmlNode);
                        }
                        Orientation = orientation;
                    }
                }
            }
        }

        #endregion //Methods
    }
}
