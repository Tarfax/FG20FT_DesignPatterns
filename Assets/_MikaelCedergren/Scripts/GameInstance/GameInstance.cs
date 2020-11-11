using FutureGamesLib;
using MC_Utility.EventSystem;
using UnityEngine;

public class GameInstance : MonoBehaviour {
    private float firstHalf = 0.95f;
    private float vector2Up = -1.7f;
    private float screenHeight = 0.5f;
    private Vector2 controlCenter;

    [Header("World Settings")]
    [SerializeField] private int width = 10;
    [SerializeField] private int height = 10;
    [SerializeField] private int spawnTileEachFrame = 10;
    public static int Width { get; private set; }
    public static int Height { get; private set; }

    private InputSystem inputSystem;
    private HeroSpawner characterSpawner;
    private EnemySpawner enemySpawner;
    private PlayerController playerController;

    private UpdateEvent updateEvent;

    private void OnEnable() {
        inputSystem = new InputSystem(Camera.main);
        playerController = new PlayerController();
        characterSpawner = new HeroSpawner();
        enemySpawner = new EnemySpawner();
    }

    private void Awake() {
        Width = width;
        Height = height;
    }

    void Start() {
        CreateTiles();
        updateEvent = EventSystem<UpdateEvent>.CreateEvent();
    }

    private void Update() {
        updateEvent.DeltaTime = Time.deltaTime;
        EventSystem<UpdateEvent>.FireEvent(updateEvent);
    }

    private void OnDisable() {
        inputSystem.Destroy();
        characterSpawner.Destroy();
        enemySpawner.Destroy();
    }

    private void OnValidate() {
        controlCenter = firstHalf * new Vector2(Screen.width, Screen.height) + Vector2.up * vector2Up * Screen.height * screenHeight;
    }

    private void OnGUI() {
        RemakeWorldButton();
    }

    private void RemakeWorldButton() {
        if (GUI.Button(
            RectsLib.RectByCenterAndSize(
                controlCenter + Vector2.down * FutureGamesLib.Constants.UI.margin * 2.5f,
                FutureGamesLib.Constants.UI.RectSize * 0.9f), "Remake Tiles") == false)
            return;

        CreateTiles();
    }

    private void CreateTiles() {
        new TileSpawner(width, height, spawnTileEachFrame);
    }

}
