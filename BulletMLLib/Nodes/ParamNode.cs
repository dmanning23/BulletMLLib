
namespace BulletMLLib
{
    /// <summary>
    /// Node representing a &lt;param&gt; element that passes a parameter value to a referenced action, bullet, or fire node.
    /// </summary>
    public class ParamNode : BulletMLNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParamNode"/> class.
        /// </summary>
        /// <param name="manager">The bullet manager.</param>
        public ParamNode(IBulletManager manager) : base(NodeName.param, manager)
        {
        }
    }
}
