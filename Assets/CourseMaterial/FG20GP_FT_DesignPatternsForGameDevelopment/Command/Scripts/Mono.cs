using FutureGamesLib;
using UnityEngine;

namespace DesignPatternCourse.CommandPattern
{
    public class Mono : MonoBehaviour
    {
        Vector2 controlCenter = 0.5f * new Vector2(Screen.width, Screen.height) + Vector2.up * 0.7f * Screen.height * 0.5f;

        CommandInvoker commandInvoker = new CommandInvoker();

        private void Update()
        {
            ExecuteCommands();
        }

        private void ExecuteCommands()
        {
            if (Input.GetKeyDown(KeyCode.Return) == false)
                return;

            commandInvoker.ExecuteCommands();
        }

        private void OnGUI()
        {
            UpButton();
            DownButton();

            RightButton();
            LeftButton();
        }

        private void UpButton()
        {
            if (GUI.Button(
                RectsLib.RectByCenterAndSize(
                    controlCenter + Vector2.down * FutureGamesLib.Constants.UI.margin * 2.5f,
                    FutureGamesLib.Constants.UI.RectSize * 0.5f), "Up") == false)
                return;

            commandInvoker.AddCommand(new MoveCommand(transform, Vector3.up));
        }

        private void DownButton()
        {
            if (GUI.Button(
                RectsLib.RectByCenterAndSize(
                    controlCenter + Vector2.up * FutureGamesLib.Constants.UI.margin * 2.5f,
                    FutureGamesLib.Constants.UI.RectSize * 0.5f), "Down") == false)
                return;

            commandInvoker.AddCommand(new MoveCommand(transform, Vector3.down));
        }


        private void RightButton()
        {
            if (GUI.Button(
                RectsLib.RectByCenterAndSize(
                    controlCenter + Vector2.right * FutureGamesLib.Constants.UI.margin * 3f,
                    FutureGamesLib.Constants.UI.RectSize * 0.5f), "Right") == false)
                return;

            commandInvoker.AddCommand(new MoveCommand(transform, Vector3.right));
        }

        private void LeftButton()
        {
            if (GUI.Button(
                RectsLib.RectByCenterAndSize(
                    controlCenter + Vector2.left * FutureGamesLib.Constants.UI.margin * 3f,
                    FutureGamesLib.Constants.UI.RectSize * 0.5f), "Left") == false)
                return;

            commandInvoker.AddCommand(new MoveCommand(transform, Vector3.left));
        }
    }
}