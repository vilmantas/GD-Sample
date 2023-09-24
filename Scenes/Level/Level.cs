using Godot;
using System;

public partial class Level : Node3D
{
	Random random = new Random();
	
	private Node3D _groundContainer;
	private Node3D _hurdlesContainer;
	
	private PackedScene groundScene;
	private PackedScene hurdleScene;

	[Export] 
	private int levelStartX = -10;
	
	[Export] 
	private int startingAreaLength = 15;

	[Export]
	private int minimumHurdleInterval = 5;
	
	[Export]
	private int maximumHurdleInterval = 5;
	
	private int lastGeneratedX;
	private int nodesSinceHurdle = 0;
	
	
	
	public override void _Ready()
	{
		groundScene = (PackedScene)ResourceLoader.Load("res://Scenes/Ground/ground.tscn");
		hurdleScene = (PackedScene)ResourceLoader.Load("res://Scenes/Hurdle/hurdle.tscn");
		_groundContainer = GetNode<Node3D>("GroundContainer");
		_hurdlesContainer = GetNode<Node3D>("HurdleContainer");
		GenerateLevel();
	}
	
	private void GenerateLevel()
	{
		GenerateStartingArea();
		for (int i = lastGeneratedX + 1; i <= 1000; i++)
		{
			GenerateNode(i);
		}
	}

	private void GenerateStartingArea()
	{
		lastGeneratedX = startingAreaLength - Math.Abs(levelStartX);
		for (int i = levelStartX; i <= lastGeneratedX; i++)
		{
			CreateGround(i, 0);
		}
		
		for (int i = 1; i <= 3; i++)
		{
			CreateGround(levelStartX, i);
		}
	}

	private void CreateGround(int x, int y)
	{
		StaticBody3D newGround = (StaticBody3D)groundScene.Instantiate();
		newGround.Position = new Vector3(x, y, 0);
		_groundContainer.AddChild(newGround);
	}

	private void GenerateNode(int x)
	{
		CreateGround(x, 0);
		bool canGenerateHurdle = nodesSinceHurdle > minimumHurdleInterval;
		if (!canGenerateHurdle)
		{
			nodesSinceHurdle += 1;
			return;
		}
		bool generateHurdle = (nodesSinceHurdle >= maximumHurdleInterval) || random.NextDouble() >= 0.7;
		if (!generateHurdle)
		{
			nodesSinceHurdle += 1;
			return;
		}

		GenerateHurdle(x, random.NextDouble() >= 0.5);

	}

	private void GenerateHurdle(int x, bool groundHurdle)
	{
		nodesSinceHurdle = 0;
		Hurdle newHurdle = (Hurdle)hurdleScene.Instantiate();
		newHurdle.Position = new Vector3(x, groundHurdle ? 1 : 2.5f, 0);
		_hurdlesContainer.AddChild(newHurdle);

	}


}
	