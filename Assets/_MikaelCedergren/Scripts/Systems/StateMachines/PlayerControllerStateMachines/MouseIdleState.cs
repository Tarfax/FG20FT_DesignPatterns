using UnityEngine;

public class MouseIdleState : IMouseState {
    private int heroLayer = 1 << 9;
    private int enemyLayer = 1 << 10;

    private Hero hoveredHero;
    private Enemy hoveredEnemy;

    public void Enter() {

    }

    public void Exit() {
        if (hoveredHero != null) {
            hoveredHero.Unhover();
            hoveredHero = null;
        }
        if (hoveredEnemy != null) {
            hoveredEnemy.Unhover();
            hoveredEnemy = null;
        }
    }

    public void Update() {
        HoverHero();
        HoverEnemy();
        LeftMouseButtonInput();
        RightMouseButtonDown();
    }

    private void HoverHero() {
        Hero hero = Hero.GetHeroAt(InputSystem.GetMousePositionInWorld(heroLayer));
        if (hero == null && hoveredHero != null) {
            hoveredHero.Unhover();
            hoveredHero = null;
        }

        if (hero != null) {
            if (hero == hoveredHero) {
                return;
            }

            if (hoveredHero != null) {
                hoveredHero.Unhover();
            }
            hero.Hover();
            hoveredHero = hero;
        }
    }

    private void HoverEnemy() {
        Enemy enemy = Enemy.GetEnemyAt(InputSystem.GetMousePositionInWorld(enemyLayer));
        if (enemy == null && hoveredEnemy != null) {
            hoveredEnemy.Unhover();
            hoveredEnemy = null;
        }

        if (enemy != null) {
            if (enemy == hoveredEnemy) {
                return;
            }

            if (hoveredEnemy != null) {
                hoveredEnemy.Unhover();
            }
            enemy.Hover();
            hoveredEnemy = enemy;
        }
    }

    private void RightMouseButtonDown() {
        if (Input.GetMouseButtonDown(1) == true) {
            Debug.Log("Hit");
            if (hoveredHero != null) {
                hoveredHero.PlayerHit();
            }
        }
    }

    private void LeftMouseButtonInput() {
        if (Input.GetMouseButtonDown(0)) {
            if (hoveredHero != null) {
                hoveredHero.Select();
                hoveredHero = null;
                PlayerController.ChangeToState<MouseHeroSelectedState>();
            }
        }
    }

}
