using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameSaver
{
    private int _currentLevel;

    public GameSaver()
    {
        I = this;
    }

    public void Load()
    {
        _currentLevel = PlayerPrefs.GetInt("Level", 0);
    }

    public void FinishLevel(int level)
    {
        if (level > _currentLevel)
        {
            _currentLevel = level;
            PlayerPrefs.SetInt("Level", _currentLevel);
        }
    }

    public int CurrentLevel 
    {
        get { return _currentLevel; }
    }

    public static GameSaver I { get; private set; }
}
