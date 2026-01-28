
namespace BulletMLLib
{
    /// <summary>
    /// Node representing a &lt;speed&gt; element that specifies a bullet's speed in pixels per frame.
    /// </summary>
    public class SpeedNode : BulletMLNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpeedNode"/> class.
        /// </summary>
        /// <param name="manager">The bullet manager.</param>
        public SpeedNode(IBulletManager manager) : base(NodeName.speed, manager)
        {
        }
    }
}
