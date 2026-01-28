using System;
using System.Collections.Generic;
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
                    NodeType = BulletMLNode.StringToType(strValue);
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
                    if (XmlNodeType.Text == childNode.NodeType)
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

                    //create a new node
                    BulletMLNode childBulletNode = NodeFactory.CreateNode(BulletMLNode.StringToName(childNode.Name), manager);

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
            //validate all the child nodes
            foreach (BulletMLNode childnode in ChildNodes)
            {
                childnode.ValidateNode();
            }
        }

        #endregion //XML Methods

        #endregion //Methods
    }
}
