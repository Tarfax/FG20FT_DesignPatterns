using UnityEngine;
using UnityEngine.Animations;

public class Brute_Anim_StepSide_R : StateMachineBehaviour {

    Tile tile;
    Transform transform;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        transform = animator.transform;
        tile = Tile.GetTileAt(Mathf.RoundToInt(transform.position.x + 1f), Mathf.RoundToInt(transform.position.z));
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Implement code that processes and affects root motion
        Vector3 deltaPosition = animator.deltaPosition;
        if (animator.IsInTransition(layerIndex) == false) {
            float desiredZPosition = tile.Position.z - transform.position.z;
            deltaPosition.z += Mathf.Lerp(0f, desiredZPosition, Time.deltaTime);
            transform.position += deltaPosition;
        }
        else {
            //deltaPosition.z = 0f;
            //transform.position += deltaPosition;
        }

    }

}
