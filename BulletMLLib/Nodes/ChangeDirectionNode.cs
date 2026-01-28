
namespace BulletMLLib
{
    /// <summary>
    /// Node representing a &lt;changeDirection&gt; element that gradually changes a bullet's direction over a duration.
    /// </summary>
    public class ChangeDirectionNode : BulletMLNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeDirectionNode"/> class.
        /// </summary>
        /// <param name="manager">The bullet manager.</param>
        public ChangeDirectionNode(IBulletManager manager) : base(NodeName.changeDirection, manager)
        {
        }
    }
}
