using System.Collections.Generic;
using UnityEngine;

namespace DesignPatternCourse.FactoryUsingCasting
{
    public class Factory : MonoBehaviour
    {
        enum ProductsToInstantiate
        {
            Rotators,
            VeritcalSinMovers,
        }
        [SerializeField]
        ProductsToInstantiate productsToInstantiate = ProductsToInstantiate.Rotators;

        [SerializeField]
        MonoBehaviour[] products = new MonoBehaviour[0];

        private void Start()
        {
            switch (productsToInstantiate)
            {
                case ProductsToInstantiate.Rotators:
                    InstantiateRotators();
                    break;

                case ProductsToInstantiate.VeritcalSinMovers:
                    InstantiateVerticalSinMovers();
                    break;
            }
        }

        private void InstantiateRotators()
        {
            Rotator[] rotators = GetRotators();
            for (int i = 0; i < rotators.Length; i++)
            {
                Instantiate(rotators[i]);
            }
        }

        public Rotator[] GetRotators()
        {
            List<Rotator> r = new List<Rotator>();

            foreach (Object t in products)
            {
                Rotator rotator = t as Rotator;
                if (rotator != null)
                {
                    r.Add(rotator);
                }
            }

            return r.ToArray();
        }

        private void InstantiateVerticalSinMovers()
        {
            VerticalSinMover[] verticalSinMovers = GetVerticalSinMovers();
            for (int i = 0; i < verticalSinMovers.Length; i++)
            {
                Instantiate(verticalSinMovers[i]);
            }
        }

        public VerticalSinMover[] GetVerticalSinMovers()
        {

            List<VerticalSinMover> r = new List<VerticalSinMover>();

            foreach (Object t in products)
            {
                VerticalSinMover verticalSinMover = t as VerticalSinMover;
                if (verticalSinMover != null)
                {
                    r.Add(verticalSinMover);
                }
            }

            return r.ToArray();
        }
    }
}