namespace BulletMLLib
{
    /// <summary>
    /// Node representing a &lt;vertical&gt; element that specifies the vertical acceleration component inside an accel node.
    /// Defaults to absolute type if no type is specified.
    /// </summary>
    public class VerticalNode : BulletMLNode
    {
        private static readonly NodeType[] VerticalTypes = { NodeType.absolute, NodeType.relative, NodeType.sequence };

        /// <summary>
        /// The values allowed in the type attribute of a vertical node.
        /// </summary>
        protected override NodeType[] ValidTypes
        {
            get
            {
                return VerticalTypes;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalNode"/> class.
        /// </summary>
        /// <param name="manager">The bullet manager.</param>
        public VerticalNode(IBulletManager manager) : base(NodeName.vertical, manager)
        {
            //set the default type to "absolute"
            NodeType = NodeType.absolute;
        }
    }
}
