using UnityEngine;

namespace DesignPatternCourse.StateMachineUsingInterfaces
{
    public class MoveState : IState
    {
        Vector3 target = Vector3.zero;

        public float fieldExtend = 5f;

        StateMachine owner = null;
        StateMachine IState.Owner { get => owner; set => owner = value; }

        void IState.Destroy()
        {
        }

        void IState.Prepare()
        {
            target = new Vector3(
                Random.Range(-fieldExtend, fieldExtend),
                Random.Range(-fieldExtend, fieldExtend), 0f);
        }

        void IState.Update()
        {
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