
namespace BulletMLLib
{
    /// <summary>
    /// Modifier type for direction and speed nodes.
    /// </summary>
    public enum NodeType
    {
        /// <summary>No modifier specified.</summary>
        none,

        /// <summary>Direction aimed at the player position.</summary>
        aim,

        /// <summary>Absolute value independent of the bullet's current state.</summary>
        absolute,

        /// <summary>Value relative to the bullet's current direction or speed.</summary>
        relative,

        /// <summary>Value added to the previous value each time the node fires.</summary>
        sequence
    };
}
