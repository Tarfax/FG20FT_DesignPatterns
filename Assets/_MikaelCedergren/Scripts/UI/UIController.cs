using MC_Utility.EventSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField] private GameObject canvasPrefab;
    [SerializeField] private GameObject buttonPrefab;

    GameObject canvasObject;

    private string[] buttonText;
    private UnityAction[] functions;

    private void Start() {
        buttonText = new string[] { "Create Hero", "Undo Command", "Redo Command", "End Turn" };
        functions = new UnityAction[] { CreateHero, UndoCommand, RedoCommand, EndTurn };
        canvasObject = Factory.CreateInstance(canvasPrefab);
        CreateButtons();
    }

    private void CreateButtons() {
        for (int i = 0; i < buttonText.Length; i++) {
            Button button = Factory.CreateInstance(buttonPrefab).GetComponent<Button>();
            button.gameObject.transform.SetParent(canvasObject.transform.GetChild(0));
            button.GetComponentInChildren<TextMeshProUGUI>().text = buttonText[i];
            button.onClick.AddListener(functions[i]);
        }
    }

    private void CreateHero() {
        EventSystem<CreateHeroEvent>.FireEvent(Factory.CreateInstance<CreateHeroEvent>(Input.mousePosition));
    }

    private void UndoCommand() {
        EventSystem<UndoEvent>.FireEvent(Factory.CreateInstance<UndoEvent>());
    }

    private void RedoCommand() {
        EventSystem<RedoEvent>.FireEvent(Factory.CreateInstance<RedoEvent>());
    }

    private void EndTurn() {
        EventSystem<EndTurnEvent>.FireEvent(Factory.CreateInstance<EndTurnEvent>());
    }

}
