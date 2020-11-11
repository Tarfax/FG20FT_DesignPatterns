using FutureGamesLib;
using UnityEngine;

namespace DesignPatternCourse.FactoryUsingCasting
{
    public class VerticalSinMover : MonoBehaviour
    {
        [SerializeField]
        float speed = 5f;

        private void Update()
        {
            transform.position = transform.position.With(y: Mathf.Sin(speed * Time.time));
        }
    }
}