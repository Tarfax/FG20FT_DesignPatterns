using UnityEngine;

namespace DesignPatternCourse.StrategyWithScriptableObjects
{
    public class SimpleBullet : MonoBehaviour
    {
        [SerializeField]
        GameActionControlledMovement moveActionForward = null;

        [SerializeField]
        float life = 1f;

        private void Update()
        {
            moveActionForward.Do(transform);

            life -= Time.deltaTime;
            if (life < 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}