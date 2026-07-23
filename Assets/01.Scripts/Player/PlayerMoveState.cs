using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveState : IPlayerState
{
    private PlayerController player;
    private Vector3 playerPos;
    private Vector3 movePos;
    private float moveTime;


    public PlayerMoveState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.animeController.SetMovetrue();
        moveTime = 0f;
        playerPos = player.transform.position;
        movePos = player.GetWorldPos(player.facingDir, 0.3f);


        if (StageManager.instance.IsUsedPos(movePos))
        {
            player.animeController.SetMovefalse();
            player.ChangeState(player.idleState);
            return;
        }
        StageManager.instance.ReturnPos(playerPos);
    }

    public void Update()
    {
        //if (Keyboard.current.spaceKey.wasPressedThisFrame && player.canAttack)
        //{
        //    //player.transform.position = movePos;
        //    //player.ChangeState(player.attackState);
        //    player.StartCoroutine(player.AttackAnime());
        //    player.Attack(player.facingDir);
        //    return;
        //}
        //if (Keyboard.current.digit3Key.wasPressedThisFrame && player.canSkill3)
        //{
        //    player.ChangeState(player.skill3State);
        //    return;
        //}
        moveTime += Time.deltaTime * player.moveSpeed;
        player.transform.position = Vector3.Lerp(playerPos, movePos, moveTime);

        if(moveTime >= 1f)
        {
            //프레임 간격때문에 조금씩 캐릭터가 벗어날 수도 있어서 마지막에 다시 보완.
            player.transform.position = movePos;
            player.ChangeState(player.idleState);
        }

    }
    public void Exit()
    {
        player.animeController.SetMovefalse();
    }

    
}
