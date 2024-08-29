
using UnityEngine;
using Fusion;

public class MixamoAnimController : NetworkBehaviour
{
    private string animParameter = "MoveSpeed";
    private Animator animator;
    private PlayerMovement player;
    private float isMoving = 0;

    public void Awake()
    {
        isMoving = 0;
        player = FindObjectOfType<PlayerMovement>();
        if (player == null)
        {
            Debug.LogError("Couldn't get GameManager");
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Couldn't get Animator component");
        }
    }
    public override void Render()
    {
        if (player != null && animator != null)
        {
            isMoving = player.animSpeed;
            Debug.Log(isMoving);
            animator.SetFloat(animParameter, isMoving);
            //Debug.Log($"Animation 'isMoving' set to: {isMoving}");
        }
    }
}