using UnityEngine;

public abstract class BaseCharacter {
    public bool IsAlive { get; protected set; }

    protected int HealthPoints = 1;

    protected BaseCharacterState state;
    protected BaseCharacterState newState;
    protected BaseCharacterState oldState;
    protected bool switchToNewState = false;

    public GameObject GameObject { get; protected set; }
    public Transform Transform { get; protected set; }
    public Animator Animator { get; protected set; }
    public Tile Tile { get; set; }

    protected IWeapon weapon;
    public WeaponData WeaponData { get; protected set; }


    public Vector3 Position {
        get => Transform.position;
        set => Transform.position = value;
    }

    public T ChangeToState<T>() where T : BaseCharacterState {
        if (state != null && state.GetType() == typeof(T)) {
            return state as T;
        }

        if (switchToNewState == true && newState.GetType() == typeof(T)) {
            return newState as T;
        }

        switchToNewState = true;

        if (state != null) {
            state.Exit();
        }
        oldState = state;

        newState = Factory.CreateInstance<T>();
        //newState.Enter(this);
        return newState as T;
    }

    public abstract void Destroy();
    public abstract void Hover();
    public abstract void Unhover();
    public abstract void Select();
    public abstract void Deselect();
    public abstract void Spawn(Tile tile);
    public abstract void TakeDamage(AttackData attackData);

    public AttackData Attack(BaseCharacter target) {
        Debug.Log("weapondata.damage " + WeaponData.Damage);
        AttackData attackData = WeaponData.AttackData;
        attackData.SetAttackTarget(target);
        attackData.SetAttackDamage(WeaponData.Damage);
        weapon.Fire(attackData);
        return attackData;
    }
    protected void OnAttack(Enemy enemy) { }



}
