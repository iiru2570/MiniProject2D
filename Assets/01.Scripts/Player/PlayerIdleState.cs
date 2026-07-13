using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : IPlayerState
{

    public PlayerController player;

    public PlayerIdleState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        
    }

    public void Update()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            player.facingDir = new Vector3Int(0, 1, 0);
            player.animeController.SetUp();
            player.ChangeState(player.runState);
        }
        else if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            player.facingDir = new Vector3Int(0, -1, 0);
            player.animeController.SetDown();
            player.ChangeState(player.runState);
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            player.facingDir = new Vector3Int(-1, 0, 0);
            player.animeController.SetLeft();
            player.ChangeState(player.runState);
        }
        else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            player.facingDir = new Vector3Int(1, 0, 0);
            player.animeController.SetRight();
            player.ChangeState(player.runState);
        }
        else if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            player.ChangeState(player.attackState);
        }
    }

    public void Exit()
    {
        
    }

}
