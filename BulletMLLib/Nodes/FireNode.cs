using System.Diagnostics;

namespace BulletMLLib
{
    public class FireNode : BulletMLNode
    {
        #region Members

        /// <summary>
        /// A bullet node this task will use to set any bullets shot from this task
        /// </summary>
        /// <value>The bullet node.</value>
        public BulletNode BulletDescriptionNode { get; set; }

        #endregion //Members

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletMLLib.FireNode"/> class.
        /// </summary>
        public FireNode(IBulletManager manager) : this(NodeName.fire, manager)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletMLLib.FireNode"/> class.
        /// this is the constructor used by sub classes
        /// </summary>
        /// <param name="NodeType">the node type.</param>
        public FireNode(NodeName nodeType, IBulletManager manager) : base(nodeType, manager)
        {
        }

        /// <summary>
        /// Validates the node.
        /// Overloaded in child classes to validate that each type of node follows the correct business logic.
        /// This checks stuff that isn't validated by the XML validation
        /// </summary>
        public override void ValidateNode()
        {
            base.ValidateNode();

            //check for a bullet node
            BulletDescriptionNode = GetChild(NodeName.bullet) as BulletNode;

            //if it didn't find one, check for the bulletref node
            if (null == BulletDescriptionNode)
            {
                //make sure that dude knows what he's doing
                BulletRefNode refNode = GetChild(NodeName.bulletRef) as BulletRefNode;
                refNode.FindMyBulletNode();
                BulletDescriptionNode = refNode.ReferencedBulletNode;
            }

            Debug.Assert(null != BulletDescriptionNode);
        }

        #endregion Methods
    }
}
