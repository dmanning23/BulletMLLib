
namespace BulletMLLib
{
    /// <summary>
    /// Node representing a &lt;times&gt; element that specifies the repeat count for a repeat node.
    /// </summary>
    public class TimesNode : BulletMLNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimesNode"/> class.
        /// </summary>
        /// <param name="manager">The bullet manager.</param>
        public TimesNode(IBulletManager manager) : base(NodeName.times, manager)
        {
        }
    }
}
