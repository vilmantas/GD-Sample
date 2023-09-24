using Godot;
using System;

public partial class PlayerAnimator : AnimationTree
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.AsText() == "W")
		{
			if (@event.IsPressed())
			{
				Set("parameters/Transition/transition_request", "Walking");				
			}
			else
			{
				Set("parameters/Transition/transition_request", "Idle");
			}
            
			
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
