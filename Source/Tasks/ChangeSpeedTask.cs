using System.Diagnostics;

namespace BulletMLLib
{
	/// <summary>
	/// This task changes the speed a little bit every frame.
	/// </summary>
	public class ChangeSpeedTask : BulletMLTask
	{
		#region Members

		/// <summary>
		/// The amount pulled out of the node
		/// </summary>
		private float NodeSpeed;

		/// <summary>
		/// the type of speed change, pulled out of the node
		/// </summary>
		private ENodeType ChangeType;

		/// <summary>
		/// How long to run this task... measured in frames
		/// </summary>
		private float Duration { get; set; }

		/// <summary>
		/// How many frames this dude has ran
		/// </summary>
		private float RunDelta { get; set; }

		#endregion //Members

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletMLLib.BulletMLTask"/> class.
		/// </summary>
		/// <param name="node">Node.</param>
		/// <param name="owner">Owner.</param>
		public ChangeSpeedTask(ChangeSpeedNode node, BulletMLTask owner) : base(node, owner)
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
			//set the length of time to run this dude
			Duration = Node.GetChildValue(ENodeName.term, this, bullet);

			//check for divide by 0
			if (0.0f == Duration)
			{
				Duration = 1.0f;
			}

			NodeSpeed = Node.GetChildValue(ENodeName.speed, this, bullet);
			ChangeType = Node.GetChild(ENodeName.speed).NodeType;
		}

		private float GetSpeed(Bullet bullet)
		{
			switch (ChangeType)
			{
				case ENodeType.sequence:
				{
					return NodeSpeed;
				}

				case ENodeType.relative:
				{
					return NodeSpeed / Duration;
				}

				default:
				{
					return ((NodeSpeed - bullet.Speed) / (Duration - RunDelta));
				}
			}
		}

		/// <summary>
		/// Run this task and all subtasks against a bullet
		/// This is called once a frame during runtime.
		/// </summary>
		/// <returns>ERunStatus: whether this task is done, paused, or still running</returns>
		/// <param name="bullet">The bullet to update this task against.</param>
		public override ERunStatus Run(Bullet bullet)
		{
			bullet.Speed += GetSpeed(bullet);

			RunDelta += 1.0f * bullet.TimeSpeed;
			if (Duration <= RunDelta)
			{
				TaskFinished = true;
				return ERunStatus.End;
			}
			else
			{
				//since this task isn't finished, run it again next time
				return ERunStatus.Continue;
			}
		}

		#endregion //Methods
	}
}