
namespace BulletMLLib
{
    /// <summary>
    /// Node representing a &lt;changeSpeed&gt; element that gradually changes a bullet's speed over a duration.
    /// </summary>
    public class ChangeSpeedNode : BulletMLNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeSpeedNode"/> class.
        /// </summary>
        /// <param name="manager">The bullet manager.</param>
        public ChangeSpeedNode(IBulletManager manager) : base(NodeName.changeSpeed, manager)
        {
        }
    }
}
