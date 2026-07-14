using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : IPlayerState
{

    public PlayerController player;
    private bool pressShift;

    public PlayerIdleState(PlayerController player)
    {
        this.player = player;
    }

    public void Enter()
    {
        
    }

    public void Update()
    {
        pressShift = Keyboard.current.leftShiftKey.isPressed;

        if (Keyboard.current.upArrowKey.isPressed)
        {
            player.facingDir = new Vector3Int(0, 1, 0);
            player.animeController.SetUp();
            if (!pressShift)
            {
                player.ChangeState(player.runState);
            }
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {
            player.facingDir = new Vector3Int(0, -1, 0);
            player.animeController.SetDown();
            if (!pressShift)
            {
                player.ChangeState(player.runState);
            }
        }
        else if (Keyboard.current.leftArrowKey.isPressed)
        {
            player.facingDir = new Vector3Int(-1, 0, 0);
            player.animeController.SetLeft();
            if (!pressShift)
            {
                player.ChangeState(player.runState);
            }
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            player.facingDir = new Vector3Int(1, 0, 0);
            player.animeController.SetRight();
            if (!pressShift)
            {
                player.ChangeState(player.runState);
            }
        }
        else if (Keyboard.current.spaceKey.wasPressedThisFrame && player.canAttack)
        {
            player.ChangeState(player.attackState);
        }
    }

    public void Exit()
    {
        
    }

}
