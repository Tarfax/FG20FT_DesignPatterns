using UnityEngine;

public class MouseCreateHeroState : IMouseState {

    private Hero spawnedCharacter;

    public void Enter() {
        if (Hero.SelectedHero != null) {
            Hero.SelectedHero.Deselect();
            Hero.SelectedHero.Unhover();
        }
        spawnedCharacter = HeroSpawner.SpawnedHero;
    }

    public void Exit() {
        spawnedCharacter = null;
    }

    public void Update() {
        Vector3 mousePosition = InputSystem.GetMousePositionInWorld(1 << 8);
        Tile tileUnderMouse = Tile.GetTileAt(mousePosition);
        if (tileUnderMouse != null) {
            spawnedCharacter.Position = Vector3.Lerp(spawnedCharacter.Position, tileUnderMouse.Position, 0.1f);

        }
        else {
            Vector3 correctedMousePosition = new Vector3(mousePosition.x - 0.5f, mousePosition.y, mousePosition.z - 0.5f);
            spawnedCharacter.Position = Vector3.Lerp(spawnedCharacter.Position, correctedMousePosition, 0.04f);
        }

        if (Input.GetMouseButtonDown(0) == true) {
            if (spawnedCharacter.IsPlacementValid(mousePosition) == true) {
                HeroSpawner.PlaceHeroOnTile(tileUnderMouse);
                spawnedCharacter = null;
                PlayerController.ChangeToState<MouseIdleState>();
            }
        }
        else if (Input.GetMouseButtonDown(1) == true) {
            HeroSpawner.DeleteInstantiatedHero();
            PlayerController.ChangeToState<MouseIdleState>();
        }
    }
}