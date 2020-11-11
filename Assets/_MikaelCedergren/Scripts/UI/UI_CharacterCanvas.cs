using UnityEngine;

public class UI_CharacterCanvas : MonoBehaviour {

    private RectTransform rectTransform;
    private new Camera camera;
    public Transform parent;
    [SerializeField] private Canvas canvas;

    public Vector3 offset = new Vector3(1f,1f,0.5f);

    public Vector2 xOffset = new Vector2(1.3f, 0.5f);
    public Vector2 yOffset = new Vector2(1.3f, 0.5f);
    public Vector2 zOffset = new Vector2(1.3f, 0.5f);

    public float alphaValueX;
    public float alphaValueY;
    public float alphaValueZ;

    public float parentX;

    private int width;
    private int height;
    void Start() {
        camera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
        canvas.worldCamera = camera;
        width = GameInstance.Width;
        height = GameInstance.Height;
        parent = canvas.transform.parent;
    }

    void Update() {
        parentX = parent.position.x;
        alphaValueX = (parent.position.x + 0.001f) / width;
        alphaValueY = (parent.position.y + 0.001f) / height;
        alphaValueZ = (parent.position.z + 0.001f) / height;
        float he = Screen.height / 1080f;
        float xValue = Mathf.Lerp(xOffset.x, xOffset.y, alphaValueX);
        float yValue = Mathf.Lerp(yOffset.x, yOffset.y, alphaValueY);
        float zValue = Mathf.Lerp(zOffset.x, zOffset.y, alphaValueZ);
        Vector3 position = new Vector3(parent.position.x + xValue + he, parent.position.y + yValue + he, parent.position.z + zValue + he);
        rectTransform.anchoredPosition = camera.WorldToScreenPoint(position, Camera.MonoOrStereoscopicEye.Mono);
    }

}
