using UnityEngine;

public class AnimAttackEvent : IEvent {
    public Animator Animator;

    public AnimAttackEvent(Animator animator) {
        Animator = animator;
    }
}