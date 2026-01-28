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
        /// <param name="manager">The bullet manager.</param>
        public static BulletMLNode CreateNode(NodeName nodeType, IBulletManager manager)
        {
            switch (nodeType)
            {
                case NodeName.bullet:
                    {
                        return new BulletNode(manager);
                    }
                case NodeName.action:
                    {
                        return new ActionNode(manager);
                    }
                case NodeName.fire:
                    {
                        return new FireNode(manager);
                    }
                case NodeName.changeDirection:
                    {
                        return new ChangeDirectionNode(manager);
                    }
                case NodeName.changeSpeed:
                    {
                        return new ChangeSpeedNode(manager);
                    }
                case NodeName.accel:
                    {
                        return new AccelNode(manager);
                    }
                case NodeName.wait:
                    {
                        return new WaitNode(manager);
                    }
                case NodeName.repeat:
                    {
                        return new RepeatNode(manager);
                    }
                case NodeName.bulletRef:
                    {
                        return new BulletRefNode(manager);
                    }
                case NodeName.actionRef:
                    {
                        return new ActionRefNode(manager);
                    }
                case NodeName.fireRef:
                    {
                        return new FireRefNode(manager);
                    }
                case NodeName.vanish:
                    {
                        return new VanishNode(manager);
                    }
                case NodeName.horizontal:
                    {
                        return new HorizontalNode(manager);
                    }
                case NodeName.vertical:
                    {
                        return new VerticalNode(manager);
                    }
                case NodeName.term:
                    {
                        return new TermNode(manager);
                    }
                case NodeName.times:
                    {
                        return new TimesNode(manager);
                    }
                case NodeName.direction:
                    {
                        return new DirectionNode(manager);
                    }
                case NodeName.speed:
                    {
                        return new SpeedNode(manager);
                    }
                case NodeName.param:
                    {
                        return new ParamNode(manager);
                    }
                case NodeName.bulletml:
                    {
                        return new BulletMLNode(NodeName.bulletml, manager);
                    }
                default:
                    {
                        throw new Exception("Unhandled type of NodeName: \"" + nodeType.ToString() + "\"");
                    }
            }
        }
    }
}
