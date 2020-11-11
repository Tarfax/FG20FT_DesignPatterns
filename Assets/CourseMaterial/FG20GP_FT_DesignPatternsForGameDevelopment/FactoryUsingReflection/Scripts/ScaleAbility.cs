using UnityEngine;

namespace DesignPatternCourse.FactoryUsingReflection
{
    public class ScaleAbility : Ability
    {
        public override string Name => "Ability of Scale";

        public override void Process(Object o, params object[] parameters)
        {
            Transform transform = o as Transform;
            if (transform == null)
            {
                Debug.LogError("Cast To Transfomr Failed!");
                return;
            }

            transform.localScale += Vector3.one * (float)parameters[0];
        }
    }
}