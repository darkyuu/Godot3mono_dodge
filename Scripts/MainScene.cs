using Godot;
using System;

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
    private AudioStreamPlayer deathSound;
    private AudioStreamPlayer music;

    public override void _Ready()
    {
        player = GetNode("Player") as PlayerObject;
        startPosition = GetNode("StartPosition") as Position2D;
        startTimer = GetNode("StartTimer") as Timer;
        scoreTimer = GetNode("ScoreTimer") as Timer;
        mobTimer = GetNode("MobTimer") as Timer;
        mobSpawnLocation = GetNode("MobPath").GetNode("MobSpawnLocation") as PathFollow2D;
        hud = GetNode("HUD") as HUDObject;
        deathSound = GetNode("DeathSound") as AudioStreamPlayer;
        music = GetNode("Music") as AudioStreamPlayer;

        hud.Connect("StartGame",this, "NewGame");
        player.Connect("Hit",this, "GameOver");

        randomGenerator = new Random();
    }

    public void NewGame()
    {
        score = 0;
        hud.UpdateScore(score);
        player.Start(startPosition.Position);
        startTimer.Start();
        hud.ShowMessage("Get Ready");
        music.Play();
    }
	
	public void GameOver()
	{
        deathSound.Play();
        music.Stop();
        scoreTimer.Stop();
        mobTimer.Stop();
        hud.GameOver();
	}

    public void OnStartTimerTimeout()
    {
        mobTimer.Start();
        scoreTimer.Start();
    }

    public void OnScoreTimerTimeout()
    {
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
        direction += (float)RandomRange(-Math.PI/4.0, Math.PI/4.0);
        mobObject.SetRotation(direction);
        Vector2 linearVelocity = new Vector2(randomGenerator.Next(mobObject.minSpeed, mobObject.maxSpeed), 0);
        mobObject.SetLinearVelocity(linearVelocity.Rotated(direction));
    }

    private double RandomRange(double fromValue, double toValue)
    {
        return randomGenerator.NextDouble() * (toValue - fromValue) + fromValue;
    }
}
