public class Weapon_Axe : Weapon {

    private int Damage;

    public Weapon_Axe(WeaponData weaponData) : base(weaponData) { }

    public override void Fire(AttackData attackData) {
        attackData.AttackTarget.TakeDamage(attackData);
    }
}
