
namespace BulletMLLib
{
	public class DirectionNode : BulletMLNode
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BulletMLLib.DirectionNode"/> class.
		/// </summary>
		public DirectionNode(IBulletManager manager) : base(ENodeName.direction, manager)
		{
			//set the default type to "aim"
			NodeType = ENodeType.aim;
		}

		/// <summary>
		/// Gets or sets the type of the node.
		/// This is virtual so sub-classes can override it and validate their own shit.
		/// </summary>
		/// <value>The type of the node.</value>
		public override ENodeType NodeType 
		{ 
			get
			{
				return base.NodeType;
			}
			protected set
			{
				switch (value)
				{
					case ENodeType.absolute:
					{
						base.NodeType = value;
					}
					break;

					case ENodeType.relative:
					{
						base.NodeType = value;
					}
					break;

					case ENodeType.sequence:
					{
						base.NodeType = value;
					}
					break;

					default:
					{
						//All other node types default to aim, because otherwise they are wrong!
						base.NodeType = ENodeType.aim;
					}
					break;
				}
			}
		}
	}
}
