
namespace BulletMLLib
{
	//TODO: sort this shit out

	/// <summary>
	/// This is a template for creating new bullets.
	/// These things are stored in a bullet object, and used to shoot more bullets.
	/// It seems like every task in a bullet has a corresponding firedata object
	/// They are initialized to 0 and set by the task when that task is run.
	/// </summary>
	public class FireData
	{
		#region Members

		/// <summary>
		/// The initial speed of bullets that are shot with this firedata object
		/// </summary>
		public float srcSpeed = 0;

		/// <summary>
		/// The initial direction of bullets that are shot with this firedata
		/// </summary>
		public float srcDir = 0;

		/// <summary>
		/// I don't quite get what this thing is for... if it is false, the bullet will default initial speed to 1?
		/// </summary>
		public bool speedInit = false;

		#endregion //Members
	}
}