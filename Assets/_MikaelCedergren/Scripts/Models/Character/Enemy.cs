using MC_Utility.EventSystem;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : BaseCharacter {

    protected static List<Enemy> enemies;

    public Enemy() { }

    public override void Destroy() {
        if (enemies != null && enemies.Count > 0) {
            if (enemies.Contains(this) == true) {
                enemies.Remove(this);
                if (enemies.Count == 0) {
                    enemies = null;
                }
            }
        }
        OnDestroy();

        //Do Tile Stuff
        Tile = null;

        EventSystem<UpdateEvent>.UnregisterListener(Update);
        EventSystem<EndTurnEvent>.UnregisterListener(EndTurn);
    }
    protected virtual void OnDestroy() { }

    public override void Spawn(Tile tile) {
        if (enemies == null) {
            enemies = new List<Enemy>();
        }
        enemies.Add(this);

        Tile = tile;
        Tile.Occupy(this);
        IsAlive = true;
        OnSpawn();
        EventSystem<UpdateEvent>.RegisterListener(Update);
        EventSystem<EndTurnEvent>.RegisterListener(EndTurn);
    }
    protected virtual void OnSpawn() { }

    private void EndTurn(EndTurnEvent endTurnEvent) {
        OnEndTurn(endTurnEvent);
    }

    protected virtual void OnEndTurn(EndTurnEvent endTurnEvent) { }

    private void Update(UpdateEvent updateEvent) {
        OnUpdate(updateEvent);
    }

    protected virtual void OnUpdate(UpdateEvent updateEvent) { }

    public static Enemy GetEnemyAt(Tile tile) {
        if (enemies != null) {
            foreach (Enemy enemy in enemies) {
                if (enemy.Tile == tile) {
                    return enemy;
                }
            }
        }
        return null;
    }
    public static Enemy GetEnemyAt(Vector3 position) {
        return GetEnemyAt(Tile.GetTileAt(position));
    }

}
