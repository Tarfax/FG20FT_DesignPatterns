using UnityEngine;

namespace DesignPatternCourse.StateMachineUsingInheritance
{
    public class StateMachine : MonoBehaviour
    {
        BaseState currentState = null;

        Rigidbody myRigidbody = null;
        public float moveSpeed = 10f;

        Rigidbody Rigidbody
        {
            get
            {
                if (myRigidbody == null)
                {
                    myRigidbody = gameObject.AddComponent<Rigidbody>();
                    myRigidbody.isKinematic = true;
                }
                return myRigidbody;
            }
        }

        private void Start()
        {
            ChangeState(new WaitState());
        }

        private void Update()
        {
            UpdateState();
        }

        private void UpdateState()
        {
            if (currentState == null)
                return;

            currentState.Update();
        }

        public void ChangeState(BaseState newState)
        {
            if (currentState != null)
            {
                currentState.Destroy();
            }

            currentState = newState;

            if (currentState != null)
            {
                currentState.owner = this;
                currentState.Prepare();
            }
        }

        public void Move(Vector3 direction)
        {
            Vector3 delta = direction * moveSpeed * Time.deltaTime;
            Rigidbody.MovePosition(Rigidbody.position + delta);
        }
    }
}