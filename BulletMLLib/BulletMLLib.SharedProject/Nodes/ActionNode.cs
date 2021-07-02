using System;

namespace BulletMLLib
{
	/// <summary>
	/// Action node... also the base class for actionref nodes
	/// </summary>
	public class ActionNode : BulletMLNode
	{
		#region Members

		/// <summary>
		/// Gets or sets the parent repeat node.
		/// This is the node immediately above this one that says how many times to repeat this action.
		/// </summary>
		/// <value>The parent repeat node.</value>
		public RepeatNode ParentRepeatNode { get; private set; }

		#endregion //Members

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletMLLib.ActionNode"/> class.
		/// </summary>
		public ActionNode(IBulletManager manager) : base(ENodeName.action, manager)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletMLLib.ActionNode"/> class.
		/// this is the constructor used by sub classes
		/// </summary>
		/// <param name="eNodeType">the node type.</param>
		public ActionNode(ENodeName eNodeType, IBulletManager manager) : base(eNodeType, manager)
		{
		}

		/// <summary>
		/// Validates the node.
		/// Overloaded in child classes to validate that each type of node follows the correct business logic.
		/// This checks stuff that isn't validated by the XML validation
		/// </summary>
		public override void ValidateNode()
		{
			//Get our parent repeat node if we have one
			ParentRepeatNode = FindParentRepeatNode();

			//do any base class validation
			base.ValidateNode();
		}

		/// <summary>
		/// Finds the parent repeat node.
		/// This method is not recursive, since action and actionref nodes can be nested.
		/// </summary>
		/// <returns>The parent repeat node.</returns>
		private RepeatNode FindParentRepeatNode()
		{
			//Parent node should never ever be empty on an action node
			if (null == Parent)
			{
				throw new NullReferenceException("Parent node cannot be empty on an action or actionRef node");
			}

			//If the parent is a repeat node, check how many times to repeat this action
			if (Parent.Name == ENodeName.repeat)
			{
				return Parent as RepeatNode;
			}

			//This dude is not under a repeat node
			return null;
		}

		/// <summary>
		/// Get the number of times this action should be repeated.
		/// </summary>
		/// <param name="myTask">the task to get the number of repeat times for</param>
		/// <returns>The number of times to repeat this node, as specified by a parent Repeat node.</returns>
		public int RepeatNum(ActionTask myTask, Bullet bullet)
		{
			if (null != ParentRepeatNode)
			{
				//Get the equation value of the repeat node
				return (int)ParentRepeatNode.GetChildValue(ENodeName.times, myTask, bullet);
			}
			else
			{
				//no repeat nodes, just repeat it once
				return 1;
			}
		}

		#endregion //Methods
	}
}
