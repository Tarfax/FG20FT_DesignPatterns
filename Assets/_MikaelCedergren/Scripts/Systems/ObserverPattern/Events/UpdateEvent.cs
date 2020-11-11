public class UpdateEvent : IEvent {
    public float DeltaTime;

    public UpdateEvent() {
        DeltaTime = 0f;
    }
}