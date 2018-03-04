using Godot;
using System;
using System.Threading.Tasks;


public class HUDObject : CanvasLayer
{
	[Signal]
    public delegate void StartGame();
	
    private Label messageLabel;
    private Label scoreLabel;
    private Godot.Timer messageTimer;
    private Button startButton;

    public override void _Ready()
    {
        messageLabel = GetNode("MessageLabel") as Label;
        scoreLabel =  GetNode("ScoreLabel") as Label;
        messageTimer = GetNode("MessageTimer") as Godot.Timer;
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
        ShowMainMenuHUDAfterGameOverMessageIsGone();
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
        this.EmitSignal("StartGame");
    }

    private async void ShowMainMenuHUDAfterGameOverMessageIsGone()
    {
        await ToSignal(messageTimer, "timeout");
        startButton.Show();
        messageLabel.Text = "Dodge the\nCreeps!";
        messageLabel.Show();
    }
}
