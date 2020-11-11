using MC_Utility.EventSystem;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hero : BaseCharacter {
    protected static List<Hero> heroes;
    public static Hero SelectedHero { get; protected set; }

    protected CommandInvoker commandInvoker;

    public Hero() {
        commandInvoker = new CommandInvoker();

    }

    public override void Destroy() {
        if (heroes.Contains(this) == true) {
            heroes.Remove(this);
            if (heroes.Count == 0) {
                heroes = null;
            }
        }
        if (state != null) {
            state.Exit();
        }
        OnDestroy();
        if (Tile != null) {
            Tile.Occupy(null);
            Tile.IsPreview(false);
        }
        Transform = null;
        Tile = null;
        GameObject = null;
        IsAlive = false;
        commandInvoker.Destroy();
        EventSystem<UpdateEvent>.UnregisterListener(Update);
        EventSystem<EndTurnEvent>.UnregisterListener(EndTurn);
    }
    public virtual void OnDestroy() { }

    abstract public bool IsPlacementValid(Vector3 position);
    public override void Spawn(Tile tile) {
        heroes.Add(this);
        IsAlive = true;
        Tile = tile;

        OnSpawn();
        ChangeToState<ReceivingCommandState>();

        EventSystem<UpdateEvent>.RegisterListener(Update);
        EventSystem<EndTurnEvent>.RegisterListener(EndTurn);
    }
    protected virtual void OnSpawn() { }

    public void PlayerHit() {
        Animator.SetTrigger(AnimationName.QuickHit);
    }

    public void IssueMoveToCommand(Tile tile) {
        ICommand command = Factory.CreateInstance<MoveHeroCommand>(this, tile);
        commandInvoker.Add(command);
        commandInvoker.Preview();
    }

    public void IssueHitEnemyCommand(Enemy enemy, Tile tile) {
        ICommand command = Factory.CreateInstance<AttackCommand>(this, tile, enemy);
        commandInvoker.Add(command);
        commandInvoker.Preview();
    }

    public void UndoMovement() {
        commandInvoker.Undo();
        Animator.SetTrigger("Undo");
    }

    public void RedoMovement() {
        commandInvoker.Redo();
    }

    private void EndTurn(EndTurnEvent endTurnEvent) {
        commandInvoker.Execute();
    }

    private void Update(UpdateEvent updateEvent) {
        if (IsAlive == true) {
            if (switchToNewState == true) {
                state = newState;
                state.Enter(this);
                switchToNewState = false;
            }
            state.Update(updateEvent.DeltaTime);
            OnUpdate(updateEvent);
        }
    }
    protected virtual void OnUpdate(UpdateEvent updateEvent) { }

    public static IEnumerable<Hero> GetHeroes() {
        foreach (var item in heroes) {
            yield return item;
        }
    }

    public static Hero GetHeroAt(Vector3 position) {
        if (heroes == null) {
            heroes = new List<Hero>();
        }
        foreach (Hero hero in heroes) {
            Tile tile = Tile.GetTileAt(position);
            if (tile != null) {
                if (hero == tile.GetHero() && hero.IsAlive == true) {
                    return hero;
                }
            }
        }
        return null;
    }

}
