
namespace BulletMLLib
{
    /// <summary>
    /// Node representing a &lt;vertical&gt; element that specifies the vertical acceleration component inside an accel node.
    /// </summary>
    public class VerticalNode : BulletMLNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalNode"/> class.
        /// </summary>
        /// <param name="manager">The bullet manager.</param>
        public VerticalNode(IBulletManager manager) : base(NodeName.vertical, manager)
        {
        }
    }
}
