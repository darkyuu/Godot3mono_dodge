using Godot;
using System;

public class PlayerObject : Area2D
{

/*     [Signal]
    private void hit;
 */
    [Export]
    private int speed = 400;
    private Vector2 velocity;
    private Vector2 screenSize;
    private bool monitoring;    
    
    public AnimatedSprite playerAnimation;

    public override void _Ready()
    {
        AddUserSignal("Hit");
        Hide();
        screenSize = GetViewportRect().Size;
        playerAnimation = GetNode("AnimatedSprite") as AnimatedSprite;
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
            velocity = velocity.Normalized() * speed;
        }
        else
        {
            playerAnimation.Stop();
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
        Hide();
        EmitSignal("Hit");
        CallDeferred("set_monitoring", false);
    }

    public void Start(Vector2 pos)
    {
        SetPosition(pos);
        Show();
        monitoring = true;
    }
}
