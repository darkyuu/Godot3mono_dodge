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

    public override void _Ready()
    {
        randomGenerator = new Random();
        player = GetNode("Player") as PlayerObject;
        startPosition = GetNode("StartPosition") as Position2D;
        startTimer = GetNode("StartTimer") as Timer;

        NewGame();
    }

    private void NewGame()
    {
        score = 0;
        player.Start(startPosition.Position);
        startTimer.Start();
    }

//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}
