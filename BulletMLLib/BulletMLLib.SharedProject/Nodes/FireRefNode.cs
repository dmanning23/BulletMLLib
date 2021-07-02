using System;
using System.Diagnostics;

namespace BulletMLLib
{
	public class FireRefNode : FireNode
	{
		#region Members

		/// <summary>
		/// Gets the referenced fire node.
		/// </summary>
		/// <value>The referenced fire node.</value>
		public FireNode ReferencedFireNode { get; private set; }

		#endregion //Members

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletMLLib.FireRefNode"/> class.
		/// </summary>
		public FireRefNode(IBulletManager manager) : base(ENodeName.fireRef, manager)
		{
		}

		/// <summary>
		/// Validates the node.
		/// Overloaded in child classes to validate that each type of node follows the correct business logic.
		/// This checks stuff that isn't validated by the XML validation
		/// </summary>
		public override void ValidateNode()
		{
			//Find the action node this dude 
			Debug.Assert(null != GetRootNode());
			BulletMLNode refNode = GetRootNode().FindLabelNode(Label, ENodeName.fire);

			//make sure we foud something
			if (null == refNode)
			{
				throw new NullReferenceException("Couldn't find the fire node \"" + Label + "\"");
			}

			ReferencedFireNode = refNode as FireNode;
			if (null == ReferencedFireNode)
			{
				throw new NullReferenceException("The BulletMLNode \"" + Label + "\" isn't a fire node");
			}

			//Do not validate the base class of this dude... it will crap out trying to find the bullet node!
		}

		#endregion //Methods
	}
}
