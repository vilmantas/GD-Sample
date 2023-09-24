using Godot;
using System;

public partial class UI : Control
{
	private Player _player;

	private Label _playerHealthLabel;

	private ProgressBar _playerHealthBar;
	
	public override void _Ready()
	{
		this._playerHealthLabel = GetNode<Label>("PlayerHealthBar/PlayerHealthLabel");
		this._playerHealthBar = GetNode<ProgressBar>("PlayerHealthBar");
		SetPlayer();
	}

	
	public override void _Process(double delta)
	{
		SetPlayer();
	}
	
	private void SetPlayer()
	{
		if (this._player != null)
			return;
	
		var player = GetTree().GetFirstNodeInGroup("Player") as Player ?? null;
		if (player == null)
			return;
		
		this._player = player;
		this.OnPlayerSet();
		
	}

	private void OnPlayerSet()
	{
		OnPlayerHealthChange(this._player.Health, this._player.MaxHeath);
		this._player.OnHealthChange += OnPlayerHealthChange;
	}

	private void OnPlayerHealthChange(int currentHealth, int maxHealth)
	{
		this._playerHealthLabel.Text = currentHealth + "/" + maxHealth;
		this._playerHealthBar.MaxValue = maxHealth;
		this._playerHealthBar.Value = currentHealth;
	}
	
}
