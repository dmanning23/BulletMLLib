
namespace BulletMLLib
{
    /// <summary>
    /// Status values returned by tasks during runtime execution.
    /// </summary>
    public enum RunStatus
    {
        /// <summary>The task is still running and should continue next frame.</summary>
        Continue,

        /// <summary>The task has finished executing.</summary>
        End,

        /// <summary>The task is paused and should stop processing for this frame.</summary>
        Stop
    }
}
