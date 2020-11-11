using UnityEngine;

namespace DesignPatternCourse.StrategyWithScriptableObjects
{
    [CreateAssetMenu(fileName = "PeriodicSpawner", menuName = "GameActions/PeriodicSpawner")]
    public class GameActionPeriodicSpawner : GameAction
    {
        [SerializeField]
        float period = 1f;
        float timeCount = 1f;

        /// <summary>
        /// Expecting GameObjectPosition
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override object Do(object obj)
        {
            GameObjectPosition gameObjectPos = obj as GameObjectPosition;
            if(gameObjectPos == null)
            {
                Debug.LogWarning("Casting to GameObjectPosition failed!");
                return null;
            }

            SpawnPeriodic(gameObjectPos);

            return null;
        }

        private void SpawnPeriodic(GameObjectPosition gameObjectPos)
        {
            timeCount -= Time.deltaTime;

            if (timeCount <= 0f)
            {
                timeCount = period;
                Instantiate(gameObjectPos.gameObject, gameObjectPos.position, Quaternion.identity);
            }
        }
    }
}