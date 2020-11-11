using MC_Utility.EventSystem;

public class HeroSpawner {
    private static HeroSpawner instance;
    public static Hero SpawnedHero { get; private set; }

    public HeroSpawner() {
        if (instance == null) {
            instance = this;
            EventSystem<CreateHeroEvent>.RegisterListener(OnCreateHero);
        }
    }

    private void OnCreateHero(CreateHeroEvent createHeroEvent) {
        if (SpawnedHero == null) {
            SpawnedHero = new GenericHero<FactoryObject_Character>(createHeroEvent.MousePosition);
            PlayerController.ChangeToState<MouseCreateHeroState>();
        }
    }

    public static void PlaceHeroOnTile(Tile tile) {
        SpawnedHero.Spawn(tile);
        tile.Occupy(SpawnedHero);
        SpawnedHero = null;
    }

    public static void DeleteInstantiatedHero() {
        SpawnedHero.Destroy();
        SpawnedHero = null;
    }

    public void Destroy() {
        EventSystem<CreateHeroEvent>.UnregisterListener(OnCreateHero);
        if (SpawnedHero != null) {
            SpawnedHero.Destroy();
            SpawnedHero = null;
        }
    }

}
