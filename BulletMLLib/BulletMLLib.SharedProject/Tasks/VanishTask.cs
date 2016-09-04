using System.Diagnostics;

namespace BulletMLLib
{
	/// <summary>
	/// This task removes a bullet from the game.
	/// </summary>
	public class VanishTask : BulletMLTask
	{
		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletMLLib.BulletMLTask"/> class.
		/// </summary>
		/// <param name="node">Node.</param>
		/// <param name="owner">Owner.</param>
		public VanishTask(VanishNode node, BulletMLTask owner) : base(node, owner)
		{
			Debug.Assert(null != Node);
			Debug.Assert(null != Owner);
		}

		/// <summary>
		/// Run this task and all subtasks against a bullet
		/// This is called once a frame during runtime.
		/// </summary>
		/// <returns>ERunStatus: whether this task is done, paused, or still running</returns>
		/// <param name="bullet">The bullet to update this task against.</param>
		public override ERunStatus Run(Bullet bullet)
		{
			//remove the bullet via the bullet manager interface
			IBulletManager manager = bullet.MyBulletManager;
			Debug.Assert(null != manager);
			manager.RemoveBullet(bullet);
			return ERunStatus.End;
		}

		#endregion //Methods
	}
}