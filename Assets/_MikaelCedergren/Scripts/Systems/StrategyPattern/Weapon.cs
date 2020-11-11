using UnityEngine;

public abstract class Weapon : IWeapon {

    protected WeaponData weaponData;
    public WeaponType WeaponType => weaponData.WeaponType;

    public Weapon(WeaponData weaponData) {
        this.weaponData = weaponData;
    }

    public abstract void Fire(AttackData attackDat);

    public void Initialize(Transform hand) {
        OnInitialize();
    }
    protected virtual void OnInitialize() { }

    public void Update() {
        OnUpdate();
    }

    protected virtual void OnUpdate() { }

}
