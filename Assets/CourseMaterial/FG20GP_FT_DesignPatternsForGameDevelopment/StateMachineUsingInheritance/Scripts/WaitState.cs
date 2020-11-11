using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternCourse.StateMachineUsingInheritance
{
    public class WaitState : BaseState
    {
        public float minWait = 1f;
        public float maxWait = 2.1f;
        float waitTime = 0f;

        public override void Prepare()
        {
            base.Prepare();

            waitTime = Random.Range(minWait, maxWait);
        }

        public override void Update()
        {
            base.Update();

            waitTime -= Time.deltaTime;

            if(waitTime < 0f)
            {
                owner.ChangeState(new MoveState());
            }

            Debug.Log("Updating Wait State");
        }
    }
}