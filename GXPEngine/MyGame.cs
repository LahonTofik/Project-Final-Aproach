using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Collections.Generic;
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions
using System.Threading;

public class MyGame : Game
{
    PlayerBall player;
    public int moves = 35;
    public List<Turret> turret;
    bool _stepped = false;
    Vec2 MousePos;
    bool _paused = false;
    int _stepIndex = 0;
    int _startSceneNumber = 0;
    public int currentLevel = 0; // levels start from level 0
    public string[] levels = new string[4]; // amount of levels is 1

    public bool nextLevel = false;

    public EasyDraw colliderHolder;

    private SoundChannel backgroundMusic;
    private Sound respawnSound = new Sound("Assets/sfc_game_start_and_respawn.wav");

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
    public void SetCurrentLevel(int _value)
    {
        currentLevel = _value;
        if (currentLevel >= 4) { currentLevel = 0; }
        LoadLevel(levels[currentLevel]);
    }

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
    public void AddLine(Vec2 start, Vec2 end, bool specialCol = false, bool twosided = false, bool addcaps = false)
    {
        if (colliderHolder == null)
        {
            colliderHolder = new EasyDraw(10, 10, false);
        }
        LineSegment line = new LineSegment(start, end, specialCol ? 0xffff00ff : 0xff00ff00, 4);
        AddChild(line);
        _lines.Add(line);

        if (twosided) AddLine(end, start, true, false); // :-)
    }

    Canvas _lineContainer = null;
    public MyGame() : base(3840, 1216, false, false, 1920, 1200, false)
    {
        _movers = new List<Ball>();
        _lines = new List<LineSegment>();
        turret = new List<Turret>();
        levels[0] = "Assets/Start.tmx";
        levels[1] = "Assets/Level1.tmx";
        levels[2] = "Assets/Level2.tmx";
        levels[3] = "Assets/Level3.tmx";
        targetFps = 60;
        LoadLevel(levels[0]);
        player = FindObjectOfType<PlayerBall>();
        Ball.acceleration.SetXY(0, 0.75f);
        backgroundMusic = new Sound("Assets/game_music_idea1.wav", true).Play();
        backgroundMusic.Volume = 0.2f;
    }

    public List<Ball> _movers;
    public List<LineSegment> _lines;
    EasyDraw congrats;
    Font rowdies;
    void DestroyAllLevels()
    {
        foreach (Ball mover in _movers)
        {
            mover.Destroy();
        }
        _movers.Clear();
        foreach (LineSegment lines in _lines)
        {
            lines.Destroy();
        }
        _lines.Clear();
        foreach (Turret turret in turret)
        {
            turret.Destroy();
        }
        List<GameObject> children = GetChildren();
        foreach (GameObject child in children)
        {
            child.Destroy();
        }
    }
    public void DrawLine(Vec2 start, Vec2 end)
    {
        _lineContainer.graphics.DrawLine(Pens.White, start.x, start.y, end.x, end.y);
    }

    public void AddMover(Ball bullet)
    {
        _movers.Add(bullet);
        AddChild(bullet);
    }
    public void AddPlayer(PlayerBall bullet)
    {
        _movers.Add(bullet);
        AddChild(bullet);
    }

    private void LoadLevel(string name)
    {
        respawnSound.Play();
        nextLevel = false;
        DestroyAllLevels();
        Level level = new Level(name);
        AddChild(level);
        //player = FindObjectOfType<Player>(); /*for when we add a player*/
    }

    // For every game object, Update is called every frame, by the engine:
    void Update()
    {
        if (Input.GetKeyUp(Key.P))
        {
            if (currentLevel <= 3)
            {
                currentLevel++;
                LoadLevel(levels[currentLevel]);
            }
        }
        if (Input.GetKeyUp(Key.N))
        {
            if (currentLevel != 0)
            {
                currentLevel--;
                LoadLevel(levels[currentLevel]);
            }
        }
        if (nextLevel)
        {
            currentLevel++;
            LoadLevel(levels[currentLevel]);
            nextLevel = false;
        }
        if (currentLevel == 0 && Input.GetMouseButton(0))
        {
            nextLevel = true;
        }

        if (!_paused)
        {
            StepThroughMovers();
        }
        if (Input.GetMouseButtonDown(1))
        {
            MousePos.SetXY(Input.mouseX, Input.mouseY);
            AddMover(new Rock(5, MousePos, 0, default, 5, new Vec2(0, 0.5f)));
            Console.WriteLine("ball made");
        }
    }

    public void RemoveMover(Ball ball)
    {
        _movers.Remove(ball);
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
    // For every game object, Update is called every frame, by the engine:

    static void Main()                          // Main() is the first method that's called when the program is run
    {
        new MyGame().Start();
    }
}