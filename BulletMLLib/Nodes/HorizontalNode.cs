
namespace BulletMLLib
{
    /// <summary>
    /// Node representing a &lt;horizontal&gt; element that specifies the horizontal acceleration component inside an accel node.
    /// </summary>
    public class HorizontalNode : BulletMLNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HorizontalNode"/> class.
        /// </summary>
        /// <param name="manager">The bullet manager.</param>
        public HorizontalNode(IBulletManager manager) : base(NodeName.horizontal, manager)
        {
        }
    }
}
