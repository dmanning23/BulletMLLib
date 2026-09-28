using System.Diagnostics;

namespace BulletMLLib
{
    /// <summary>
    /// Node representing a &lt;fireRef&gt; element that references a labeled fire definition.
    /// </summary>
    public class FireRefNode : FireNode
    {
        #region Members

        /// <summary>
        /// Gets the referenced fire node.
        /// </summary>
        /// <value>The referenced fire node.</value>
        public FireNode ReferencedFireNode { get; private set; }

        #endregion //Members

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletMLLib.FireRefNode"/> class.
        /// </summary>
        public FireRefNode(IBulletManager manager) : base(NodeName.fireRef, manager)
        {
        }

        /// <summary>
        /// Validates the node.
        /// Overloaded in child classes to validate that each type of node follows the correct business logic.
        /// This checks stuff that isn't validated by the XML validation
        /// </summary>
        public override void ValidateNode()
        {
            if (string.IsNullOrEmpty(Label))
            {
                throw ValidationError("A fireRef node is missing a label");
            }

            //make sure only param nodes are underneath this node
            ValidateChildNodeNames();

            //Find the fire node this reference points to
            Debug.Assert(null != GetRootNode());
            BulletMLNode refNode = GetRootNode().FindLabelNode(Label, NodeName.fire);

            //make sure we found something
            if (null == refNode)
            {
                throw ValidationError("Couldn't find the fire node \"" + Label + "\"");
            }

            ReferencedFireNode = refNode as FireNode;
            if (null == ReferencedFireNode)
            {
                throw ValidationError("The BulletMLNode \"" + Label + "\" isn't a fire node");
            }

            //Skip base class validation since the bullet node belongs to the referenced fire, not this ref node.
        }

        #endregion //Methods
    }
}
