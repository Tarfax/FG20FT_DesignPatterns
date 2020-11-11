using MC_Utility.EventSystem;
using UnityEngine;

public class Brute_Anim_Attack : StateMachineBehaviour {

    public float eventTime = 0.24f;
    private float eventTimer = 0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        eventTimer = eventTime;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (animator.IsInTransition(layerIndex) == false) {
            eventTimer -= Time.deltaTime;
            if (eventTimer <= 0f) {
                EventSystem<AnimAttackEvent>.FireEvent(Factory.CreateInstance<AnimAttackEvent>(animator));
            }
        }
    }

}
