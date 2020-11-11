using UnityEngine;

namespace DesignPatternCourse.FactoryUsingGeneric
{
    public class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField]
        T product = null;

        public T GetNewInstance()
        {
            return Instantiate(product);
        }
    }
}