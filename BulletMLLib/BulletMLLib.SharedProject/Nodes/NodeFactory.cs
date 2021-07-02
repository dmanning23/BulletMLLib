using System;

namespace BulletMLLib
{
	/// <summary>
	/// This is a simple class used to create different types of nodes.
	/// </summary>
	public static class NodeFactory
	{
		/// <summary>
		/// Given a node type, create the correct node.
		/// </summary>
		/// <returns>An instance of the correct node type</returns>
		/// <param name="nodeType">Node type that we want.</param>
		public static BulletMLNode CreateNode(ENodeName nodeType, IBulletManager manager)
		{
			switch (nodeType)
			{
				case ENodeName.bullet:
				{
					return new BulletNode(manager);
				}
				case ENodeName.action:
				{
					return new ActionNode(manager);
				}
				case ENodeName.fire:
				{
					return new FireNode(manager);
				}
				case ENodeName.changeDirection:
				{
					return new ChangeDirectionNode(manager);
				}
				case ENodeName.changeSpeed:
				{
					return new ChangeSpeedNode(manager);
				}
				case ENodeName.accel:
				{
					return new AccelNode(manager);
				}
				case ENodeName.wait:
				{
					return new WaitNode(manager);
				}
				case ENodeName.repeat:
				{
					return new RepeatNode(manager);
				}
				case ENodeName.bulletRef:
				{
					return new BulletRefNode(manager);
				}
				case ENodeName.actionRef:
				{
					return new ActionRefNode(manager);
				}
				case ENodeName.fireRef:
				{
					return new FireRefNode(manager);
				}
				case ENodeName.vanish:
				{
					return new VanishNode(manager);
				}
				case ENodeName.horizontal:
				{
					return new HorizontalNode(manager);
				}
				case ENodeName.vertical:
				{
					return new VerticalNode(manager);
				}
				case ENodeName.term:
				{
					return new TermNode(manager);
				}
				case ENodeName.times:
				{
					return new TimesNode(manager);
				}
				case ENodeName.direction:
				{
					return new DirectionNode(manager);
				}
				case ENodeName.speed:
				{
					return new SpeedNode(manager);
				}
				case ENodeName.param:
				{
					return new ParamNode(manager);
				}
				case ENodeName.bulletml:
				{
					return new BulletMLNode(ENodeName.bulletml, manager);
				}
				default:
				{
					throw new Exception("Unhandled type of ENodeName: \"" + nodeType.ToString() + "\"");
				}
			}
		}
	}
}
