using FutureGamesLib;
using System;
using UnityEngine;

namespace DesignPatternCourse.Observer
{
    public class Button : MonoBehaviour
    {
        const string label = "Click";

        /// <summary>
        /// The Action (Event) to which other objects can subscribe to receive when this Event happens.
        /// </summary>
        public static Action OnClicked = delegate { };

        private void OnGUI()
        {
            Clicked();
        }

        private void Clicked()
        {
            if (GUI.Button(new Rect(Vector2.one * Constants.UI.margin, Constants.UI.RectSize), label) == false)
                return;

            OnClicked();
        }
    }
}