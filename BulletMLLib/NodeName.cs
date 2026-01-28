
namespace BulletMLLib
{
    /// <summary>
    /// Identifies the type of a BulletML XML element.
    /// </summary>
    public enum NodeName
    {
        /// <summary>A bullet definition with direction, speed, and actions.</summary>
        bullet,

        /// <summary>A sequence of commands defining bullet behavior.</summary>
        action,

        /// <summary>Fires a new bullet with optional direction and speed.</summary>
        fire,

        /// <summary>Gradually changes a bullet's direction over a duration.</summary>
        changeDirection,

        /// <summary>Gradually changes a bullet's speed over a duration.</summary>
        changeSpeed,

        /// <summary>Applies horizontal and vertical acceleration over a duration.</summary>
        accel,

        /// <summary>Pauses action execution for a number of frames.</summary>
        wait,

        /// <summary>Repeats a child action a specified number of times.</summary>
        repeat,

        /// <summary>References a labeled bullet definition.</summary>
        bulletRef,

        /// <summary>References a labeled action definition.</summary>
        actionRef,

        /// <summary>References a labeled fire definition.</summary>
        fireRef,

        /// <summary>Removes the bullet from the game.</summary>
        vanish,

        /// <summary>Horizontal acceleration component inside an accel node.</summary>
        horizontal,

        /// <summary>Vertical acceleration component inside an accel node.</summary>
        vertical,

        /// <summary>Duration in frames for changeDirection, changeSpeed, or accel nodes.</summary>
        term,

        /// <summary>Repeat count for a repeat node.</summary>
        times,

        /// <summary>Direction value with a type modifier (aim, absolute, relative, sequence).</summary>
        direction,

        /// <summary>Speed value with a type modifier (absolute, relative, sequence).</summary>
        speed,

        /// <summary>Parameter value passed to a referenced action, bullet, or fire node.</summary>
        param,

        /// <summary>Root element of a BulletML document.</summary>
        bulletml
    };
}
