using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace BulletMLLib
{
	/// <summary>
	/// This is a task..each task is the action from a single xml node, for one bullet.
	/// basically each bullet makes a tree of these to match its pattern
	/// </summary>
	public class BulletMLTask
	{
		#region Members

		/// <summary>
		/// A list of child tasks of this dude
		/// </summary>
		public List<BulletMLTask> ChildTasks { get; private set; }

		/// <summary>
		/// The parameter list for this task
		/// </summary>
		public List<float> ParamList { get; private set; }

		/// <summary>
		/// the parent task of this dude in the tree
		/// Used to fetch param values.
		/// </summary>
		public BulletMLTask Owner { get; set; }

		/// <summary>
		/// The bullet ml node that this dude represents
		/// </summary>
		public BulletMLNode Node { get; private set; }

		/// <summary>
		/// whether or not this task has finished running
		/// </summary>
		public bool TaskFinished { get; protected set; }

		#endregion //Members

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletMLLib.BulletMLTask"/> class.
		/// </summary>
		/// <param name="node">Node.</param>
		/// <param name="owner">Owner.</param>
		public BulletMLTask(BulletMLNode node, BulletMLTask owner)
		{
			if (null == node)
			{
				throw new NullReferenceException("node argument cannot be null");
			}

			ChildTasks = new List<BulletMLTask>();
			ParamList = new List<float>();
			TaskFinished = false;
			this.Owner = owner;
			this.Node = node; 
		}

		/// <summary>
		/// Parse a specified node and bullet into this task
		/// </summary>
		/// <param name="myNode">the node for this dude</param>
		/// <param name="bullet">the bullet this dude is controlling</param>
		public virtual void ParseTasks(Bullet bullet)
		{
			if (null == bullet)
			{
				throw new NullReferenceException("bullet argument cannot be null");
			}

			foreach (BulletMLNode childNode in Node.ChildNodes)
			{
				ParseChildNode(childNode, bullet);
			}
		}
		
		/// <summary>
		/// Parse a specified node and bullet into this task
		/// </summary>
		/// <param name="myNode">the node for this dude</param>
		/// <param name="bullet">the bullet this dude is controlling</param>
		public virtual void ParseChildNode(BulletMLNode childNode, Bullet bullet)
		{
			Debug.Assert(null != childNode);
			Debug.Assert(null != bullet);

			//construct the correct type of node
			switch (childNode.Name)
			{
				case ENodeName.repeat:
				{
					//convert the node to an repeatnode
					RepeatNode myRepeatNode = childNode as RepeatNode;

					//create a placeholder bulletmltask for the repeat node
					RepeatTask repeatTask = new RepeatTask(myRepeatNode, this);

					//parse the child nodes into the repeat task
					repeatTask.ParseTasks(bullet);

					//store the task
					ChildTasks.Add(repeatTask);
				}
				break;
			
				case ENodeName.action:
				{
					//convert the node to an ActionNode
					ActionNode myActionNode = childNode as ActionNode;

					//create the action task
					ActionTask actionTask = new ActionTask(myActionNode, this);

					//parse the children of the action node into the task
					actionTask.ParseTasks(bullet);

					//store the task
					ChildTasks.Add(actionTask);
				}
				break;
		
				case ENodeName.actionRef:
				{
					//convert the node to an ActionNode
					ActionRefNode myActionNode = childNode as ActionRefNode;

					//create the action task
					ActionTask actionTask = new ActionTask(myActionNode, this);

					//add the params to the action task
					for (int i = 0; i < childNode.ChildNodes.Count; i++)
					{
						actionTask.ParamList.Add(childNode.ChildNodes[i].GetValue(this, bullet));
					}

					//parse the children of the action node into the task
					actionTask.ParseTasks(bullet);

					//store the task
					ChildTasks.Add(actionTask);
				}
				break;
	
				case ENodeName.changeSpeed:
				{
					ChildTasks.Add(new ChangeSpeedTask(childNode as ChangeSpeedNode, this));
				}
				break;
	
				case ENodeName.changeDirection:
				{
					ChildTasks.Add(new ChangeDirectionTask(childNode as ChangeDirectionNode, this));
				}
				break;

				case ENodeName.fire:
				{
					//convert the node to a fire node
					FireNode myFireNode = childNode as FireNode;

					//create the fire task
					FireTask fireTask = new FireTask(myFireNode, this);

					//parse the children of the fire node into the task
					fireTask.ParseTasks(bullet);

					//store the task
					ChildTasks.Add(fireTask);
				}
				break;

				case ENodeName.fireRef:
				{
					//convert the node to a fireref node
					FireRefNode myFireNode = childNode as FireRefNode;

					//create the fire task
					FireTask fireTask = new FireTask(myFireNode.ReferencedFireNode, this);

					//add the params to the fire task
					for (int i = 0; i < childNode.ChildNodes.Count; i++)
					{
						fireTask.ParamList.Add(childNode.ChildNodes[i].GetValue(this, bullet));
					}

					//parse the children of the action node into the task
					fireTask.ParseTasks(bullet);

					//store the task
					ChildTasks.Add(fireTask);
				}
				break;

				case ENodeName.wait:
				{
					ChildTasks.Add(new WaitTask(childNode as WaitNode, this));
				}
				break;

				case ENodeName.vanish:
				{
					ChildTasks.Add(new VanishTask(childNode as VanishNode, this));
				}
				break;

				case ENodeName.accel:
				{
					ChildTasks.Add(new AccelTask(childNode as AccelNode, this));
				}
				break;
			}
		}

		/// <summary>
		/// This gets called when nested repeat nodes get initialized.
		/// </summary>
		/// <param name="bullet">Bullet.</param>
		public virtual void HardReset(Bullet bullet)
		{
			TaskFinished = false;

			foreach (BulletMLTask task in ChildTasks)
			{
				task.HardReset(bullet);
			}

			SetupTask(bullet);
		}

		/// <summary>
		/// Init this task and all its sub tasks.  
		/// This method should be called AFTER the nodes are parsed, but BEFORE run is called.
		/// </summary>
		/// <param name="bullet">the bullet this dude is controlling</param>
		public virtual void InitTask(Bullet bullet)
		{
			TaskFinished = false;

			foreach (BulletMLTask task in ChildTasks)
			{
				task.InitTask(bullet);
			}

			SetupTask(bullet);
		}

		/// <summary>
		/// this sets up the task to be run.
		/// </summary>
		/// <param name="bullet">Bullet.</param>
		protected virtual void SetupTask(Bullet bullet)
		{
			//overload in child classes
		}

		/// <summary>
		/// Run this task and all subtasks against a bullet
		/// This is called once a frame during runtime.
		/// </summary>
		/// <returns>ERunStatus: whether this task is done, paused, or still running</returns>
		/// <param name="bullet">The bullet to update this task against.</param>
		public virtual ERunStatus Run(Bullet bullet)
		{
			//run all the child tasks
			TaskFinished = true;
			for (int i = 0; i < ChildTasks.Count; i++)
			{
				//is the child task finished running?
				if (!ChildTasks[i].TaskFinished)
				{
					//Run the child task...
					ERunStatus childStaus = ChildTasks[i].Run(bullet);
					if (childStaus == ERunStatus.Stop)
					{
						//The child task is paused, so it is not finished
						TaskFinished = false;
						return childStaus;
					}
					else if (childStaus == ERunStatus.Continue)
					{
						//child task needs to do some more work
						TaskFinished = false;
					}
				}
			}

			return (TaskFinished ?  ERunStatus.End : ERunStatus.Continue);
		}

		/// <summary>
		/// Get the value of a parameter of this task.
		/// </summary>
		/// <returns>The parameter value.</returns>
		/// <param name="iParamNumber">the index of the parameter to get</param>
		public double GetParamValue(int iParamNumber)
		{
			//if that task doesn't have any params, go up until we find one that does
			if (ParamList.Count < iParamNumber)
			{
				//the current task doens't have enough params to solve this value
				if (null != Owner)
				{
					return Owner.GetParamValue(iParamNumber);
				}
				else
				{
					//got to the top of the list...this means not enough params were passed into the ref
					return 0.0f;
				}
			}
			
			//the value of that param is the one we want
			return ParamList[iParamNumber - 1];
		}

		/// <summary>
		/// Gets the node value.
		/// </summary>
		/// <returns>The node value.</returns>
		public float GetNodeValue(Bullet bullet)
		{
			return Node.GetValue(this, bullet);
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
			//check if this is the corretc task
			if (strLabel == Node.Label)
			{
				return this;
			}

			//check if any of teh child tasks have a task with that label
			foreach (BulletMLTask childTask in ChildTasks)
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
			//check if this is the corretc task
			if ((strLabel == Node.Label) && (eName == Node.Name))
			{
				return this;
			}

			//check if any of teh child tasks have a task with that label
			foreach (BulletMLTask childTask in ChildTasks)
			{
				BulletMLTask foundTask = childTask.FindTaskByLabelAndName(strLabel, eName);
				if (null != foundTask)
				{
					return foundTask;
				}
			}

			return null;
		}

		#endregion //Methods
	}
}