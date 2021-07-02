using System;

namespace BulletMLLib
{
	public class BulletRefNode : BulletNode
	{
		#region Members

		/// <summary>
		/// Gets the referenced bullet node.
		/// </summary>
		/// <value>The referenced bullet node.</value>
		public BulletNode ReferencedBulletNode { get; private set; }

		#endregion //Members

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="BulletMLLib.BulletRefNode"/> class.
		/// </summary>
		public BulletRefNode(IBulletManager manager) : base(ENodeName.bulletRef, manager)
		{
		}

		/// <summary>
		/// Validates the node.
		/// Overloaded in child classes to validate that each type of node follows the correct business logic.
		/// This checks stuff that isn't validated by the XML validation
		/// </summary>
		public override void ValidateNode()
		{
			//do any base class validation
			base.ValidateNode();

			//make sure this dude knows where his bullet node is
			FindMyBulletNode();
		}

		/// <summary>
		/// Finds the referenced bullet node.
		/// </summary>
		public void FindMyBulletNode()
		{
			if (null == ReferencedBulletNode)
			{
				//Find the action node this dude references
				BulletMLNode refNode = GetRootNode().FindLabelNode(Label, ENodeName.bullet);

				//make sure we foud something
				if (null == refNode)
				{
					throw new NullReferenceException("Couldn't find the bullet node \"" + Label + "\"");
				}

				ReferencedBulletNode = refNode as BulletNode;
				if (null == ReferencedBulletNode)
				{
					throw new NullReferenceException("The BulletMLNode \"" + Label + "\" isn't a bullet node");
				}
			}
		}

		#endregion //Methods
	}
}
