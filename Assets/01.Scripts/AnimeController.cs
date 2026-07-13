using UnityEngine;

public class AnimeController : MonoBehaviour
{

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void SetUp()
    {
        animator.SetBool("isUp", true);
        animator.SetBool("isDown", false);
        animator.SetBool("isRight", false);
        animator.SetBool("isLeft", false);
    }
    public void SetDown()
    {
        animator.SetBool("isUp", false);
        animator.SetBool("isDown", true);
        animator.SetBool("isRight", false);
        animator.SetBool("isLeft", false);
    }
    public void SetRight()
    {
        animator.SetBool("isUp", false);
        animator.SetBool("isDown", false);
        animator.SetBool("isRight", true);
        animator.SetBool("isLeft", false);
    }
    public void SetLeft()
    {
        animator.SetBool("isUp", false);
        animator.SetBool("isDown", false);
        animator.SetBool("isRight", false);
        animator.SetBool("isLeft", true);
    }

    public void SetRuntrue()
    {
        animator.SetBool("isRun", true);
    }
    public void SetRunfalse()
    {
        animator.SetBool("isRun", false);
    }
    public void SetAttacktrue()
    {
        animator.SetBool("isAttack", true);
    }
    public void SetAttackfalse()
    {
        animator.SetBool("isAttack", false);
    }
}
