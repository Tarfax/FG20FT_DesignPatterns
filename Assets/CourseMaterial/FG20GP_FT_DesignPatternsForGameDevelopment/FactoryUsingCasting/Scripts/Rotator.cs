using UnityEngine;

namespace DesignPatternCourse.FactoryUsingCasting
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField]
        float speed = 120f;

        private void Update()
        {
            transform.Rotate(0f, 0f, speed * Time.deltaTime);
        }
    }
}