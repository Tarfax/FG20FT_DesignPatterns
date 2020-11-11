using UnityEngine;

public class Brute_Anim_StepForward : StateMachineBehaviour {

    Tile tile;
    Transform transform;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        transform = animator.transform;
        tile = Tile.GetTileAt(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z + 1));
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // Implement code that processes and affects root motion
        Vector3 deltaPosition = animator.deltaPosition;
        if (animator.IsInTransition(layerIndex) == false) {
            float desiredXPosition = tile.Position.x - transform.position.x;
            deltaPosition.x += Mathf.Lerp(0f, desiredXPosition, Time.deltaTime);
            transform.position += deltaPosition;
        }
        else {
            //deltaPosition.x = 0f;
            //transform.position += deltaPosition;

        }
    }


}
