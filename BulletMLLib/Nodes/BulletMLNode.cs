using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace BulletMLLib
{
    /// <summary>
    /// This is a single node from a BulletML document.
    /// Used as the base node for all the other node types.
    /// </summary>
    public class BulletMLNode
    {
        #region Members

        /// <summary>
        /// The XML node name of this item
        /// </summary>
        public NodeName Name { get; private set; }

        /// <summary>
        /// The type modifier of this node (e.g. aim, absolute, relative, sequence).
        /// </summary>
        private NodeType _nodeType = NodeType.none;

        /// <summary>
        /// Gets or sets the type of the node.
        /// Virtual so subclasses can override it with their own validation logic.
        /// </summary>
        /// <value>The type of the node.</value>
        public virtual NodeType NodeType
        {
            get
            {
                return _nodeType;
            }
            protected set
            {
                _nodeType = value;
            }
        }

        /// <summary>
        /// The label of this node
        /// This can be used by other nodes to reference this node
        /// </summary>
        public string Label { get; protected set; }

        /// <summary>
        /// An equation used to get a value of this node.
        /// </summary>
        /// <value>The node value.</value>
        protected BulletMLEquation NodeEquation;

        /// <summary>
        /// A list of all the child nodes of this node.
        /// </summary>
        public List<BulletMLNode> ChildNodes { get; private set; }

        /// <summary>
        /// The parent node of this node in the tree.
        /// </summary>
        protected BulletMLNode Parent { get; private set; }

        /// <summary>
        /// The ID of this node.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The line in the xml file this node came from, or 0 if not known.
        /// </summary>
        public int LineNumber { get; private set; }

        /// <summary>
        /// The column in the xml file this node came from, or 0 if not known.
        /// </summary>
        public int LinePosition { get; private set; }

        /// <summary>
        /// Keys used to store the location of an error in Exception.Data, so BulletPattern can report it.
        /// </summary>
        internal const string LineNumberKey = "BulletML.LineNumber";
        internal const string LinePositionKey = "BulletML.LinePosition";

        /// <summary>
        /// The values allowed in the type attribute of this node.
        /// Empty for nodes that don't take a type attribute.
        /// </summary>
        protected virtual NodeType[] ValidTypes
        {
            get
            {
                return NoTypes;
            }
        }

        private static readonly NodeType[] NoTypes = new NodeType[0];

        /// <summary>
        /// The child nodes each type of node is allowed to have, from the bulletml DTD.
        /// </summary>
        private static readonly Dictionary<NodeName, NodeName[]> AllowedChildren = new Dictionary<NodeName, NodeName[]>
        {
            { NodeName.bulletml, new[] { NodeName.bullet, NodeName.fire, NodeName.action } },
            { NodeName.bullet, new[] { NodeName.direction, NodeName.speed, NodeName.action, NodeName.actionRef } },
            { NodeName.action, new[] { NodeName.changeDirection, NodeName.accel, NodeName.vanish, NodeName.changeSpeed, NodeName.repeat, NodeName.wait, NodeName.fire, NodeName.fireRef, NodeName.action, NodeName.actionRef } },
            { NodeName.fire, new[] { NodeName.direction, NodeName.speed, NodeName.bullet, NodeName.bulletRef } },
            { NodeName.changeDirection, new[] { NodeName.direction, NodeName.term } },
            { NodeName.changeSpeed, new[] { NodeName.speed, NodeName.term } },
            { NodeName.accel, new[] { NodeName.horizontal, NodeName.vertical, NodeName.term } },
            { NodeName.repeat, new[] { NodeName.times, NodeName.action, NodeName.actionRef } },
            { NodeName.bulletRef, new[] { NodeName.param } },
            { NodeName.actionRef, new[] { NodeName.param } },
            { NodeName.fireRef, new[] { NodeName.param } },
        };

        /// <summary>
        /// The child nodes each type of node must have, from the bulletml DTD.
        /// </summary>
        private static readonly Dictionary<NodeName, NodeName[]> RequiredChildren = new Dictionary<NodeName, NodeName[]>
        {
            { NodeName.changeDirection, new[] { NodeName.direction, NodeName.term } },
            { NodeName.changeSpeed, new[] { NodeName.speed, NodeName.term } },
            { NodeName.accel, new[] { NodeName.term } },
            { NodeName.repeat, new[] { NodeName.times } },
        };

        #endregion //Members

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletMLLib.BulletMLNode"/> class.
        /// </summary>
        public BulletMLNode(NodeName nodeType, IBulletManager manager)
        {
            NodeEquation = new BulletMLEquation(manager);
            ChildNodes = new List<BulletMLNode>();
            Name = nodeType;
            NodeType = NodeType.none;
        }

        /// <summary>
        /// Convert a string to it's NodeType enum equivalent
        /// </summary>
        /// <returns>The enum value of that string.</returns>
        /// <param name="str">The string to convert to an enum</param>
        public static NodeType StringToType(string str)
        {
            //make sure there is something there
            if (string.IsNullOrEmpty(str))
            {
                return NodeType.none;
            }
            else
            {
                return (NodeType)Enum.Parse(typeof(NodeType), str);
            }
        }

        /// <summary>
        /// Convert a string to it's NodeName enum equivalent
        /// </summary>
        /// <returns>The enum value of that string.</returns>
        /// <param name="str">The string to convert to an enum</param>
        public static NodeName StringToName(string str)
        {
            return (NodeName)Enum.Parse(typeof(NodeName), str);
        }

        /// <summary>
        /// Gets the root node.
        /// </summary>
        /// <returns>The root node.</returns>
        public BulletMLNode GetRootNode()
        {
            //recurse up until we get to the root node
            if (null != Parent)
            {
                return Parent.GetRootNode();
            }

            //if it gets here, there is no parent node and this is the root.
            return this;
        }

        /// <summary>
        /// Find a node of a specific type and label
        /// Recurse into the xml tree until we find it!
        /// </summary>
        /// <returns>The label node.</returns>
        /// <param name="strLabel">Label of the node we are looking for.</param>
        /// <param name="eName">Name of the node we are looking for.</param>
        public BulletMLNode FindLabelNode(string strLabel, NodeName eName)
        {
            //an empty label would match every unlabeled node, so it can never be a valid search
            if (string.IsNullOrEmpty(strLabel))
            {
                return null;
            }

            //this uses breadth first search, since labelled nodes are usually top level

            //Check if any of our child nodes match the request
            for (int i = 0; i < ChildNodes.Count; i++)
            {
                if ((eName == ChildNodes[i].Name) && (strLabel == ChildNodes[i].Label))
                {
                    return ChildNodes[i];
                }
            }

            //recurse into the child nodes and see if we find any matches
            for (int i = 0; i < ChildNodes.Count; i++)
            {
                BulletMLNode foundNode = ChildNodes[i].FindLabelNode(strLabel, eName);
                if (null != foundNode)
                {
                    return foundNode;
                }
            }

            //didnt find a BulletMLNode with that name :(
            return null;
        }

        /// <summary>
        /// Find a parent node of the specified node type
        /// </summary>
        /// <returns>The first parent node of that type, null if none found</returns>
        /// <param name="nodeType">Node type to find.</param>
        public BulletMLNode FindParentNode(NodeName nodeType)
        {
            //first check if we have a parent node
            if (null == Parent)
            {
                return null;
            }
            else if (nodeType == Parent.Name)
            {
                //Our parent matches the query, return it!
                return Parent;
            }
            else
            {
                //recurse into parent nodes to check grandparents, etc.
                return Parent.FindParentNode(nodeType);
            }
        }

        /// <summary>
        /// Gets the value of a specific type of child node for a task
        /// </summary>
        /// <returns>The child value. return 0.0 if no node found</returns>
        /// <param name="name">type of child node we want.</param>
        /// <param name="task">Task to get a value for.</param>
        /// <param name="bullet">The bullet to evaluate against.</param>
        public float GetChildValue(NodeName name, BulletMLTask task, Bullet bullet)
        {
            foreach (BulletMLNode tree in ChildNodes)
            {
                if (tree.Name == name)
                {
                    return tree.GetValue(task, bullet);
                }
            }
            return 0.0f;
        }

        /// <summary>
        /// Get a direct child node of a specific type.  Does not recurse!
        /// </summary>
        /// <returns>The child.</returns>
        /// <param name="name">The type of child node to find.</param>
        public BulletMLNode GetChild(NodeName name)
        {
            foreach (BulletMLNode node in ChildNodes)
            {
                if (node.Name == name)
                {
                    return node;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the value of this node for a specific instance of a task.
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="task">Task.</param>
        /// <param name="bullet">The bullet to get the value for</param>
        public float GetValue(BulletMLTask task, Bullet bullet)
        {
            //send to the equation for an answer
            return (float)NodeEquation.Solve(task.GetParamValue);
        }

        #region XML Methods

        /// <summary>
        /// Parse the specified bulletNodeElement.
        /// Reads all data from the xml node into this BulletMLNode.
        /// </summary>
        /// <param name="bulletNodeElement">Bullet node element.</param>
        /// <param name="parentNode">The parent node in the tree.</param>
        /// <param name="manager">The bullet manager.</param>
        public void Parse(XmlNode bulletNodeElement, BulletMLNode parentNode, IBulletManager manager)
        {
            // Handle null argument.
            if (null == bulletNodeElement)
            {
                throw new ArgumentNullException("bulletNodeElement");
            }

            //grab the parent node
            Parent = parentNode;

            //remember where this node is in the file, for error messages
            IXmlLineInfo lineInfo = bulletNodeElement as IXmlLineInfo;
            if (null != lineInfo && lineInfo.HasLineInfo())
            {
                LineNumber = lineInfo.LineNumber;
                LinePosition = lineInfo.LinePosition;
            }

            //Parse all our attributes
            XmlNamedNodeMap mapAttributes = bulletNodeElement.Attributes;
            for (int i = 0; i < mapAttributes.Count; i++)
            {
                string strName = mapAttributes.Item(i).Name;
                string strValue = mapAttributes.Item(i).Value;

                if ("type" == strName)
                {
                    //skip the type attribute in top level nodes
                    if (NodeName.bulletml == Name)
                    {
                        continue;
                    }

                    //get the bullet node type
                    NodeType = ParseValidType(strValue);
                }
                else if ("label" == strName)
                {
                    //label is just a text value
                    Label = strValue;
                }
            }

            //parse all the child nodes
            if (bulletNodeElement.HasChildNodes)
            {
                for (XmlNode childNode = bulletNodeElement.FirstChild;
                     null != childNode;
                     childNode = childNode.NextSibling)
                {
                    //if the child node is a text node, parse it into this node
                    if ((XmlNodeType.Text == childNode.NodeType) || (XmlNodeType.CDATA == childNode.NodeType))
                    {
                        //Get the text of the child xml node, but store it in THIS bullet node
                        NodeEquation.Parse(childNode.Value);
                        continue;
                    }
                    else if (XmlNodeType.Comment == childNode.NodeType)
                    {
                        //skip any comments in the bulletml script
                        continue;
                    }

                    //make sure it's an element bulletml knows about
                    NodeName childName;
                    if (!Enum.TryParse(childNode.Name, out childName) || (childName.ToString() != childNode.Name))
                    {
                        throw CreateError("Unknown element <" + childNode.Name + ">", childNode);
                    }

                    //create a new node
                    BulletMLNode childBulletNode = NodeFactory.CreateNode(childName, manager);

                    //read in the node and store it
                    childBulletNode.Parse(childNode, this, manager);
                    ChildNodes.Add(childBulletNode);
                }
            }
        }

        /// <summary>
        /// Validates the node.
        /// Overloaded in child classes to validate that each type of node follows the correct business logic.
        /// This checks stuff that isn't validated by the XML validation
        /// </summary>
        public virtual void ValidateNode()
        {
            ValidateChildNodeNames();

            //validate all the child nodes
            foreach (BulletMLNode childnode in ChildNodes)
            {
                childnode.ValidateNode();
            }
        }

        /// <summary>
        /// Check that this node only has the child nodes the bulletml DTD allows, and has all the ones it requires.
        /// </summary>
        protected void ValidateChildNodeNames()
        {
            NodeName[] allowed;
            if (!AllowedChildren.TryGetValue(Name, out allowed))
            {
                allowed = new NodeName[0];
            }

            foreach (BulletMLNode childNode in ChildNodes)
            {
                if (Array.IndexOf(allowed, childNode.Name) < 0)
                {
                    throw childNode.ValidationError("<" + childNode.Name + "> is not allowed inside <" + Name + ">");
                }
            }

            NodeName[] required;
            if (RequiredChildren.TryGetValue(Name, out required))
            {
                foreach (NodeName requiredName in required)
                {
                    if (null == GetChild(requiredName))
                    {
                        throw ValidationError("<" + Name + "> requires a <" + requiredName + "> child");
                    }
                }
            }

            //a repeat node needs something to repeat
            if ((NodeName.repeat == Name) && (null == GetChild(NodeName.action)) && (null == GetChild(NodeName.actionRef)))
            {
                throw ValidationError("<repeat> requires an <action> or <actionRef> child");
            }
        }

        /// <summary>
        /// Create the exception to throw when this node is invalid.
        /// The message includes the node's location in the xml file.
        /// </summary>
        /// <returns>The exception to throw.</returns>
        /// <param name="message">Description of the problem.</param>
        internal InvalidDataException ValidationError(string message)
        {
            return CreateError(message, LineNumber, LinePosition);
        }

        /// <summary>
        /// Create the exception to throw when an xml element is invalid.
        /// The message includes the element's location in the xml file, if it is known.
        /// </summary>
        /// <returns>The exception to throw.</returns>
        /// <param name="message">Description of the problem.</param>
        /// <param name="xmlNode">The xml element that has the problem.</param>
        internal static InvalidDataException CreateError(string message, XmlNode xmlNode)
        {
            IXmlLineInfo lineInfo = xmlNode as IXmlLineInfo;
            if (null != lineInfo && lineInfo.HasLineInfo())
            {
                return CreateError(message, lineInfo.LineNumber, lineInfo.LinePosition);
            }
            return CreateError(message, 0, 0);
        }

        private static InvalidDataException CreateError(string message, int lineNumber, int linePosition)
        {
            if (lineNumber <= 0)
            {
                return new InvalidDataException(message);
            }

            var error = new InvalidDataException(message + " (line " + lineNumber + ", column " + linePosition + ")");
            error.Data[LineNumberKey] = lineNumber;
            error.Data[LinePositionKey] = linePosition;
            return error;
        }

        /// <summary>
        /// Convert the text of a type attribute to a NodeType, making sure it is valid for this node.
        /// </summary>
        /// <returns>The node type.</returns>
        /// <param name="strValue">The text of the type attribute.</param>
        private NodeType ParseValidType(string strValue)
        {
            foreach (NodeType validType in ValidTypes)
            {
                if (validType.ToString() == strValue)
                {
                    return validType;
                }
            }

            throw ValidationError("\"" + strValue + "\" is not a valid type for a <" + Name + "> node");
        }

        #endregion //XML Methods

        #endregion //Methods
    }
}
