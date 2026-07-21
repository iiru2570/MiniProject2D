using UnityEngine;

public class PlayerSkillState : IPlayerState
{
    private PlayerController player;
    private int skillNum;

    public PlayerSkillState(PlayerController player, int skillNum)
    {
        this.player = player;
        this.skillNum = skillNum;
    }

    public void Enter()
    {
        player.StartCoroutine(player.AttackAnime());
        if(skillNum == 1)
        {
            player.Skill1();
        }
        if (skillNum == 2)
        {
            player.Skill2();
        }


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
