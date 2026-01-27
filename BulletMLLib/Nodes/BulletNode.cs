
namespace BulletMLLib
{
    public class BulletNode : BulletMLNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BulletMLLib.BulletNode"/> class.
        /// </summary>
        public BulletNode(IBulletManager manager) : this(NodeName.bullet, manager)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletMLLib.BulletNode"/> class.
        /// this is the constructor used by sub classes
        /// </summary>
        /// <param name="NodeType">the node type.</param>
        public BulletNode(NodeName nodeType, IBulletManager manager) : base(nodeType, manager)
        {
        }
    }
}
