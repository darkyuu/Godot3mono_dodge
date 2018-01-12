using Godot;
using System;

public class MobObject : RigidBody2D
{
    [Export]
    private int minSpeed;
    [Export]
    private int maxSpeed;
    private string[] mobTypes = ["fly", "swim", "walk"];
    private AnimatedSprite mobAnimation;
    private Random randomGenerator;

    public override void _Ready()
    {
        randomGenerator = new Random();

        mobAnimation = GetNode("AnimatedSprite") as AnimatedSprite;
        mobAnimation.Animation = mobTypes[randomGenerator.Next(0,mobTypes.Length-1)]; 
    }

    public void OnVisibilityNotifier2DScreenExited()
    {
        QueueFree();
    }

//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}
