using Godot;
using System;

public partial class Hurdle : Area3D
{
	[Export]
	private int _hurdleDamage = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.BodyEntered += this.BodyEnteredHandler;

	}

	private void BodyEnteredHandler(Node3D body)
	{
		Player player = body as Player;
		if (player != null)
		{
			player.DamagePlayer(this._hurdleDamage);
		}
		QueueFree();
	}
	
}
