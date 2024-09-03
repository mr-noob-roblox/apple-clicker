using Godot;
using System;
using System.Numerics;

public partial class Apple : TextureButton
{
    private int count = 0;
    private int angle = 0;
    private float extraScaleLeft = 0;
    private float angleScale = 0;
    private float milestoneFade = 0;

    public override void _Ready()
    {
        GD.Randomize();
    }
    public override void _Process(double delta)
    {
        if (angle >= 360)
        {
            angle = 0;
        }
        
        angle += 1;
        Rotation = Mathf.DegToRad(Mathf.Sin(Mathf.DegToRad(angle))*10);
        
        if (angleScale > 0)
        {
            angleScale -= 0.15f;
        }

        if (extraScaleLeft > 0)
        {
            extraScaleLeft = Mathf.Sin(angleScale * Mathf.Pi / 2)*0.9f;
        }

        if (milestoneFade > 0)
        {
            milestoneFade -= 0.01f;
        }

        Scale = new Godot.Vector2(4 + extraScaleLeft, 4 + extraScaleLeft);
        GetTree().Root.GetNode("Main").GetNode<TextureRect>("Milestone").Modulate = new Color(1,1,1,milestoneFade);
    }
    private void OnButtonPressed()
    {
        count += 1;

        // funi milestone thing
        if (count == 1000)
        {
            milestoneFade = 1;
            GetNode<AudioStreamPlayer>("MilestoneReached").Play();
        }

        var randomNum = GD.Randi() % 2;

        if (randomNum == 1)
        {
            GetNode<AudioStreamPlayer>("AppleClick").Play();
        } else {
            GetNode<AudioStreamPlayer>("AppleClickAlt").Play();
        }

        extraScaleLeft = 0.7f;
        angleScale = 1;
        GetTree().Root.GetNode<Node2D>("Main").GetNode<Label>("counter").Text = count.ToString();
    }
}
