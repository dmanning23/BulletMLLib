using System;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace BulletMLLib
{
    /// <summary>
    /// This task changes the direction a little bit every frame
    /// </summary>
    public class ChangeDirectionTask : BulletMLTask
    {
        #region Members

        /// <summary>
        /// The amount pulled out of the node
        /// </summary>
        private float NodeDirection;

        /// <summary>
        /// the type of direction change, pulled out of the node
        /// </summary>
        private NodeType ChangeType;

        /// <summary>
        /// How long to run this task... measured in frames
        /// </summary>
        private float Duration { get; set; }

        /// <summary>
        /// How many frames this task has run.
        /// </summary>
        private float RunDelta { get; set; }

        #endregion //Members

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="BulletMLLib.BulletMLTask"/> class.
        /// </summary>
        /// <param name="node">Node.</param>
        /// <param name="owner">Owner.</param>
        public ChangeDirectionTask(ChangeDirectionNode node, BulletMLTask owner) : base(node, owner)
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
            RunDelta = 0;

            //set the time length to run this task
            Duration = Node.GetChildValue(NodeName.term, this, bullet);

            //check for divide by 0
            if (0.0f == Duration)
            {
                Duration = 1.0f;
            }

            //Get the amount to change direction from the nodes
            DirectionNode dirNode = Node.GetChild(NodeName.direction) as DirectionNode;
            NodeDirection = dirNode.GetValue(this, bullet) * (float)Math.PI / 180.0f; //also make sure to convert to radians

            //How do we want to change direction?
            ChangeType = dirNode.NodeType;
        }

        private float GetDirection(Bullet bullet)
        {
            //How do we want to change direction?
            float direction = 0.0f;
            switch (ChangeType)
            {
                case NodeType.sequence:
                    {
                        //We are going to add this amount to the direction every frame
                        direction = NodeDirection;
                    }
                    break;

                case NodeType.absolute:
                    {
                        //We are going to go in the direction we are given, regardless of where we are pointing right now
                        direction = NodeDirection - bullet.Direction;
                    }
                    break;

                case NodeType.relative:
                    {
                        //The direction change will be relative to our current direction
                        direction = NodeDirection;
                    }
                    break;

                default:
                    {
                        //the direction change is to aim at the enemy
                        direction = ((NodeDirection + bullet.GetAimDir()) - bullet.Direction);
                    }
                    break;
            }

            //keep the direction between -180 and 180
            direction = MathHelper.WrapAngle(direction);

            //The sequence type of change direction is unaffected by the duration
            if (ChangeType == NodeType.absolute)
            {
                //divide by the amount of time remaining
                direction /= Duration - RunDelta;
            }
            else if (ChangeType != NodeType.sequence)
            {
                //Divide by the duration so we ease into the direction change
                direction /= Duration;
            }

            return direction;
        }

        /// <summary>
        /// Run this task against a bullet. Adjusts the bullet's direction each frame.
        /// </summary>
        /// <returns>RunStatus indicating whether the task is done or still running.</returns>
        /// <param name="bullet">The bullet to update.</param>
        public override RunStatus Run(Bullet bullet)
        {
            //change the direction of the bullet by the correct amount
            bullet.Direction += GetDirection(bullet);

            //decrement the amount if time left to run and return End when this task is finished
            RunDelta += 1.0f * bullet.TimeSpeed;
            if (Duration <= RunDelta)
            {
                TaskFinished = true;
                return RunStatus.End;
            }
            else
            {
                //since this task isn't finished, run it again next time
                return RunStatus.Continue;
            }
        }

        #endregion //Methods
    }
}