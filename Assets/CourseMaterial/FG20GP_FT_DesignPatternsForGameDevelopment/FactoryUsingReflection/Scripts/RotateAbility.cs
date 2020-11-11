using UnityEngine;

namespace DesignPatternCourse.FactoryUsingReflection
{
    public class RotateAbility : Ability
    {
        public override string Name => "Ability of Rotation";

        public override void Process(Object o, params object[] parameters)
        {
            Transform transform = o as Transform;
            if (transform == null)
            {
                Debug.LogError("Cast To Transfomr Failed!");
                return;
            }

            transform.Rotate(0f, 0f, (float)parameters[0]);
        }
    }
}