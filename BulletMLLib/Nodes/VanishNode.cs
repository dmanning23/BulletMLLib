
namespace BulletMLLib
{
    /// <summary>
    /// Node representing a &lt;vanish&gt; element that removes the bullet from the game.
    /// </summary>
    public class VanishNode : BulletMLNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VanishNode"/> class.
        /// </summary>
        /// <param name="manager">The bullet manager.</param>
        public VanishNode(IBulletManager manager) : base(NodeName.vanish, manager)
        {
        }
    }
}
