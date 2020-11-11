public class Weapon_Bow : Weapon {
    
    public Weapon_Bow(WeaponData weaponData) : base(weaponData) { }

    public override void Fire(AttackData attackData) {
        attackData.AttackTarget.TakeDamage(attackData);
    }
}