
namespace BulletMLLib
{
    /// <summary>
    /// Node representing an &lt;accel&gt; element that applies horizontal and vertical acceleration over a duration.
    /// </summary>
    public class AccelNode : BulletMLNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccelNode"/> class.
        /// </summary>
        /// <param name="manager">The bullet manager.</param>
        public AccelNode(IBulletManager manager) : base(NodeName.accel, manager)
        {
        }
    }
}
