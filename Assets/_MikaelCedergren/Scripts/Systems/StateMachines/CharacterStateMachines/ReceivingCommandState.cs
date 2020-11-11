using UnityEngine;

public class ReceivingCommandState : BaseCharacterState {

    private Tile tile;
    private bool hasArrived;

    protected override void OnEnter() {
        hasArrived = false;
        tile = baseCharacter.Tile;
    }

    protected override void OnUpdate(float deltaTime) {
        if (hasArrived == false) {
            if (Vector3.Distance(baseCharacter.Position, tile.Position) > 0.01f) {
                baseCharacter.Position = Vector3.Lerp(baseCharacter.Position, tile.Position, deltaTime);
            }
            else {
                hasArrived = true;
            }
        }
    }

}
