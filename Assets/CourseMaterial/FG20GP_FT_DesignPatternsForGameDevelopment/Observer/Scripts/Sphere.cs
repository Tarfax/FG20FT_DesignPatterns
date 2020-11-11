using FutureGamesLib;
using UnityEngine;

namespace DesignPatternCourse.Observer
{
    /// <summary>
    /// Change position when the button is clicked.
    /// </summary>
    public class Sphere : MonoBehaviour
    {
        Vector3[] positions = new Vector3[] { Vector3.zero.With(x: 2f), Vector3.zero.With(y: 2f) };

        private void Start()
        {
            transform.position = positions[0];
        }

        private void OnEnable()
        {
            // Add ButtonClicked method to the observers of the Button.OnClicked Action (Event).
            // Each time the button is clicked, the ButtonClicked method will fire.
            // Notice how the Sphere class is reacting to something happening inside the Button class 
            // without having to reference each other, this is a powerful loose coupling technic.
            Button.OnClicked += ButtonClicked;
        }

        private void OnDisable()
        {
            // Remove ButtonClicked method from the observers of the Button.OnClicked Action (Event)
            // Cleaning up is always nice!
            Button.OnClicked -= ButtonClicked;
        }

        private void ButtonClicked()
        {
            ChangePosition();
        }

        private void ChangePosition()
        {
            for (int i = 0; i < positions.Length; i++)
            {
                if (transform.position == positions[i])
                {
                    transform.position = positions[(i + 1) % positions.Length];
                    break;
                }
            }
        }
    }
}