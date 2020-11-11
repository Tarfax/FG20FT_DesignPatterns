using MC_Utility.EventSystem;
using MCUtility.Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacterState : BaseCharacterState {
    private Animator animator;

    private Queue<MoveToCommand> commands;
    private MoveToCommand currentCommand;
    private Tile nextTile;
    private Tile endTile;

    private Vector3 VDirection = Vector3.zero;

    private bool isAttacking;

    private Pathfinding path;

    protected override void OnEnter() {
        animator = baseCharacter.Animator;
        isAttacking = false;

        if (SetPath() == false) {
            if (currentCommand.HasAttackCommand() == true) {
                Attack();
                return;
            }
            else {
                baseCharacter.ChangeToState<ReceivingCommandState>();
                return;
            }
        }

        animator.SetBool(AnimationName.IsWalking, true);
        SetSpeed();
    }

    protected override void OnExit() {
        animator.SetBool(AnimationName.IsWalking, false);
        CleanUpAnimations();
    }

    public void MoveTo(MoveToCommand moveToCommand) {
        if (commands == null) {
            commands = new Queue<MoveToCommand>();
        }
        commands.Enqueue(moveToCommand);
    }

    protected override void OnUpdate(float deltaTime) {
        if (isAttacking == true) {
            return;
        }

        float distance = Vector3.Distance(baseCharacter.Position, nextTile.Position);
        if (distance < 0.12f) {
            HasReachedNextTile(deltaTime);
        }
    }

    private bool SetPath() {
        if (commands.Count > 0) {
            currentCommand = commands.Dequeue();
            endTile = currentCommand.Tile;
            if (endTile == null || endTile == baseCharacter.Tile) {
                return false;
            }

            path = new Pathfinding(baseCharacter.Tile, endTile, baseCharacter);
            path.PreviewPath();
            nextTile = path.Next();
            if (nextTile != null || nextTile != endTile || nextTile != baseCharacter.Tile) {
                return true;
            }
        }

        return false;
    }

    private void HasReachedNextTile(float deltaTime) {
        Tile currentTile = baseCharacter.Tile;
        currentTile.Occupy(null);
        currentTile.PreviewPath(false);

        nextTile.Occupy(baseCharacter);
        baseCharacter.Tile = nextTile;
        if (nextTile == endTile) {
            HasReachedDestination();
            return;
        }

        nextTile = path.Next();
        if (nextTile != null) {
            SetSpeed();
        }
    }

    private void HasReachedDestination() {

        if (currentCommand.HasAttackCommand() == true) {
            Attack();
            return;
        }

        if (SetPath() == false) {
            path = null;
            baseCharacter.ChangeToState<ReceivingCommandState>();
            return;
        }
        animator.SetBool(AnimationName.IsWalking, true);
        SetSpeed();
    }


    private void SetSpeed() {
        VDirection = nextTile.Position - baseCharacter.Tile.Position;

        if (VDirection.z != 0f) {
            if (VDirection.z > 0f) {
                CleanUpAnimations();
                animator.SetBool(AnimationName.MoveForward, true);
            }
            else {
                CleanUpAnimations();
                animator.SetBool(AnimationName.MoveBackward, true);
            }

        }
        else if (VDirection.x != 0) {
            if (VDirection.x > 0f) {
                CleanUpAnimations();
                animator.SetBool(AnimationName.MoveRight, true);
            }
            else {
                CleanUpAnimations();
                animator.SetBool(AnimationName.MoveLeft, true);
            }

        }
    }

    private void CleanUpAnimations() {
        animator.SetBool(AnimationName.MoveForward, false);
        animator.SetBool(AnimationName.MoveBackward, false);
        animator.SetBool(AnimationName.MoveLeft, false);
        animator.SetBool(AnimationName.MoveRight, false);
    }

    private void Attack() {
        animator.SetBool(AnimationName.IsWalking, false);
        animator.SetTrigger(AnimationName.Attack);
        isAttacking = true;
        EventSystem<AnimAttackEvent>.RegisterListener(OnAnimAttackEvent);
        CleanUpAnimations();
    }

    private void OnAnimAttackEvent(AnimAttackEvent animAttackEvent) {
        if (animAttackEvent.Animator == animator) {
            baseCharacter.Attack(currentCommand.Enemy);
            EventSystem<AnimAttackEvent>.UnregisterListener(OnAnimAttackEvent);

            if (SetPath() == true) {
                animator.SetBool(AnimationName.IsWalking, true);
                isAttacking = false;
                SetSpeed();
            }
            else {
                baseCharacter.ChangeToState<ReceivingCommandState>();
            }
        }
    }

}
