using MC_Utility.EventSystem;

public class EnemySpawner {

    private static EnemySpawner instance;

    public EnemySpawner() {
        if (instance == null) {
            instance = this;
            EventSystem<CreateEnemyEvent>.RegisterListener(OnCreateEnemy);
        }
    }

    private void OnCreateEnemy(CreateEnemyEvent createHeroEvent) {
        Tile tile = Tile.GetRandomEnemyTile();
        if (tile != null) {
            new GenericEnemy<FactoryObject_Enemy>(tile.Position).Spawn(tile);
        }
    }

    public void Destroy() {
        EventSystem<CreateEnemyEvent>.UnregisterListener(OnCreateEnemy);
    }

}
