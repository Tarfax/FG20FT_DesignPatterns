using UnityEngine;

namespace DesignPatternCourse.StrategyWithScriptableObjects
{
    public abstract class GameAction : ScriptableObject, IGameAction
    {
        public abstract object Do(object obj);
    }
}