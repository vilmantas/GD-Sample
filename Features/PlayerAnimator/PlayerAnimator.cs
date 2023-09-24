using Godot;
using System;

public partial class PlayerAnimator : AnimationTree
{
	public override void _Input(InputEvent @event)
	{
		// if (@event.AsText() == "W")
		// {
		// 	if (@event.IsPressed())
		// 	{
		// 		Set("parameters/Transition/transition_request", "Walking");				
		// 	}
		// 	else
		// 	{
		// 		Set("parameters/Transition/transition_request", "Idle");
		// 	}
		// }
	}
}
