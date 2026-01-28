
namespace BulletMLLib
{
    /// <summary>
    /// Node representing a &lt;term&gt; element that specifies the duration in frames for changeDirection, changeSpeed, or accel nodes.
    /// </summary>
    public class TermNode : BulletMLNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TermNode"/> class.
        /// </summary>
        /// <param name="manager">The bullet manager.</param>
        public TermNode(IBulletManager manager) : base(NodeName.term, manager)
        {
        }
    }
}
