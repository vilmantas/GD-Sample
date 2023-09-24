using Godot;

namespace Gdsample.Features.PlayerAnimator;

public partial class PlayerAnimationManager : Node3D
{
    public void SetWalking()
    {
        var node = GetNode<AnimationTree>("AnimationTree");
        
        node.Set("parameters/Transition/transition_request", "Walking");
    }

    public void SetIdle()
    {
        var node = GetNode<AnimationTree>("AnimationTree");
        
        node.Set("parameters/Transition/transition_request", "Idle");
    }
}