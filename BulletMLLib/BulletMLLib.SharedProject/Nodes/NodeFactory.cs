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
		public static BulletMLNode CreateNode(ENodeName nodeType)
		{
			switch (nodeType)
			{
				case ENodeName.bullet:
				{
					return new BulletNode();
				}
				case ENodeName.action:
				{
					return new ActionNode();
				}
				case ENodeName.fire:
				{
					return new FireNode();
				}
				case ENodeName.changeDirection:
				{
					return new ChangeDirectionNode();
				}
				case ENodeName.changeSpeed:
				{
					return new ChangeSpeedNode();
				}
				case ENodeName.accel:
				{
					return new AccelNode();
				}
				case ENodeName.wait:
				{
					return new WaitNode();
				}
				case ENodeName.repeat:
				{
					return new RepeatNode();
				}
				case ENodeName.bulletRef:
				{
					return new BulletRefNode();
				}
				case ENodeName.actionRef:
				{
					return new ActionRefNode();
				}
				case ENodeName.fireRef:
				{
					return new FireRefNode();
				}
				case ENodeName.vanish:
				{
					return new VanishNode();
				}
				case ENodeName.horizontal:
				{
					return new HorizontalNode();
				}
				case ENodeName.vertical:
				{
					return new VerticalNode();
				}
				case ENodeName.term:
				{
					return new TermNode();
				}
				case ENodeName.times:
				{
					return new TimesNode();
				}
				case ENodeName.direction:
				{
					return new DirectionNode();
				}
				case ENodeName.speed:
				{
					return new SpeedNode();
				}
				case ENodeName.param:
				{
					return new ParamNode();
				}
				case ENodeName.bulletml:
				{
					return new BulletMLNode(ENodeName.bulletml);
				}
				default:
				{
					throw new Exception("Unhandled type of ENodeName: \"" + nodeType.ToString() + "\"");
				}
			}
		}
	}
}
