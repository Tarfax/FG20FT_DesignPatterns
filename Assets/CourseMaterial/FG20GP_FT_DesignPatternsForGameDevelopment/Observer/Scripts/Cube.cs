using UnityEngine;

namespace DesignPatternCourse.Observer
{
    /// <summary>
    /// Switch colors when the button is clicked.
    /// </summary>
    public class Cube : MonoBehaviour
    {
        Color[] colors = new Color[] { Color.red, Color.green };
        Renderer myRenderer = null;

        private void Start()
        {
            myRenderer = GetComponent<Renderer>();
            myRenderer.material.color = colors[0];
        }

        private void OnEnable()
        {
            // Add ButtonClicked method to the observers of the Button.OnClicked Action (Event).
            // Each time the button is clicked, the ButtonClicked method will fire.
            // Notice how the Cube class is reacting to something happening inside the Button class 
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
            SwitchColors();
        }

        private void SwitchColors()
        {
            for (int i = 0; i < colors.Length; i++)
            {
                if (myRenderer.material.color == colors[i])
                {
                    myRenderer.material.color = colors[(i + 1) % colors.Length];
                    break;
                }
            }
        }
    }
}