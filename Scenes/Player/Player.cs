using Godot;
using System;

public partial class Player : CharacterBody3D
{
	// Variables
	private const float Speed = 5.0f;
	private const float JumpVelocity = 5f;
	private int _maxHealth = 3;
	private int _health = 3;
	public int Health => _health;
	public int MaxHeath => _maxHealth;

	private bool _isCrouching = false;
	
	// Nodes
	private CollisionShape3D _playerCollider;
	private MeshInstance3D _playerMesh;
	
	// Signals
	
	[Signal]
	public delegate void PlayerDiedEventHandler();

	[Signal]
	public delegate void OnHealthChangeEventHandler(int health, int maxHealth);
	
	

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

		// Handle Crouch.
		if (Input.IsActionJustPressed("crouch"))
		{
			this._isCrouching = true;
			Vector3 crouchingScale = new Vector3(1f, 0.5f, 1f);
			this.Scale = crouchingScale;
		}

		if (Input.IsActionJustReleased("crouch"))
		{
			this._isCrouching = false;
			Vector3 nonCrouchingScale = new Vector3(1f, 1f, 1f);
			this.Scale = nonCrouchingScale;
		}
		
		// Handle Jump.
		if (!_isCrouching && Input.IsActionJustPressed("jump") && IsOnFloor())
			velocity.Y = JumpVelocity;
		
		// Handle Movement
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
		this.OnPlayerHealthChange();
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
		this.OnPlayerHealthChange();
		
	}

	private void OnPlayerHealthChange()
	{
		EmitSignal(SignalName.OnHealthChange, this._health, this._maxHealth);
		GD.Print("Current health: " + this._health + "/" + this._maxHealth);
	}
	
}
