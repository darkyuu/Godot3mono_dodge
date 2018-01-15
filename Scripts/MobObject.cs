using Godot;
using System;

public class MobObject : RigidBody2D
{
    [Export]
    public int minSpeed;
    [Export]
    public int maxSpeed;
    private static readonly string[] mobTypes = { "fly", "swim", "walk"};
    private AnimatedSprite mobAnimation;
    private Random randomGenerator;

    public override void _Ready()
    {
        mobAnimation = GetNode("AnimatedSprite") as AnimatedSprite;

        randomGenerator = new Random();
        int typeIndex = randomGenerator.Next(0,mobTypes.Length-1);
        mobAnimation.Animation = mobTypes[typeIndex]; 
    }

    public void OnVisibilityNotifier2DScreenExited()
    {
        QueueFree();
    }
}
