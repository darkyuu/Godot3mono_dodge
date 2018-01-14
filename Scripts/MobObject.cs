using Godot;
using System;

public class MobObject : RigidBody2D
{
    [Export]
    public int minSpeed;
    [Export]
    public int maxSpeed;
    private string[] mobTypes;
    private AnimatedSprite mobAnimation;
    private Random randomGenerator;

    public override void _Ready()
    {
        mobTypes[0] = "fly";
        mobTypes[1] = "swim"; 
        mobTypes[2] = "walk";

        randomGenerator = new Random();

        mobAnimation = GetNode("AnimatedSprite") as AnimatedSprite;
        mobAnimation.Animation = mobTypes[randomGenerator.Next(0,mobTypes.Length-1)]; 
    }

    public void OnVisibilityNotifier2DScreenExited()
    {
        QueueFree();
    }
}
