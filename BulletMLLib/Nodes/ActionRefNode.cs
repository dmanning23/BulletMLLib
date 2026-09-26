using System;
using System.Collections.Generic;
using System.IO;

namespace BulletMLLib
{
    /// <summary>
    /// Action reference node.
    /// This node type references another Action node.
    /// </summary>
    public class ActionRefNode : ActionNode
    {
        #region Members

        /// <summary>
        /// The action node that this reference points to, resolved during validation.
        /// </summary>
        public ActionNode ReferencedActionNode { get; private set; }

        #endregion //Members

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletMLLib.ActionRefNode"/> class.
        /// </summary>
        public ActionRefNode(IBulletManager manager) : base(NodeName.actionRef, manager)
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

            if (string.IsNullOrEmpty(Label))
            {
                throw new InvalidDataException("An actionRef node is missing a label");
            }

            //Find the action node this reference points to
            BulletMLNode refNode = GetRootNode().FindLabelNode(Label, NodeName.action);

            //make sure we found something
            if (null == refNode)
            {
                throw new NullReferenceException("Couldn't find the action node \"" + Label + "\"");
            }

            ReferencedActionNode = refNode as ActionNode;
            if (null == ReferencedActionNode)
            {
                throw new NullReferenceException("The BulletMLNode \"" + Label + "\" isn't an action node");
            }

            //An action that references itself would expand into an infinite task tree, so catch that here
            CheckForCircularReference(ReferencedActionNode, new Stack<ActionNode>());
        }

        /// <summary>
        /// Walk the action tree that would be expanded from an action node, and throw if any action ends up referencing itself.
        /// </summary>
        /// <param name="action">The action node to check.</param>
        /// <param name="path">The action nodes currently being expanded.</param>
        private static void CheckForCircularReference(ActionNode action, Stack<ActionNode> path)
        {
            if (path.Contains(action))
            {
                throw new InvalidDataException("The action node \"" + action.Label + "\" has a circular actionRef");
            }

            path.Push(action);
            CheckChildNodesForCircularReference(action, path);
            path.Pop();
        }

        /// <summary>
        /// Recurse into the child nodes of a node, following any actionRefs to the actions they point to.
        /// </summary>
        /// <param name="node">The node whose children to check.</param>
        /// <param name="path">The action nodes currently being expanded.</param>
        private static void CheckChildNodesForCircularReference(BulletMLNode node, Stack<ActionNode> path)
        {
            foreach (BulletMLNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case NodeName.fire:
                    case NodeName.fireRef:
                    case NodeName.bullet:
                    case NodeName.bulletRef:
                        {
                            //Fired bullets build their own task trees, so a bullet firing itself is not a cycle
                        }
                        break;

                    case NodeName.actionRef:
                        {
                            //If the label doesn't resolve, that actionRef's own validation will report it
                            ActionNode refAction = childNode.GetRootNode().FindLabelNode(childNode.Label, NodeName.action) as ActionNode;
                            if (null != refAction)
                            {
                                CheckForCircularReference(refAction, path);
                            }
                        }
                        break;

                    case NodeName.action:
                        {
                            CheckForCircularReference(childNode as ActionNode, path);
                        }
                        break;

                    default:
                        {
                            CheckChildNodesForCircularReference(childNode, path);
                        }
                        break;
                }
            }
        }

        #endregion //Methods
    }
}
