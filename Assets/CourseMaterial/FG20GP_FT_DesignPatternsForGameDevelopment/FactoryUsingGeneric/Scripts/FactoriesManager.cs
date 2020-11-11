using UnityEngine;

namespace DesignPatternCourse.FactoryUsingGeneric
{
    public class FactoriesManager : MonoBehaviour
    {
        [SerializeField]
        float randomRadius = 5f;

        RotatorFactory[] rotatorFactories = new RotatorFactory[0];
        VerticalSinMoverFactory[] verticalSinMoverFactories = new VerticalSinMoverFactory[0];

        private void Start()
        {
            rotatorFactories = FindObjectsOfType<RotatorFactory>();
            verticalSinMoverFactories = FindObjectsOfType<VerticalSinMoverFactory>();

            foreach (RotatorFactory t in rotatorFactories)
            {
                Rotator rotator = t.GetNewInstance();
                rotator.transform.position = Random.insideUnitSphere * randomRadius;
            }

            foreach (VerticalSinMoverFactory t in verticalSinMoverFactories)
            {
                VerticalSinMover verticalSinMover = t.GetNewInstance();
                verticalSinMover.transform.position = Random.insideUnitSphere * randomRadius;
            }
        }
    }
}