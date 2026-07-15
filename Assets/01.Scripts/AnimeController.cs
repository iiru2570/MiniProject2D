using UnityEngine;

public class AnimeController : MonoBehaviour
{

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

    public void SetMovetrue()
    {
        animator.SetBool("isMove", true);
    }
    public void SetMovefalse()
    {
        animator.SetBool("isMove", false);
    }
    public void SetAttacktrue()
    {
        animator.SetBool("isAttack", true);
    }
    public void SetAttackfalse()
    {
        animator.SetBool("isAttack", false);
    }
    public void SetIdletrue()
    {
        animator.SetBool("isIdle", true);
    }
    public void SetIdlefalse()
    {
        animator.SetBool("isIdle", false);
    }
}
