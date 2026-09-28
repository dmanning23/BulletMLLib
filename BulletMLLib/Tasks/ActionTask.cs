using System.Collections.Generic;
using System.Diagnostics;

namespace BulletMLLib
{
    /// <summary>
    /// An action task that contains a list of child tasks that are repeated.
    /// </summary>
    public class ActionTask : BulletMLTask
    {
        #region Members

        /// <summary>
        /// The max number of times to repeat this action
        /// </summary>
        public int RepeatNumMax { get; private set; }

        /// <summary>
        /// The number of times this task has been run.
        /// This starts at 0 and the task will repeat until it hits the "max"
        /// </summary>
        public int RepeatNum { get; private set; }

        /// <summary>
        /// Whether this is a recursive actionRef whose referenced action hasn't been built into tasks yet.
        /// </summary>
        private bool ReferencedActionDeferred { get; set; }

        /// <summary>
        /// Params for a restart requested by a recursive actionRef in tail position below this task.
        /// null if no restart is pending.
        /// </summary>
        private List<float> _restartParams;

        /// <summary>
        /// Whether the current run through this action's children started in an earlier frame.
        /// </summary>
        private bool _bodyHasRun;

        #endregion //Members

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletMLLib.ActionTask"/> class.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="owner">Owner.</param>
        public ActionTask(ActionNode node, BulletMLTask owner) : base(node, owner)
        {
            Debug.Assert(null != Node);
            Debug.Assert(null != Owner);
        }

        /// <summary>
        /// Parse a specified node and bullet into this task
        /// </summary>
        /// <param name="bullet">The bullet this task is controlling.</param>
        public override void ParseTasks(Bullet bullet)
        {
            //set the number of times to repeat this action
            var actionNode = Node as ActionNode;
            Debug.Assert(null != actionNode);
            RepeatNumMax = actionNode.RepeatNum(this, bullet);

            //is this an actionref task?
            if (NodeName.actionRef == Node.Name)
            {
                //A recursive actionRef would expand forever, so wait until it runs to build that part of the tree
                if (IsExpanding((Node as ActionRefNode).ReferencedActionNode))
                {
                    ReferencedActionDeferred = true;
                }
                else
                {
                    ParseReferencedAction(bullet);
                }
            }

            //call the base class
            base.ParseTasks(bullet);
        }

        /// <summary>
        /// Add a sub task under this one for the referenced action.
        /// </summary>
        /// <param name="bullet">The bullet this task is controlling.</param>
        private void ParseReferencedAction(Bullet bullet)
        {
            //create the action task
            ActionTask actionTask = new ActionTask((Node as ActionRefNode).ReferencedActionNode, this);

            //parse the children of the action node into the task
            actionTask.ParseTasks(bullet);

            //store the task, ahead of anything else so it runs in the same order as a non-deferred one
            ChildTasks.Insert(0, actionTask);
        }

        /// <summary>
        /// Check whether a task for an action node is already being built above this one in the task tree.
        /// </summary>
        /// <param name="action">The action node to look for.</param>
        /// <returns>true if an owner of this task is running that action node</returns>
        private bool IsExpanding(ActionNode action)
        {
            for (BulletMLTask task = Owner; null != task; task = task.Owner)
            {
                if (task.Node == action)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Find the action a recursive actionRef can restart instead of nesting a new copy of it.
        /// That's only possible when nothing else is left to run between this task and that action,
        /// so restarting it is the same as calling it again. This is what keeps long-running recursive patterns from overflowing the stack.
        /// </summary>
        /// <returns>The running task for the referenced action, or null if this call has to nest.</returns>
        private ActionTask FindTailCallTarget()
        {
            //an actionRef under a repeat calls the action several times, so it isn't a single tail call
            if (1 != RepeatNumMax)
            {
                return null;
            }

            ActionNode target = (Node as ActionRefNode).ReferencedActionNode;
            BulletMLTask child = this;
            for (BulletMLTask parent = Owner; null != parent; child = parent, parent = parent.Owner)
            {
                //everything else in the parent has to be done, so the parent finishes when this does
                if (!IsLastUnfinishedChild(parent, child))
                {
                    return null;
                }

                //an action with repeats still to go has more work after this
                ActionTask parentAction = parent as ActionTask;
                if (null != parentAction && parentAction.RepeatNum < parentAction.RepeatNumMax - 1)
                {
                    return null;
                }

                if (parent.Node == target)
                {
                    //null if the target is the bullet's top task, which isn't an ActionTask. That call nests once, then later calls restart the nested copy.
                    return parentAction;
                }
            }

            return null;
        }

        /// <summary>
        /// Check whether a task is the last child of its parent, and all the other children are finished.
        /// </summary>
        private static bool IsLastUnfinishedChild(BulletMLTask parent, BulletMLTask child)
        {
            List<BulletMLTask> siblings = parent.ChildTasks;
            if (0 == siblings.Count || siblings[siblings.Count - 1] != child)
            {
                return false;
            }

            for (int i = 0; i < siblings.Count - 1; i++)
            {
                if (!siblings[i].TaskFinished)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Evaluate the params of this actionRef, the same way they are evaluated when the task tree is built.
        /// </summary>
        /// <param name="bullet">The bullet this task is controlling.</param>
        /// <returns>The param values.</returns>
        private List<float> EvaluateParams(Bullet bullet)
        {
            var values = new List<float>();
            foreach (BulletMLNode paramNode in Node.ChildNodes)
            {
                values.Add(paramNode.GetValue(Owner, bullet));
            }
            return values;
        }

        /// <summary>
        /// Start this action over with new params, as if a new copy of it had been called.
        /// </summary>
        /// <param name="bullet">The bullet this task is controlling.</param>
        private void Restart(Bullet bullet)
        {
            //set the params first, so the child tasks are set up with the new values
            ParamList.Clear();
            ParamList.AddRange(_restartParams);
            _restartParams = null;

            TaskFinished = false;
            RepeatNum = 0;
            foreach (BulletMLTask task in ChildTasks)
            {
                task.InitTask(bullet);
            }
        }

        /// <summary>
        /// Init this task and all its sub tasks.
        /// </summary>
        /// <param name="bullet">The bullet this task is controlling.</param>
        public override void InitTask(Bullet bullet)
        {
            ClearRestartParams();
            base.InitTask(bullet);
        }

        /// <summary>
        /// This gets called when nested repeat nodes get initialized.
        /// </summary>
        /// <param name="bullet">Bullet.</param>
        public override void HardReset(Bullet bullet)
        {
            ClearRestartParams();
            base.HardReset(bullet);
        }

        /// <summary>
        /// When an action is set up from scratch, drop any params a restart gave it, so it goes back to the params of the actionRef that called it.
        /// </summary>
        private void ClearRestartParams()
        {
            //only actionRef tasks get params of their own, so an action task's params can only have come from a restart
            if (NodeName.action == Node.Name)
            {
                ParamList.Clear();
            }
            _restartParams = null;
        }

        /// <summary>
        /// this sets up the task to be run.
        /// </summary>
        /// <param name="bullet">Bullet.</param>
        protected override void SetupTask(Bullet bullet)
        {
            RepeatNum = 0;
            _bodyHasRun = false;
        }

        /// <summary>
        /// Run this task and all subtasks against a bullet
        /// This is called once a frame during runtime.
        /// </summary>
        /// <returns>ERunStatus: whether this task is done, paused, or still running</returns>
        /// <param name="bullet">The bullet to update this task against.</param>
        public override RunStatus Run(Bullet bullet)
        {
            if (ReferencedActionDeferred)
            {
                //if this is a tail call, restart the running action instead of nesting another copy of it
                ActionTask target = FindTailCallTarget();
                if (null != target)
                {
                    target._restartParams = EvaluateParams(bullet);
                    TaskFinished = true;
                    return RunStatus.End;
                }

                //build the deferred part of a recursive actionRef now that it's needed
                ReferencedActionDeferred = false;
                ParseReferencedAction(bullet);
                ChildTasks[0].InitTask(bullet);
            }

            bool bodyFromEarlierFrame = _bodyHasRun;
            _bodyHasRun = true;

            //run the action until we hit the limit
            while (RepeatNum < RepeatNumMax)
            {
                RunStatus runStatus = base.Run(bullet);

                //What was the return value from running all the child actions?
                switch (runStatus)
                {
                    case RunStatus.End:
                        {
                            //A recursive actionRef below this task asked for it to run again
                            if (null != _restartParams)
                            {
                                Restart(bullet);

                                //If the whole action ran start to finish in this frame, its waits were 0 and it would restart forever.
                                //Hold it to one run per frame by picking up again next frame.
                                if (!bodyFromEarlierFrame)
                                {
                                    _bodyHasRun = false;
                                    return RunStatus.Stop;
                                }

                                bodyFromEarlierFrame = false;
                                break;
                            }

                            //The actions completed successfully, initialize everything and run it again
                            RepeatNum++;

                            //reset all the child tasks
                            foreach (BulletMLTask task in ChildTasks)
                            {
                                task.InitTask(bullet);
                            }
                        }
                        break;

                    case RunStatus.Stop:
                        {
                            //Something in the child tasks paused this action
                            return runStatus;
                        }

                    default:
                        {
                            //One of the child tasks needs to keep running next frame
                            return RunStatus.Continue;
                        }
                }
            }

            //if it gets here, all the child tasks have been run the correct number of times
            TaskFinished = true;
            return RunStatus.End;
        }

        #endregion //Methods
    }
}