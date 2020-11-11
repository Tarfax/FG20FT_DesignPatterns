using UnityEngine;

namespace DesignPatternCourse.FactoryUsingReflection
{
    public abstract class Ability
    {
        public abstract string Name { get; }
        public abstract void Process(Object o, params object[] parameters);
    }
}