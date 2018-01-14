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
        mobTypes = new string[3];
        mobTypes[0] = "fly";
        mobTypes[1] = "swim"; 
        mobTypes[2] = "walk";

        randomGenerator = new Random();
        mobAnimation = GetNode("AnimatedSprite") as AnimatedSprite;
        int typeIndex = randomGenerator.Next(0,mobTypes.Length-1);
        mobAnimation.Animation = mobTypes[typeIndex]; 
    }

    public void OnVisibilityNotifier2DScreenExited()
    {
        QueueFree();
    }
}
