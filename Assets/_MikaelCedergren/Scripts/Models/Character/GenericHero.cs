using UnityEngine;
using Object = UnityEngine.Object;

public class GenericHero<T> : Hero {

    public GenericHero(Vector3 position) {
        GameObject = Factory.CreateInstance<T>(position, Quaternion.identity);
        FactoryObject factoryObject = Factory.FetchWeaponDataOfType<T>();
        if (factoryObject is FactoryObject_Character characterData) {
            WeaponData = characterData.WeaponData;
        }
        weapon = Factory.CreateInstance<IWeapon>(WeaponData.FetchType(WeaponData.WeaponType), WeaponData);
        Transform = GameObject.transform;
        IsAlive = false;
        Animator = GameObject.GetComponent<Animator>();
    }

    public override void Hover() {
        Animator.SetBool("IsHovered", true);
    }
    public override void Unhover() {
        Animator.SetBool("IsHovered", false);
    }

    public override void Select() {
        SelectedHero = this;
        Animator.SetBool("IsSelected", true);
        Tile.IsPreview(true);
        ShowPath();
    }
    public override void Deselect() {
        Animator.SetBool("IsSelected", false);
        Tile.IsPreview(false);
        HidePath();
    }

    private void ShowPath() {
        commandInvoker.Preview();
    }

    private void HidePath() {
        commandInvoker.RevokePreview();
    }

    public override bool IsPlacementValid(Vector3 position) {
        Tile tile = Tile.GetTileAt(position);
        if (tile == null) {
            return false;
        }
        return tile.IsOccupied() == false;
    }

    protected override void OnSpawn() {
        Animator.SetTrigger("Spawn");
    }

    public override void OnDestroy() {
        if (IsAlive == false) {
            Despawn();
        }
        else {
            Animator.SetTrigger(AnimationName.QuickDeath);
            Object.Destroy(GameObject, 3f);
            SelectedHero = null;
        }

    }

    private void Despawn() {
        Animator.SetTrigger("Despawn");
        Object.Destroy(GameObject, 1.25f);
    }

    public override string ToString() {
        return typeof(T).Name + ", " + Position;
    }

    public override void TakeDamage(AttackData attackData) { }

}
