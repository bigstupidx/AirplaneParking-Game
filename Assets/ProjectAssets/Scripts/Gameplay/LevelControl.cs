using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.ProjectAssets.Scripts.Common.States;

public class LevelControl : MonoBehaviour
{

    public List<Level> Levels;
    public Transform Player;
    public Transform Trailer;

    private bool _failed;
    private bool _won;

    public static int CurrentLevel;
	void Awake ()
	{
	    I = this;

	    if (CurrentLevel >= Levels.Count)
	    {
	        CurrentLevel = 0;
	    }
	    Levels[CurrentLevel].gameObject.SetActive(true);
	    Player.transform.position = Levels[CurrentLevel].StartPos.position;
        Player.transform.rotation = Levels[CurrentLevel].StartPos.rotation;

        Trailer.transform.position = Levels[CurrentLevel].StartPos.position;
        Trailer.transform.rotation = Levels[CurrentLevel].StartPos.rotation;
	}
	
	void Update () {
	
	}

    public void FinishLevel()
    {
        if (!_failed && !_won)
        {
            Debug.Log("Win");
            GameSaver.I.FinishLevel(CurrentLevel + 1);
            CurrentLevel++;
            AppRoot.I.SetState(new LevelWinState(), false);
            _won = true;
        }
    }

    public void FailLevel(bool timerEnded = false)
    {
        if (!_failed && !_won)
        {
            Debug.Log("Fail");
            AppRoot.I.SetState(new LoseState(timerEnded), false);
            _failed = true;
        }
    }

    public static LevelControl I { get; private set; }
}
