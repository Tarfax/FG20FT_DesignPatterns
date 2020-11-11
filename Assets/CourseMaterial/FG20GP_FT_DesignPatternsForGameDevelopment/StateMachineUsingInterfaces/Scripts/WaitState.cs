using FutureGamesLib;
using UnityEngine;

namespace DesignPatternCourse.StateMachineUsingInterfaces
{
    public class WaitState : IState
    {
        public float minWait = 1f;
        public float maxWait = 2.1f;
        float waitTime = 0f;

        StateMachine owner = null;
        StateMachine IState.Owner { get => owner; set => owner = value; }

        void IState.Destroy()
        {}

        void IState.Prepare()
        {
            waitTime = Random.Range(minWait, maxWait);
        }

        void IState.Update()
        {
            waitTime -= Time.deltaTime;

            if (waitTime < 0f)
            {
                owner.ChangeState(new MoveState());
            }

            Debug.Log("Updating Wait State");
        }
    }
}