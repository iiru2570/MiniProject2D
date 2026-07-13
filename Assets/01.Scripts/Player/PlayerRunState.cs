using UnityEngine;

public class PlayerRunState : IPlayerState
{
    private PlayerController player;
    private Vector3 playerPos;
    private Vector3 movePos;
    private float moveTime;


    public PlayerRunState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        player.animeController.SetRuntrue();
        moveTime = 0f;
        playerPos = player.transform.position;
        movePos = player.Move(player.facingDir, 0.3f);
    }

    public void Update()
    {
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
        player.animeController.SetRunfalse();
    }

    
}
