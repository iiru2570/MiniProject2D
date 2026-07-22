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
        if(skillNum == 1)
        {
            player.StartCoroutine(player.AttackAnime());
            player.Skill1();
        }
        if (skillNum == 2)
        {
            player.StartCoroutine(player.AttackAnime());
            player.Skill2();
        }
        if(skillNum == 3)
        {
            //힐 애니메이션 넣어야함.
            player.Skill3();
        }


        player.ChangeState(player.idleState);
    }
    public void Update()
    {
       
    }

    public void Exit()
    {

    }

}
