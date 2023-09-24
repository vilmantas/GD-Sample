using Godot;
using System;
using Gdsample.Features.PlayerAnimator;

public partial class GameManager : Node
{
    public override void _Input(InputEvent @event)
    {
        if (@event.AsText() == "W")
        {
	        var node = GetParent().GetNode<PlayerAnimationManager>("player_test_animations");
            
        	if (@event.IsPressed())
        	{
        		node.SetWalking();				
        	}
        	else
        	{
        		node.SetIdle();
        	}
        }
    }
}
