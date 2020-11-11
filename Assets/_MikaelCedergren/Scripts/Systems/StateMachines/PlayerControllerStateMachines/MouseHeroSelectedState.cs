using MC_Utility.EventSystem;
using MCUtility.Pathfinding;
using UnityEngine;

public class MouseHeroSelectedState : IMouseState {

    private Hero selectedHero;
    private Tile hoveredTile;
    private Enemy hoveredEnemy;
    private int floorLayer = 1 << 8;
    private int heroLayer = 1 << 9;
    private int enemyLayer = 1 << 10;
    private Pathfinding pathfinding;

    public void Enter() {
        selectedHero = Hero.SelectedHero;
        EventSystem<UndoEvent>.RegisterListener(OnUndoEvent);
        EventSystem<RedoEvent>.RegisterListener(OnRedoEvent);
        EventSystem<EndTurnEvent>.RegisterListener(OnEndTurnEvent);
    }

    public void Exit() {
        if (hoveredTile != null) {
            hoveredTile.Unhover();
        }
        if (hoveredEnemy != null) {
            hoveredEnemy.Unhover();
        }

        CleanUpPreviewPath();
        EventSystem<UndoEvent>.UnregisterListener(OnUndoEvent);
        EventSystem<RedoEvent>.UnregisterListener(OnRedoEvent);
        EventSystem<EndTurnEvent>.UnregisterListener(OnEndTurnEvent);

    }

    public void Update() {
        LeftMouseButtonInput();
        Hover();
        KillHero();
        RightMouseButtonInput();
    }

    public void KillHero() {
        if (Input.GetKeyDown(KeyCode.K)) {
            selectedHero.Destroy();
            PlayerController.ChangeToState<MouseIdleState>();
        }
    }

    private void LeftMouseButtonInput() {
        if (Input.GetMouseButtonDown(0)) {
            Tile moveToTile = Tile.GetTileAt(InputSystem.GetMousePositionInWorld(floorLayer));
            Hero hero = Hero.GetHeroAt(InputSystem.GetMousePositionInWorld(heroLayer));
            Enemy enemy = Enemy.GetEnemyAt(InputSystem.GetMousePositionInWorld(enemyLayer));
            if (hero != null && hero != selectedHero) {
                selectedHero.Deselect();
                selectedHero.Unhover();
                selectedHero = hero;
                selectedHero.Select();
            }
            else if (moveToTile != null) {
                if (moveToTile.IsOccupied() == false) {
                    selectedHero.IssueMoveToCommand(moveToTile);

                }
                else if (enemy != null) {
                    Pathfinding pathfinding = new Pathfinding(selectedHero.Tile, enemy.Tile, selectedHero, false);
                    selectedHero.IssueHitEnemyCommand(enemy, pathfinding.Last() != null ? pathfinding.Last() : selectedHero.Tile/*enemy.Tile*/);
                }
            }
        }
    }

    private void RightMouseButtonInput() {
        if (Input.GetMouseButtonDown(1)) {
            selectedHero.Deselect();
            selectedHero.Unhover();
            Hero hero = Hero.GetHeroAt(InputSystem.GetMousePositionInWorld(heroLayer));
            if (hero != null) {
                hero.Hover();
            }
            PlayerController.ChangeToState<MouseIdleState>();
        }
    }

    private void Hover() {
        if (hoveredEnemy == null) {
            Tile tile = Tile.GetTileAt(InputSystem.GetMousePositionInWorld(floorLayer));
            if (tile != null && tile != hoveredTile) {
                if (hoveredTile != null) {
                    hoveredTile.Unhover();
                }
                tile.Hover();
                hoveredTile = tile;
                return;
            }
            else if (tile == null && hoveredTile != null) {
                hoveredTile.Unhover();
                hoveredTile = null;
            }
        }
        else if (hoveredTile != null) {
            hoveredTile.Unhover();
            hoveredTile = null;
        }


        Enemy enemy = Enemy.GetEnemyAt(InputSystem.GetMousePositionInWorld(enemyLayer));
        if (enemy != null && enemy != hoveredEnemy) {
            if (hoveredEnemy != null) {
                hoveredEnemy.Unhover();
            }
            enemy.Hover();
            hoveredEnemy = enemy;
            return;
        }
        else if (enemy == null && hoveredEnemy != null) {
            hoveredEnemy.Unhover();
            hoveredEnemy = null;
        }

        CleanUpPreviewPath();
        PreviewPath();

    }

    private void PreviewPath() {
        if (hoveredTile != null) {
            pathfinding = new Pathfinding(selectedHero.Tile, hoveredTile, selectedHero, hoveredEnemy == null);
        }
        else if (hoveredEnemy != null) {
            pathfinding = new Pathfinding(selectedHero.Tile, hoveredEnemy.Tile, selectedHero, hoveredEnemy == null);
        }
        if (pathfinding != null) {
            foreach (var item in pathfinding.GetPath()) {
                item.PreviewPath(true);
            }
        }
    }

    private void CleanUpPreviewPath() {
        if (pathfinding != null) {
            foreach (var item in pathfinding.GetPath()) {
                item.PreviewPath(false);
                pathfinding = null;
            }
        }
    }

    private void OnUndoEvent(UndoEvent undoEvent) {
        selectedHero.UndoMovement();
    }

    private void OnRedoEvent(RedoEvent redoEvent) {
        selectedHero.RedoMovement();
    }

    private void OnEndTurnEvent(EndTurnEvent redoEvent) {
        selectedHero.Deselect();
        selectedHero.Unhover();
        PlayerController.ChangeToState<MouseIdleState>();
    }
}
