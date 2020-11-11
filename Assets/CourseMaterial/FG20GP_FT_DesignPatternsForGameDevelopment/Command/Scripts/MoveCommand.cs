using UnityEngine;

namespace DesignPatternCourse.CommandPattern
{
    public class MoveCommand : Command
    {
        protected Transform transform = null;
        Vector3 moveAmount = Vector3.zero;

        public MoveCommand(Transform transform, Vector3 moveAmount)
        {
            this.transform = transform;
            this.moveAmount = moveAmount;
        }

        public override void Execute()
        {
            transform.Translate(moveAmount);
        }
    }
}