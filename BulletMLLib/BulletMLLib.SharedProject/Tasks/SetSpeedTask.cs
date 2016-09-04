using System.Diagnostics;

namespace BulletMLLib
{
	/// <summary>
	/// This action sets the velocity of a bullet
	/// </summary>
	public class SetSpeedTask : BulletMLTask
	{
		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletMLLib.BulletMLTask"/> class.
		/// </summary>
		/// <param name="node">Node.</param>
		/// <param name="owner">Owner.</param>
		public SetSpeedTask(SpeedNode node, BulletMLTask owner) : base(node, owner)
		{
			Debug.Assert(null != Node);
			Debug.Assert(null != Owner);
		}

		#endregion //Methods
	}
}