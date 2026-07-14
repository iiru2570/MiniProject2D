using UnityEngine;

public class PlayerAttackState : IPlayerState
{

    private PlayerController player;
    private float attackTime;
    private float attackDuration;

    public PlayerAttackState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        attackTime = 0f;
        attackDuration = 0.2f;
        player.animeController.SetAttacktrue();
        player.Attack(player.facingDir);
    }
    public void Update()
    {
        attackTime += Time.deltaTime;
        if(attackTime >= attackDuration)
        {
            player.ChangeState(player.idleState);
        }
    }

    public void Exit()
    {
        player.animeController.SetAttackfalse();
    }

 
}
