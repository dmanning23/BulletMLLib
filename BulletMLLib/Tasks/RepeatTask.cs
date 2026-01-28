using System.Diagnostics;

namespace BulletMLLib
{
	/// <summary>
	/// Task that handles repeat nodes by resetting all child sequence nodes when re-initialized.
	/// This ensures sequence values accumulate correctly across repeat iterations.
	/// </summary>
	public class RepeatTask : BulletMLTask
	{
		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletMLLib.BulletMLTask"/> class.
		/// </summary>
		/// <param name="node">Node.</param>
		/// <param name="owner">Owner.</param>
		public RepeatTask(RepeatNode node, BulletMLTask owner) : base(node, owner)
		{
			Debug.Assert(null != Node);
			Debug.Assert(null != Owner);
		}

		/// <summary>
		/// Init this task and all its sub tasks.  
		/// This method should be called AFTER the nodes are parsed, but BEFORE run is called.
		/// </summary>
		/// <param name="bullet">The bullet this task is controlling.</param>
		public override void InitTask(Bullet bullet)
		{
			//Init task is being called on a RepeatTask, which means all the sequence nodes underneath this one need to be reset

			//Call the HardReset method of the base class
			HardReset(bullet);
		}
		
		#endregion //Methods
	}
}