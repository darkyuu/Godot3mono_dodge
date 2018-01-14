using Godot;
using System;
using GodotCSTools;

public class PlayerObject : Area2D
{
    [Signal]
    public delegate void Hit();
    [Export]
    private int speed = 400;
    private Vector2 velocity;
    private Vector2 screenSize;
    private bool monitoring;    
    private AnimatedSprite playerAnimation;
    private CollisionShape2D collision;
    private Particles2D trail;

    public override void _Ready()
    {
        this.SetupNodeTools();

        collision = GetNode("CollisionShape2D") as CollisionShape2D;
        playerAnimation = GetNode("AnimatedSprite") as AnimatedSprite;
        trail = GetNode("Trail") as Particles2D;

        Hide();
        screenSize = GetViewportRect().Size;
    }

    public override void _Process(float delta)
    {
        velocity = new Vector2();
        if (Input.IsActionPressed("ui_right"))
            velocity.x += 1;
        if (Input.IsActionPressed("ui_left"))
            velocity.x -= 1;
        if (Input.IsActionPressed("ui_down"))
            velocity.y += 1;
        if (Input.IsActionPressed("ui_up"))
            velocity.y -= 1;

        if(velocity.Length() > 0.0f)
        {
            playerAnimation.Play();
            trail.Emitting = true;
            velocity = velocity.Normalized() * speed;
        }
        else
        {
            playerAnimation.Stop();
            trail.Emitting = false;
        }

        Position += velocity * delta; 
        SetPosition(new Vector2(Mathf.Clamp(Position.x, 0 , screenSize.x), Mathf.Clamp(Position.y, 0 , screenSize.y)));

        if (velocity.x != 0)
        {
            playerAnimation.Animation = "right";
            playerAnimation.FlipV = false;
            playerAnimation.FlipH = velocity.x < 0;
        }
        else if (velocity.y != 0)
        {
            playerAnimation.Animation = "up";
            playerAnimation.FlipV = velocity.y > 0;
        }
    }

    public void OnPlayerBodyEntered(Area2D area)
    {
        collision.Disabled = true;
        Hide();
        this.EmitSignal<Hit>();
    }

    public void Start(Vector2 pos)
    {
        SetPosition(pos);
        Show();
        collision.Disabled = false;
    }
}