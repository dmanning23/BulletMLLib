using System.Diagnostics;

namespace BulletMLLib
{
	/// <summary>
	/// This task pauses for a specified amount of time before resuming
	/// </summary>
	public class WaitTask : BulletMLTask
	{
		#region Members

		/// <summary>
		/// How long to run this task... measured in frames
		/// This task will pause until the durection runs out, then resume running tasks
		/// </summary>
		private float Duration { get; set; }

		#endregion //Members

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletMLLib.BulletMLTask"/> class.
		/// </summary>
		/// <param name="node">Node.</param>
		/// <param name="owner">Owner.</param>
		public WaitTask(WaitNode node, BulletMLTask owner) : base(node, owner)
		{
			Debug.Assert(null != Node);
			Debug.Assert(null != Owner);
		}

		/// <summary>
		/// this sets up the task to be run.
		/// </summary>
		/// <param name="bullet">Bullet.</param>
		protected override void SetupTask(Bullet bullet)
		{
			Duration = Node.GetValue(this, bullet);
		}

		/// <summary>
		/// Run this task and all subtasks against a bullet
		/// This is called once a frame during runtime.
		/// </summary>
		/// <returns>ERunStatus: whether this task is done, paused, or still running</returns>
		/// <param name="bullet">The bullet to update this task against.</param>
		public override ERunStatus Run(Bullet bullet)
		{
			Duration -= 1.0f * bullet.TimeSpeed;
			if (Duration >= 0.0f)
			{
				return ERunStatus.Stop;
			}
			else
			{
				TaskFinished = true;
				return ERunStatus.End;
			}
		}

		#endregion //Methods
	}
}