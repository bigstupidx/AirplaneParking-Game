using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class StateMainMenu : StateBase
{
    public static bool ShouldShowMenuAd = true;

    public StateMainMenu()
        : base("mainMenu")
    {
    }


    protected override void OnActivate()
    {
        base.OnActivate();
        
        EventController.I.Subscribe("PlayPressed", this);
        EventController.I.Subscribe("MoreGames", this);

        UIRoot.I.GetView<MainMenuView>().SetVisible(true);

        if (ShouldShowMenuAd)
        {
            AdMobAndroidEventListener.Instance.Request();
            AdMobAndroidEventListener.Instance.DontShow();
            ShouldShowMenuAd = false;
        }
        AdMobAndroid.hideBanner(false);

        GA.I.LogScreen("Main Menu");
    }

    protected override void OnDeactivate()
    {
        base.OnDeactivate();
        EventController.I.Unsubscribe("PlayPressed", this);
        UIRoot.I.GetView<MainMenuView>().SetVisible(false);
    }

    public override void OnEvent(string EventName, GameObject Sender)
    {
        base.OnEvent(EventName, Sender);

        Debug.Log(EventName);
        if (EventName == "PlayPressed")
        {
            AppRoot.I.SetState(new LevelSelectState());
            AdMobAndroidEventListener.Instance.ShowAd();
        }
        if (EventName == "MoreGames")
        {
            Application.OpenURL("https://play.google.com/store/apps/developer?id=i6+Games");
        }
    }
}
