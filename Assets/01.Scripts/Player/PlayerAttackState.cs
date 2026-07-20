using System.Collections;
using UnityEngine;

public class PlayerAttackState : IPlayerState
{

    private PlayerController player;

    public PlayerAttackState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.StartCoroutine(player.AttackAnime());
        player.Attack(player.facingDir);
        player.ChangeState(player.idleState);
    }
    public void Update()
    {
        //attackTime += Time.deltaTime;
        //if(attackTime >= attackDuration)
        //{
        //    player.ChangeState(player.idleState);
        //}
    }

    public void Exit()
    {
        
    }

}
