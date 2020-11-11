public abstract class BaseCharacterState {
    protected BaseCharacter baseCharacter;

    public void Enter(BaseCharacter character) {
        baseCharacter = character;
        OnEnter();
    }
    protected virtual void OnEnter() { }

    public void Exit() {
        OnExit();
    }
    protected virtual void OnExit() { }

    public void Update(float deltaTime) {
        OnUpdate(deltaTime);
    }
    protected virtual void OnUpdate(float deltaTime) { }
}
