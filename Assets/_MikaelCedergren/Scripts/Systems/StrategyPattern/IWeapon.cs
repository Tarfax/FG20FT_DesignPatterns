using System;
using UnityEngine;

public interface IWeapon 
{
    WeaponType WeaponType { get; }
    void Initialize(Transform _);
    void Update();
    void Fire(AttackData attackDat);

}
