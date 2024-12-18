using UnityEditor.Experimental.Rendering;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private string animJump = "Jump";
    private string animSliding = "Sliding";
    private string animHit = "isHit";
    private string animRun = "isRun";
    private string restart = "Restart";
    public void SetAnimJump()
    {
        animator.SetTrigger(animJump);
    }
    public void SetAnimSliding()
    {
        animator.SetTrigger(animSliding);
    }
    public void SetAnimHit()
    {
        animator.SetTrigger(animHit);
    }
    public void SetAnimRun()
    {
        animator.SetTrigger(animRun);
    }
    public void SetRestart()
    {
        animator.SetTrigger(restart);
    }
}
