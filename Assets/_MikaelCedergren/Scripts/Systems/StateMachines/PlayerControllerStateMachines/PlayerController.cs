using MC_Utility.EventSystem;
using UnityEngine;

public class PlayerController {

    private static PlayerController instance;

    private IMouseState state;
    private IMouseState newState;
    private IMouseState oldState;

    private bool switchToNewState = true;

    public PlayerController() {
        if (instance == null) {
            instance = this;
            ChangeToState<MouseIdleState>();
            EventSystem<UpdateEvent>.RegisterListener(OnUpdate);
        }
    }

    private void OnUpdate(UpdateEvent updateEvent) {
        if (switchToNewState == true) {
            state = newState;
            switchToNewState = false;
        }

        SpawnEnemy();

        state.Update();
    }

    public void SpawnEnemy() {
        if (Input.GetKeyDown(KeyCode.P) == true) {
            EventSystem<CreateEnemyEvent>.FireEvent(Factory.CreateInstance<CreateEnemyEvent>());
        }
    }

    public static void ChangeToState<T>() where T : IMouseState {
        instance.switchToNewState = true;

        if (instance.state != null) {
            instance.state.Exit();
        }
        instance.oldState = instance.state;

        instance.newState = Factory.CreateInstance<T>();
        instance.newState.Enter();
    }

    public void Destroy() {
        EventSystem<UpdateEvent>.UnregisterListener(OnUpdate);
        state.Exit();
        state = null;
        oldState = null;
    }

}
