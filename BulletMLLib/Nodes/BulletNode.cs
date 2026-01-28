
namespace BulletMLLib
{
    /// <summary>
    /// Node representing a &lt;bullet&gt; element that defines a bullet's direction, speed, and actions.
    /// </summary>
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
        /// <param name="nodeType">The node type.</param>
        /// <param name="manager">The bullet manager.</param>
        public BulletNode(NodeName nodeType, IBulletManager manager) : base(nodeType, manager)
        {
        }
    }
}
