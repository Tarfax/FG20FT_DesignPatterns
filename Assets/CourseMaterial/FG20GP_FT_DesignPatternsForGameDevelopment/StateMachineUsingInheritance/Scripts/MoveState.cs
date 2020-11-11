using UnityEngine;

namespace DesignPatternCourse.StateMachineUsingInheritance
{
    public class MoveState : BaseState
    {
        Vector3 target = Vector3.zero;

        public float fieldExtend = 5f;

        public override void Prepare()
        {
            base.Prepare();

            target = new Vector3(
                Random.Range(-fieldExtend, fieldExtend),
                Random.Range(-fieldExtend, fieldExtend), 0f);
        }

        public override void Update()
        {
            base.Update();

            Vector3 direction = target - owner.transform.position;
            if (direction.magnitude > 1f)
                direction = direction.normalized;

            owner.Move(direction);

            if (direction.magnitude < 0.2f)
            {
                owner.ChangeState(new WaitState());
            }

            Debug.Log("Updating Move State");
        }
    }
}