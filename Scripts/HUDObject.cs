using Godot;
using System;
using GodotCSTools;

public class HUDObject : CanvasLayer
{
    [Signal("StartGame")]
    public delegate void StartGame();
    private Label messageLabel;
    private Label scoreLabel;
    private Timer messageTimer;
    private Button startButton;

    public override void _Ready()
    {
        this.SetupNodeTools();

        messageLabel = GetNode("MessageLabel") as Label;
        scoreLabel =  GetNode("ScoreLabel") as Label;
        messageTimer = GetNode("MessageTimer") as Timer;
        startButton = GetNode("StartButton") as Button;
    }

    public void ShowMessage(string textString)
    {
        messageLabel.Text = textString;
        messageLabel.Show();
        messageTimer.Start();
    }

    public void GameOver()
    {
        ShowMessage("Game Over");
        //TODO: find C# api:  yield(messageTimer, "Timeout");
        startButton.Show();
        messageLabel.Text = "Dodge the\nCreeps!";
        messageLabel.Show();  
    }

    public void UpdateScore(int score)
    {
        scoreLabel.Text = score.ToString();
    }

    public void OnMessageTimerTimeout()
    {
        messageLabel.Hide();
    }

    public void OnStartButtonPressed()
    {
        startButton.Hide();
        this.EmitSignal<StartGame>();
    }
}
