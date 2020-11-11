using UnityEngine;

namespace DesignPatternCourse.StrategyWithScriptableObjects
{
    public class GameObjectPosition
    {
        public GameObject gameObject = null;
        public Vector3 position = Vector3.zero;

        public GameObjectPosition(GameObject gameObject, Vector3 position)
        {
            this.gameObject = gameObject;
            this.position = position;
        }
    }
}