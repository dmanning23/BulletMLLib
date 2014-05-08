using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Vector2Extensions;

namespace BulletMLLib
{
	/// <summary>
	/// This is the bullet class that outside assemblies will interact with.
	/// Just inherit from this class and override the abstract functions!
	/// </summary>
	public abstract class Bullet : IBullet
	{
		#region Members

		/// <summary>
		/// The direction this bullet is travelling.  Measured as an angle in radians
		/// </summary>
		private float _direction;

		/// <summary>
		/// A bullet manager that manages this bullet.
		/// </summary>
		/// <value>My bullet manager.</value>
		private readonly IBulletManager _bulletManager;

		/// <summary>
		/// The tree node that describes this bullet.  These are shared between multiple bullets
		/// </summary>
		public BulletMLNode MyNode { get; private set; }

		/// <summary>
		/// How fast time moves in this game.
		/// Can be used to do slowdown, speedup, etc.
		/// </summary>
		/// <value>The time speed.</value>
		public float TimeSpeed { get; set; }

		/// <summary>
		/// Change the size of this bulletml script
		/// If you want to reuse a script for a game but the size is wrong, this can be used to resize it
		/// </summary>
		/// <value>The scale.</value>
		public float Scale { get; set; }

		private float _speed;

		/// <summary>
		/// Store the initial velocity of the bullet when it is fired.
		/// </summary>
		private Vector2 _initialVelocity;

		//TODO: do a task factory, we are going to be creating a LOT of those little dudes

		#endregion //Members

		#region Properties

		/// <summary>
		/// The acceleration of this bullet
		/// </summary>
		/// <value>The accel, in pixels/frame^2</value>
		public Vector2 Acceleration { get; set; }

		/// <summary>
		/// Gets or sets the speed
		/// </summary>
		/// <value>The speed, in pixels/frame</value>
		public virtual float Speed 
		{
			get
			{
				return _speed;
			}
			set
			{
				_speed = value;
			}
		}

		/// <summary>
		/// A list of tasks that will define this bullets behavior
		/// </summary>
		public List<BulletMLTask> Tasks { get; private set; }

		/// <summary>
		/// Abstract property to get the X location of this bullet.
		/// measured in pixels from upper left
		/// </summary>
		/// <value>The horizontrla position.</value>
		public abstract float X
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the y parameter of the location
		/// measured in pixels from upper left
		/// </summary>
		/// <value>The vertical position.</value>
		public abstract float Y
		{
			get;
			set;
		}

		/// <summary>
		/// Gets my bullet manager.
		/// </summary>
		/// <value>My bullet manager.</value>
		public IBulletManager MyBulletManager
		{
			get
			{
				return _bulletManager;
			}
		}

		/// <summary>
		/// Gets or sets the direction.
		/// </summary>
		/// <value>The direction in radians.</value>
		public virtual float Direction
		{
			get
			{
				return _direction;
			}
			set
			{
				_direction = MathHelper.WrapAngle(value);
			}
		}

		/// <summary>
		/// Convenience property to get teh label of a bullet.
		/// </summary>
		/// <value>The label.</value>
		public string Label
		{
			get
			{
				return MyNode.Label;
			}
		}

		/// <summary>
		/// This is the initial velocity of the bullet when it is fired.
		/// For example, if the enemy is moving forward and fires bullets, they will clump together because they don't retain the enemy's velocity.
		/// Set this property if you have fast moving enemies or guns and want the bullet pattern to inherit the velocity of the object that fired them.
		/// </summary>
		public Vector2 InitialVelocity
		{
			get
			{
				return _initialVelocity;
			}
			set
			{
				_initialVelocity = value;
			}
		}

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletMLLib.Bullet"/> class.
		/// </summary>
		/// <param name="myBulletManager">My bullet manager.</param>
		protected Bullet(IBulletManager myBulletManager)
		{
			//grba the bullet manager for this dude
			Debug.Assert(null != myBulletManager);
			_bulletManager = myBulletManager;

			Acceleration = Vector2.Zero;

			Tasks = new List<BulletMLTask>();

			//init these to the default
			TimeSpeed = 1.0f;
			Scale = 1.0f;
		}

		/// <summary>
		/// Initialize this bullet with a top level node
		/// </summary>
		/// <param name="rootNode">This is a top level node... find the first "top" node and use it to define this bullet</param>
		public void InitTopNode(BulletMLNode rootNode)
		{
			Debug.Assert(null != rootNode);

			//okay find the item labelled 'top'
			bool bValidBullet = false;
			BulletMLNode topNode = rootNode.FindLabelNode("top", ENodeName.action);
			if (topNode != null)
			{
				//initialize with the top node we found!
				InitNode(topNode);
				bValidBullet = true;
			}
			else
			{
				//ok there is no 'top' node, so that means we have a list of 'top#' nodes
				for (int i = 1; i < 10; i++)
				{
					topNode = rootNode.FindLabelNode("top" + i, ENodeName.action);
					if (topNode != null)
					{
						if (!bValidBullet)
						{
							//Use this bullet!
							InitNode(topNode);
							bValidBullet = true;
						}
						else
						{
							//Create a new bullet
							IBullet newDude = _bulletManager.CreateTopBullet();

							//set the position to this dude's position
							newDude.X = this.X;
							newDude.Y = this.Y;

							//initialize with the node we found
							newDude.InitNode(topNode);
						}
					}
				}
			}

			if (!bValidBullet)
			{
				//We didnt find a "top" node for this dude, remove him from the game.
				_bulletManager.RemoveBullet(this);
			}
		}
		
		/// <summary>
		/// This bullet is fired from another bullet, initialize it from the node that fired it
		/// </summary>
		/// <param name="subNode">Sub node that defines this bullet</param>
		public void InitNode(BulletMLNode subNode)
		{
			Debug.Assert(null != subNode);

			//clear everything out
			Tasks.Clear();
			
			//Grab that top level node
			MyNode = subNode;

			//found a top num node, add a task for it
			BulletMLTask task = new BulletMLTask(subNode, null);

			//parse the nodes into the task list
			task.ParseTasks(this);

			//initialize all the tasks
			task.InitTask(this);

			Tasks.Add(task);

			//Check if we should change the start heading and speed to adjust for an initial velocity
			if (Vector2.Zero != InitialVelocity)
			{
				//now that the heading and speed have been set, adjust them according to the initial velocity.
				Vector2 final = (Direction.ToVector2() * Speed) + InitialVelocity;
				Direction = final.Angle();
				Speed = final.Length();
			}
		}

		/// <summary>
		/// Asynchronous update method
		/// </summary>
		/// <returns></returns>
		public Task UpdateAsync()
		{
			//run the update method on a different thread
			return Task.Factory.StartNew(() => { Update(); });
		}

		/// <summary>
		/// Update this bullet.  Called once every 1/60th of a second during runtime
		/// </summary>
		public virtual void Update()
		{
			//Flag to tell whether or not this bullet has finished all its tasks
			for (int i = 0; i < Tasks.Count; i++)
			{
				Tasks[i].Run(this);
			}

			//only do this stuff if the bullet isn't done, cuz sin/cosin are expensive
			Vector2 vel = (Acceleration + (Direction.ToVector2() * (Speed * TimeSpeed))) * Scale;
			X += vel.X;
			Y += vel.Y;
		}

		/// <summary>
		/// This method gets called after the update method
		/// </summary>
		public abstract void PostUpdate();

		/// <summary>
		/// Get the direction to aim that bullet
		/// </summary>
		/// <returns>angle to target the bullet</returns>
		public virtual float GetAimDir()
		{
			//get the player position so we can aim at that little fucker
			Debug.Assert(null != MyBulletManager);
			Vector2 shipPos = MyBulletManager.PlayerPosition(this);

			//get our position
			Vector2 pos = new Vector2(X, Y);

			//get the angle at that dude
			return (shipPos - pos).Angle();
		}

		/// <summary>
		/// Finds the task by label.
		/// This recurses into child tasks to find the taks with the correct label
		/// Used only for unit testing!
		/// </summary>
		/// <returns>The task by label.</returns>
		/// <param name="strLabel">String label.</param>
		public BulletMLTask FindTaskByLabel(string strLabel)
		{
			//check if any of teh child tasks have a task with that label
			foreach (BulletMLTask childTask in Tasks)
			{
				BulletMLTask foundTask = childTask.FindTaskByLabel(strLabel);
				if (null != foundTask)
				{
					return foundTask;
				}
			}

			return null;
		}

		/// <summary>
		/// given a label and name, find the task that matches
		/// </summary>
		/// <returns>The task by label and name.</returns>
		/// <param name="strLabel">String label of the task</param>
		/// <param name="eName">the name of the node the task should be attached to</param>
		public BulletMLTask FindTaskByLabelAndName(string strLabel, ENodeName eName)
		{
			//check if any of teh child tasks have a task with that label
			foreach (BulletMLTask childTask in Tasks)
			{
				BulletMLTask foundTask = childTask.FindTaskByLabelAndName(strLabel, eName);
				if (null != foundTask)
				{
					return foundTask;
				}
			}

			return null;
		}

		/// <summary>
		/// Check if this bullet has finished running all its tasks.
		/// </summary>
		/// <returns></returns>
		public bool TasksFinished()
		{
			for (int i = 0; i < Tasks.Count; i++)
			{
				if (!Tasks[i].TaskFinished)
				{
					//at least one of the tasks is still working
					return false;
				}
			}

			//all the tasks and their child tasks are done running
			return true;
		}

		#endregion //Methods
	}
}
