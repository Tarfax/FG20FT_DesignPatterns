using UnityEngine;

public class CreateHeroEvent : IEvent {
    public Vector3 MousePosition;

    public CreateHeroEvent(Vector3 mousePosition) {
        MousePosition = mousePosition;
    }

    public static CreateHeroEvent GetEvent(Vector3 mousePosition) {
        return new CreateHeroEvent(mousePosition);
    }
}
