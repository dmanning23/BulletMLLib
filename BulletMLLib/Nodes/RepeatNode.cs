
namespace BulletMLLib
{
    /// <summary>
    /// Node representing a &lt;repeat&gt; element that repeats a child action a specified number of times.
    /// </summary>
    public class RepeatNode : BulletMLNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepeatNode"/> class.
        /// </summary>
        /// <param name="manager">The bullet manager.</param>
        public RepeatNode(IBulletManager manager) : base(NodeName.repeat, manager)
        {
        }
    }
}
