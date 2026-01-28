
namespace BulletMLLib
{
    /// <summary>
    /// Orientation of a bullet pattern, set on the root bulletml element.
    /// </summary>
    public enum PatternType
    {
        /// <summary>Bullets travel top-to-bottom. 0 degrees points up.</summary>
        vertical,

        /// <summary>Bullets travel left-to-right. 0 degrees points right.</summary>
        horizontal,

        /// <summary>No specific orientation.</summary>
        none
    }
}
