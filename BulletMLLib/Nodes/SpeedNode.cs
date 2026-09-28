
namespace BulletMLLib
{
    /// <summary>
    /// Node representing a &lt;speed&gt; element that specifies a bullet's speed in pixels per frame.
    /// Defaults to absolute type if no type is specified.
    /// </summary>
    public class SpeedNode : BulletMLNode
    {
        private static readonly NodeType[] SpeedTypes = { NodeType.absolute, NodeType.relative, NodeType.sequence };

        /// <summary>
        /// The values allowed in the type attribute of a speed node.
        /// </summary>
        protected override NodeType[] ValidTypes
        {
            get
            {
                return SpeedTypes;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeedNode"/> class.
        /// </summary>
        /// <param name="manager">The bullet manager.</param>
        public SpeedNode(IBulletManager manager) : base(NodeName.speed, manager)
        {
            //set the default type to "absolute"
            NodeType = NodeType.absolute;
        }
    }
}
