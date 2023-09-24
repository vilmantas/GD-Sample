using Godot;
using System;

public partial class Hurdle : Area3D
{
	[Export]
	private int _hurdleDamage = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Hurdle ready");
		this.BodyEntered += (body =>
		{
			Player player = body as Player;
			if (player != null)
			{
				player.DamagePlayer(this._hurdleDamage);
			}
		});
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
}
