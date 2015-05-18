
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectState:StateBase
{
    private readonly List<GameObject> _levels = new List<GameObject>();
    private LevelSelectView _view;


    public LevelSelectState() 
        : base("")
    {
    }

    protected override void OnActivate()
    {
        base.OnActivate();
        _view = UIRoot.I.GetView<LevelSelectView>();
        _view.SetVisible(true);

        EventController.I.Subscribe("LevelSelected", this);
        EventController.I.Subscribe("SelectLeft", this);
        EventController.I.Subscribe("SelectRight", this);
        EventController.I.Subscribe("BackPressed", this);

        CreateLevels(0, 10);
        CreateLevels(10, 10);

        SelectFirstPart();

        GA.I.LogScreen("Level Select");
    }

    private void CreateLevels(int offset, int count)
    {
        for (int i = offset; i < count + offset; i++)
        {
            GameObject go;
            if (GameSaver.I.CurrentLevel >= i)
            {
                go = _view.LevelPrefab.Create();
            }
            else
            {
                go = _view.LockedLevel.Create();
            }

            go.transform.parent = _view.LevelsRoot;
            go.GetComponentInChildren<Text>().text = (i+1).ToString();
            _levels.Add(go);
        }
    }



    protected override void OnDeactivate()
    {
        base.OnDeactivate();

        EventController.I.Unsubscribe("LevelSelected", this);
        EventController.I.Unsubscribe("SelectLeft", this);
        EventController.I.Unsubscribe("SelectRight", this);
        EventController.I.Unsubscribe("BackPressed", this);

        foreach (var gameObject in _levels)
        {
            GameObject.Destroy(gameObject);
        }

        _levels.Clear();

        _view.SetVisible(false);
    }

    public override void OnEvent(string EventName, GameObject Sender)
    {
        base.OnEvent(EventName, Sender);

        if (EventName == "LevelSelected")
        {
            LevelControl.CurrentLevel =int.Parse(Sender.GetComponentInChildren<Text>().text) - 1;
            UIRoot.I.GetView<LoadingView>().SetVisible(true);
            AdMobAndroid.hideBanner(true);
            AppRoot.I.SetState(new PlayState(false, true), false);
            StateMainMenu.ShouldShowMenuAd = true;
        }
        if (EventName == "SelectLeft")
        {
            SelectFirstPart();
        }
        if (EventName == "SelectRight")
        {
            SelectSecondPart();
        }
        if (EventName == "BackPressed")
        {
            AppRoot.I.SetState(new StateMainMenu());
        }
    }

    private void SelectSecondPart()
    {
        SelectLevels(10, 10);
        _view.RightButton.gameObject.SetActive(false);
        _view.LeftButton.gameObject.SetActive(true);
    }

    private void SelectFirstPart()
    {
        SelectLevels(0, 10);
        _view.RightButton.gameObject.SetActive(true);
        _view.LeftButton.gameObject.SetActive(false);
    }

    private void SelectLevels(int offset, int count)
    {
        for (int i = 0; i < _levels.Count; i++)
        {
            _levels[i].SetActive(false);
        }
        for (int i = offset; i < offset + count; i++)
        {
            _levels[i].SetActive(true);
        }
    }
}
