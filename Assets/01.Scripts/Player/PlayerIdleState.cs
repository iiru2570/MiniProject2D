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
        if(player.isDead == true)
        {
            return;
        }

        if (Keyboard.current.upArrowKey.isPressed)
        {
            if(player.facingDir != new Vector3Int(0, 1, 0))
            {
                player.facingDir = new Vector3Int(0, 1, 0);
                player.animeController.SetUp();
            }
            if (!pressShift)
            {
                player.ChangeState(player.moveState);
            }
        }
        else if (Keyboard.current.downArrowKey.isPressed)
        {
           
            if (player.facingDir != new Vector3Int(0, -1, 0))
            {
                player.facingDir = new Vector3Int(0, -1, 0);
                player.animeController.SetDown();
            }
            if (!pressShift)
            {
                player.ChangeState(player.moveState);
            }
        }
        else if (Keyboard.current.leftArrowKey.isPressed)
        {
            if(player.facingDir != new Vector3Int(-1, 0, 0))
            {
                player.facingDir = new Vector3Int(-1, 0, 0);
                player.animeController.SetLeft();
            }
            if (!pressShift)
            {
                player.ChangeState(player.moveState);
            }
        }
        else if (Keyboard.current.rightArrowKey.isPressed)
        {
            if(player.facingDir != new Vector3Int(1, 0, 0))
            {
                player.facingDir = new Vector3Int(1, 0, 0);
                player.animeController.SetRight();
            }
            if (!pressShift)
            {
                player.ChangeState(player.moveState);
            }
        }
        else if (Keyboard.current.spaceKey.wasPressedThisFrame && player.canAttack)
        {
            player.ChangeState(player.attackState);
        }
        else if(Keyboard.current.digit1Key.wasPressedThisFrame && player.canAttack)
        {
            player.ChangeState(player.skill1State);
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame && player.canSkill2)
        {
            player.ChangeState(player.skill2State);
        }
        else if(Keyboard.current.digit3Key.wasPressedThisFrame && player.canSkill3)
        {
            player.ChangeState(player.skill3State);
        }
    }

    public void Exit()
    {
        
    }

}
