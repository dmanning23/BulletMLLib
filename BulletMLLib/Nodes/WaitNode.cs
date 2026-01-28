
namespace BulletMLLib
{
    /// <summary>
    /// Node representing a &lt;wait&gt; element that pauses action execution for a number of frames.
    /// </summary>
    public class WaitNode : BulletMLNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WaitNode"/> class.
        /// </summary>
        /// <param name="manager">The bullet manager.</param>
        public WaitNode(IBulletManager manager) : base(NodeName.wait, manager)
        {
        }
    }
}
