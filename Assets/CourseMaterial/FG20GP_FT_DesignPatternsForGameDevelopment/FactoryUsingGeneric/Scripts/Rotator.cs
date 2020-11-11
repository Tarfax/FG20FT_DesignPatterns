using UnityEngine;

namespace DesignPatternCourse.FactoryUsingGeneric
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