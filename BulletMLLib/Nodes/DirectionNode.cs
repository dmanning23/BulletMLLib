
namespace BulletMLLib
{
    public class DirectionNode : BulletMLNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BulletMLLib.DirectionNode"/> class.
        /// </summary>
        public DirectionNode(IBulletManager manager) : base(NodeName.direction, manager)
        {
            //set the default type to "aim"
            NodeType = NodeType.aim;
        }

        /// <summary>
        /// Gets or sets the type of the node.
        /// This is virtual so sub-classes can override it and validate their own shit.
        /// </summary>
        /// <value>The type of the node.</value>
        public override NodeType NodeType
        {
            get
            {
                return base.NodeType;
            }
            protected set
            {
                switch (value)
                {
                    case NodeType.absolute:
                        {
                            base.NodeType = value;
                        }
                        break;

                    case NodeType.relative:
                        {
                            base.NodeType = value;
                        }
                        break;

                    case NodeType.sequence:
                        {
                            base.NodeType = value;
                        }
                        break;

                    default:
                        {
                            //All other node types default to aim, because otherwise they are wrong!
                            base.NodeType = NodeType.aim;
                        }
                        break;
                }
            }
        }
    }
}
