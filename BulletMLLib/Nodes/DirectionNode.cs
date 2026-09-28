
namespace BulletMLLib
{
    /// <summary>
    /// Node representing a &lt;direction&gt; element that specifies a bullet's direction in degrees.
    /// Defaults to aim type if no type is specified.
    /// </summary>
    public class DirectionNode : BulletMLNode
    {
        private static readonly NodeType[] DirectionTypes = { NodeType.aim, NodeType.absolute, NodeType.relative, NodeType.sequence };

        /// <summary>
        /// The values allowed in the type attribute of a direction node.
        /// </summary>
        protected override NodeType[] ValidTypes
        {
            get
            {
                return DirectionTypes;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletMLLib.DirectionNode"/> class.
        /// </summary>
        public DirectionNode(IBulletManager manager) : base(NodeName.direction, manager)
        {
            //set the default type to "aim"
            NodeType = NodeType.aim;
        }
    }
}
