using UnityEngine;

namespace DesignPatternCourse.StrategyWithScriptableObjects
{
    [CreateAssetMenu(fileName = "ControlledMovement", menuName = "GameActions/ControlledMovement")]
    public class GameActionControlledMovement : GameAction
    {
        [SerializeField]
        Vector3 direction = Vector3.forward;

        [SerializeField]
        float speed = 10f;

        /// <summary>
        /// Expecting a transform
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override object Do(object obj)
        {
            Transform transform = obj as Transform;
            if (transform == null)
            {
                Debug.LogWarning("Casting to Transfom failed!");
                return null;
            }

            transform.Translate(direction * speed * Time.deltaTime);

            return null;
        }
    }
}