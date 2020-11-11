using FutureGamesLib;
using UnityEngine;

namespace DesignPatternCourse.FactoryUsingReflection
{
    public class Buttons : MonoBehaviour
    {
        [SerializeField]
        Transform transformToActOn = null;

        [SerializeField]
        float scaleBy = 1f;

        [SerializeField]
        float rotateBy = 45f;

        private void OnGUI()
        {
            FireRotateAbility();
            FireScaleAbility();
        }

        private void FireRotateAbility()
        {
            if (GUI.Button(new Rect(Vector2.one * Constants.UI.margin, Constants.UI.RectSize), "Rotate") == false)
                return;

            Ability ability = AbilityFactory.GetAbility("Ability of Rotation");
            ability.Process(transformToActOn, rotateBy);
        }

        private void FireScaleAbility()
        {
            if (GUI.Button(new Rect(
                (Vector2.one * Constants.UI.margin) + (Vector2.up * (Constants.UI.margin + Constants.UI.RectSize.y)),
                Constants.UI.RectSize), "Scale") == false)
                return;

            Ability ability = AbilityFactory.GetAbility("Ability of Scale");
            ability.Process(transformToActOn, scaleBy);
        }
    }
}