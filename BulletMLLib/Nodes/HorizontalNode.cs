namespace BulletMLLib
{
    /// <summary>
    /// Node representing a &lt;horizontal&gt; element that specifies the horizontal acceleration component inside an accel node.
    /// Defaults to absolute type if no type is specified.
    /// </summary>
    public class HorizontalNode : BulletMLNode
    {
        private static readonly NodeType[] HorizontalTypes = { NodeType.absolute, NodeType.relative, NodeType.sequence };

        /// <summary>
        /// The values allowed in the type attribute of a horizontal node.
        /// </summary>
        protected override NodeType[] ValidTypes
        {
            get
            {
                return HorizontalTypes;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HorizontalNode"/> class.
        /// </summary>
        /// <param name="manager">The bullet manager.</param>
        public HorizontalNode(IBulletManager manager) : base(NodeName.horizontal, manager)
        {
            //set the default type to "absolute"
            NodeType = NodeType.absolute;
        }
    }
}
