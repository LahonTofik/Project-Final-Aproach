using System;                                   // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Collections.Generic;
using System.Drawing;                           // System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game {
	public int currentLevel = 0; // levels start from level 0
	public string[] levels = new string[1];	// amount of levels is 1

	public bool nextLevel = false;

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

	public MyGame() : base(800, 600, false)     
	{
		levels[0] = "TestLevel.tmx";

		LoadLevel(levels[0]);

	}

	void DestroyAllLevels()
	{
		List<GameObject> children = GetChildren();
		foreach (GameObject child in children)
		{
			child.Destroy();
		}
	}

	private void LoadLevel(string name)
	{
		nextLevel = false;
		DestroyAllLevels();
		Level level = new Level(name);
		AddChild(level);
		//player = FindObjectOfType<Player>(); /*for when we add a player*/
	}

	// For every game object, Update is called every frame, by the engine:
	void Update() {
		// Empty
	}

	static void Main()                         
	{
		new MyGame().Start();                   
	}
}