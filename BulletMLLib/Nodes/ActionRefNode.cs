using System.Collections.Generic;

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
                throw ValidationError("An actionRef node is missing a label");
            }

            //Find the action node this reference points to
            BulletMLNode refNode = GetRootNode().FindLabelNode(Label, NodeName.action);

            //make sure we found something
            if (null == refNode)
            {
                throw ValidationError("Couldn't find the action node \"" + Label + "\"");
            }

            ReferencedActionNode = refNode as ActionNode;
            if (null == ReferencedActionNode)
            {
                throw ValidationError("The BulletMLNode \"" + Label + "\" isn't an action node");
            }

            //An action can reference itself, but only if it waits first. Otherwise it would loop forever in a single frame.
            CheckForCircularReference(ReferencedActionNode, new Stack<ActionNode>());
        }

        /// <summary>
        /// Walk the actions that would run from an action node in a single frame, and throw if any action ends up referencing itself.
        /// Once a wait node is reached, the rest runs in a later frame, so the walk stops there.
        /// </summary>
        /// <param name="action">The action node to check.</param>
        /// <param name="path">The action nodes currently being walked.</param>
        /// <returns>true if a wait node was reached</returns>
        private static bool CheckForCircularReference(ActionNode action, Stack<ActionNode> path)
        {
            if (path.Contains(action))
            {
                throw action.ValidationError("The action node \"" + action.Label + "\" has a circular actionRef with no wait before it");
            }

            path.Push(action);
            bool waited = CheckChildNodesForCircularReference(action, path);
            path.Pop();
            return waited;
        }

        /// <summary>
        /// Recurse into the child nodes of a node, following any actionRefs to the actions they point to.
        /// </summary>
        /// <param name="node">The node whose children to check.</param>
        /// <param name="path">The action nodes currently being walked.</param>
        /// <returns>true if a wait node was reached</returns>
        private static bool CheckChildNodesForCircularReference(BulletMLNode node, Stack<ActionNode> path)
        {
            foreach (BulletMLNode childNode in node.ChildNodes)
            {
                bool waited = false;
                switch (childNode.Name)
                {
                    case NodeName.wait:
                        {
                            waited = true;
                        }
                        break;

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
                                waited = CheckForCircularReference(refAction, path);
                            }
                        }
                        break;

                    case NodeName.action:
                        {
                            waited = CheckForCircularReference(childNode as ActionNode, path);
                        }
                        break;

                    default:
                        {
                            waited = CheckChildNodesForCircularReference(childNode, path);
                        }
                        break;
                }

                if (waited)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion //Methods
    }
}
