using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon_", menuName = "StrategyPattern/Weapon Definition")]
public class WeaponData : ScriptableObject {

    public WeaponType WeaponType;

    public AttackData AttackData;
    [Range(1, 10)] public int Damage = 1;
    [Range(1f, 100f)] public float ProjectileSpeed = 1f;

    private void OnValidate() {
        ProjectileSpeed = Mathf.RoundToInt(ProjectileSpeed * 10f) / 10f;
    }

    public static Type FetchType(WeaponType weaponType) {
        switch (weaponType) {
            default:
            case WeaponType.Melee:
                return typeof(Weapon_Axe);
            case WeaponType.Ranged:
                return typeof(Weapon_Bow);
        }
    }

}