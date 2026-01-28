
namespace BulletMLLib
{
	/// <summary>
	/// Template data for creating new bullets.
	/// Stored in a bullet object and used by fire tasks to initialize newly fired bullets.
	/// Each task in a bullet has a corresponding FireData that is initialized to defaults
	/// and set by the task when it runs.
	/// </summary>
	public class FireData
	{
		#region Members

		/// <summary>
		/// The initial speed of bullets fired with this fire data.
		/// </summary>
		public float srcSpeed = 0;

		/// <summary>
		/// The initial direction of bullets fired with this fire data.
		/// </summary>
		public float srcDir = 0;

		/// <summary>
		/// Whether the speed has been explicitly set by a speed node.
		/// If false, the bullet will use a default initial speed of 1.
		/// </summary>
		public bool speedInit = false;

		#endregion //Members
	}
}
