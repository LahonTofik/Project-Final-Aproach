using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Collections.Generic;
using System.Threading;

public class MyGame : Game {
    bool _stepped = false;
    bool _paused = false;
    int _stepIndex = 0;
    int _startSceneNumber = 0;

    Canvas _lineContainer = null;

    List<Ball> _movers;
    List<LineSegment> _lines;
    EasyDraw congrats;
	Font rowdies;

    public int GetNumberOfLines()
    {
        return _lines.Count;
    }

    public LineSegment GetLine(int index)
    {
        if (index >= 0 && index < _lines.Count)
        {
            return _lines[index];
        }
        return null;
    }

    public int GetNumberOfMovers()
    {
        return _movers.Count;
    }

    public Ball GetMover(int index)
    {
        if (index >= 0 && index < _movers.Count)
        {
            return _movers[index];
        }
        return null;
    }
    public MyGame() : base(800, 600, false)     // Create a window that's 800x600 and NOT fullscreen
    {

        _movers = new List<Ball>();
        _lines = new List<LineSegment>();
        // Draw some things on a canvas:
        EasyDraw canvas = new EasyDraw(800, 600);
        canvas.Clear(Color.MediumPurple);
        canvas.Fill(Color.Yellow);
        canvas.Ellipse(width / 2, height / 2, 200, 200);
        canvas.Fill(50);
        canvas.TextSize(32);
        canvas.TextAlign(CenterMode.Center, CenterMode.Center);
        canvas.Text("Welcome!", width / 2, height / 2);

        // Add the canvas to the engine to display it:
        AddChild(canvas);
        Console.WriteLine("MyGame initialized");

    }
    void StepThroughMovers()
    {
        if (_stepped)
        { // move everything step-by-step: in one frame, only one mover moves
            _stepIndex++;
            if (_stepIndex >= _movers.Count)
            {
                _stepIndex = 0;
            }
            if (_movers[_stepIndex].moving)
            {
                _movers[_stepIndex].Step();
            }
        }
        else
        { // move all movers every frame
            for (int i = _movers.Count - 1; i >= 0; i--)
            {
                if (_movers[i].moving)
                {
                    _movers[i].Step();
                }
            }
        }
    }

    void Update()
    {
        if (!_paused)
        {
            StepThroughMovers();
        }
    }

    // For every game object, Update is called every frame, by the engine:

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
}