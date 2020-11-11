using UnityEngine;

namespace DesignPatternCourse.StrategyWithScriptableObjects
{
    [CreateAssetMenu(fileName = "Spawner", menuName = "GameActions/Spawner")]
    public class GameActionSpawner : GameAction
    {
        /// <summary>
        /// Expecting GameObjectPosition
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override object Do(object obj)
        {
            GameObjectPosition gameObjectPos = obj as GameObjectPosition;
            if (gameObjectPos == null)
            {
                Debug.LogWarning("Casting to GameObjectPosition failed!");
                return null;
            }

            Instantiate(gameObjectPos.gameObject, gameObjectPos.position, Quaternion.identity);

            return null;
        }
    }
}