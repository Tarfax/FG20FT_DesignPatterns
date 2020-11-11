namespace DesignPatternCourse.StateMachineUsingInheritance
{
    public class BaseState
    {
        public StateMachine owner = null;

        public virtual void Prepare()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Destroy()
        {

        }
    }
}