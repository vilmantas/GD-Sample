using Godot;
using System;

public partial class Player : CharacterBody3D
{
	// Variables
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;
	private int _maxHealth = 3;
	private int _health = 3;
	
	// Nodes
	private CollisionShape3D _playerCollider;
	private MeshInstance3D _playerMesh;
	
	// Signals
	
	[Signal]
	public delegate void PlayerDiedEventHandler();
	
	

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	private float _gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public override void _Ready()
	{
		this._playerCollider = GetNode<CollisionShape3D>("PlayerCollider");
		this._playerMesh = GetNode<MeshInstance3D>("PlayerMesh");
		base._Ready();
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= _gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity.Y = JumpVelocity;
		
		// Handle Crouch.
		if (Input.IsActionJustPressed("crouch"))
		{
			Vector3 crouchingScale = new Vector3(1f, 0.5f, 1f);
			this._playerMesh.Scale = crouchingScale;
			this._playerCollider.Scale = crouchingScale;
		}

		if (Input.IsActionJustReleased("crouch"))
		{
			Vector3 nonCrouchingScale = new Vector3(1f, 1f, 1f);
			this._playerMesh.Scale = nonCrouchingScale;
			this._playerCollider.Scale = nonCrouchingScale;
		}
		

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("left", "right", "up", "down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, 0)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public void DamagePlayer(int damageAmount)
	{
		int newHealth = this._health - damageAmount;

		if (newHealth <= 0)
		{
			this._health = 0;
			EmitSignal(SignalName.PlayerDied);
		}
		else
		{
			this._health = newHealth;
		}
	}
	
	public void HealPlayer(int healAmount)
	{
		int newHealth = this._health + healAmount;

		if (newHealth >= this._maxHealth)
		{
			this._health = _maxHealth;
		}
		else
		{
			this._health = newHealth;
		}
	}
}
