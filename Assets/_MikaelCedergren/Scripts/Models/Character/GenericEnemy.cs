using MC_Utility.EventSystem;
using TMPro;
using UnityEngine;

public class GenericEnemy<T> : Enemy {

    private Color defaultColor = new Color(0.533f, 0.363f, 1f);
    private Material material;
    private GameObject textMeshGameObject;
    private TextMeshPro textMeshPro;
    private SpriteRenderer textMeshProBackground;
    private float canvasTimer;

    public GenericEnemy(Vector3 position) {
        HealthPoints = 3;
        GameObject = Factory.CreateInstance<T>(position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
        Transform = GameObject.transform;
        IsAlive = false;
        Animator = GameObject.GetComponent<Animator>();
        material = GameObject.GetComponentInChildren<MeshRenderer>().material;
        textMeshPro = GameObject.GetComponentInChildren<TextMeshPro>();
        textMeshGameObject = textMeshPro.gameObject;
        textMeshProBackground = textMeshGameObject.GetComponentInChildren<SpriteRenderer>();
        textMeshGameObject.transform.SetParent(Transform);
        SetText("HP: " + HealthPoints);
        textMeshGameObject.SetActive(false);
    }

    private void SetText(string text) {
        textMeshPro.text = text;
        textMeshProBackground.size = new Vector2(textMeshPro.preferredWidth + 2.5f, textMeshPro.preferredHeight + 2.5f);
    }

    public override void Hover() {
        material.color = Color.green;
        textMeshGameObject.SetActive(true);
    }

    public override void Unhover() {
        material.color = defaultColor;
        textMeshGameObject.SetActive(false);

    }

    public override void Select() { }

    public override void Deselect() { }

    protected override void OnSpawn() { }

    public override void TakeDamage(AttackData attackData) {
        HealthPoints -= attackData.Damage;
        if (Animator != null) {
            Animator.SetTrigger(AnimationName.TakeDamage);
            canvasTimer = 3f;
            textMeshGameObject.SetActive(true);
            SetText(attackData.Damage + " damage");
            EventSystem<UpdateEvent>.RegisterListener(Update);
        }
        if (HealthPoints < 0) {

        }
    }

    private void Update(UpdateEvent updateEvent) {
        canvasTimer -= updateEvent.DeltaTime;
        textMeshGameObject.SetActive(true);

        if (canvasTimer <= 0f) {
            SetText("HP: " + HealthPoints);
            textMeshGameObject.SetActive(false);
            EventSystem<UpdateEvent>.UnregisterListener(Update);
        }
    }


}