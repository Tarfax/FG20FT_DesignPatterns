public class EndTurnEvent : IEvent {
    public static EndTurnEvent GetEvent() {
        return new EndTurnEvent();
    }
}