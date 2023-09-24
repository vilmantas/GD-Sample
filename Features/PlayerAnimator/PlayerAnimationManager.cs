using Godot;

namespace Gdsample.Features.PlayerAnimator;

public partial class PlayerAnimationManager : Node3D
{
    private AnimationTree _animationTree;
    
    public override void _Ready()
    {
        _animationTree = GetNode<AnimationTree>("AnimationTree");
    }

    public void SetWalking()
    {
        _animationTree.Set("parameters/Transition/transition_request", "Walking");
    }

    public void SetIdle()
    {
        _animationTree.Set("parameters/Transition/transition_request", "Idle");
    }
    
    public void SetRunning()
    {
        _animationTree.Set("parameters/Transition/transition_request", "Running");
    }
    
    public void SetCrouching()
    {
        _animationTree.Set("parameters/Transition/transition_request", "Crouching");
    }
}