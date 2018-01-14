using Godot;
using System;
using GodotCSTools;

public class MainScene : Node
{
    [Export]
    private PackedScene mob;
    private int score;
    private Random randomGenerator;
    private PlayerObject player;
    private Position2D startPosition;
    private Timer startTimer;
	private Timer scoreTimer;
	private Timer mobTimer;
    private PathFollow2D mobSpawnLocation;
    private HUDObject hud;

    public override void _Ready()
    {
        randomGenerator = new Random();
        player = GetNode("Player") as PlayerObject;
        startPosition = GetNode("StartPosition") as Position2D;
        startTimer = GetNode("StartTimer") as Timer;
		scoreTimer = GetNode("ScoreTimer") as Timer;
		mobTimer = GetNode("MobTimer") as Timer;
        mobSpawnLocation = GetNode("MobPath").GetNode("MobSpawnLocation") as PathFollow2D;
        hud = GetNode("HUD") as HUDObject;

        hud.Connect("StartGame",this, "NewGame");
        player.Connect("Hit",this, "GameOver");
    }

    public void NewGame()
    {
        GD.Print("NewGame()");
        score = 0;
        player.Start(startPosition.Position);
        startTimer.Start();
        hud.ShowMessage("Get Ready");
        hud.UpdateScore(score);
    }
	
	public void GameOver()
	{
        GD.Print("GameOver()");
		scoreTimer.Stop();
		mobTimer.Stop();
        hud.GameOver();
	}

    public void OnStartTimerTimeout()
    {
        GD.Print("OnStartTimerTimeout()");
        mobTimer.Start();
        scoreTimer.Start();
    }

    public void OnScoreTimerTimeout()
    {
        GD.Print("OnScoreTimerTimeout()");
        score += 1;
        hud.UpdateScore(score);
    }

    public void OnMobTimerTimeout()
    {
        mobSpawnLocation.SetOffset(randomGenerator.Next());
        MobObject mobObject = (MobObject)mob.Instance();
        AddChild(mobObject);
        var direction = mobSpawnLocation.GetRotation();
        mobObject.SetPosition(mobSpawnLocation.GetPosition());
        double a = -Math.PI/4.0f;
        double b = Math.PI/4.0f;
        GD.Print("a:" + a);
        GD.Print("b:" + b);
//        direction += (float)randomGenerator.Next((int)a, (int)b);
        direction += (float)RandomRange(a, b);
        mobObject.SetRotation(direction);
        Vector2 linearVelocity = new Vector2(randomGenerator.Next(mobObject.minSpeed, mobObject.maxSpeed), 0);
        mobObject.SetLinearVelocity(linearVelocity.Rotated(direction));
    }

    private double RandomRange(double fromValue, double toValue)
    {
        return randomGenerator.NextDouble() * (toValue - fromValue) + fromValue;
    }
}
