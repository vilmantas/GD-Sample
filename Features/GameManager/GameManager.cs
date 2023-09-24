using Godot;
using System;
using Gdsample.Features.PlayerAnimator;

public partial class GameManager : Node
{
	private PlayerAnimationManager _playerAnimationManager;
    
	public override void _Ready()
	{
		_playerAnimationManager = GetParent().GetNode<PlayerAnimationManager>("player_test_animations");
	}

	public override void _Input(InputEvent @event)
    {
        if (@event.AsText() == "W")
        {
        	if (@event.IsPressed())
        	{
		        _playerAnimationManager.SetWalking();				
        	}
        	else
        	{
		        _playerAnimationManager.SetIdle();
        	}
        }

        if (Input.IsActionJustPressed("run"))
        {
	        _playerAnimationManager.SetRunning();
        }

        if (Input.IsActionJustReleased("run"))
        {
	        _playerAnimationManager.SetWalking();
        }
    }
}
