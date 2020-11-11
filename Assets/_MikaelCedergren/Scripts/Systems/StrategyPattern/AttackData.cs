using UnityEngine;

[CreateAssetMenu(fileName = "AttackData_", menuName = "StrategyPattern/Attack Data")]
public class AttackData : ScriptableObject {

    public GameObject AttackProjectileVisual;
    public BaseCharacter AttackTarget { get; private set; }
    public int Damage { get; set; }

    public void SetAttackTarget(BaseCharacter target) {
        AttackTarget = target;
    }
    public void SetAttackDamage(int damage) {
        Damage = damage;
    }

}
