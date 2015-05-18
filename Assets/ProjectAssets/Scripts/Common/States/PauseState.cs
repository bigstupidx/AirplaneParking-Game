using UnityEngine;

public  class PauseState:StateBase
{
    protected PauseView View { get; private set; }

    public PauseState() 
        : base("")
    {
    }

    protected override void OnActivate()
    {
        base.OnActivate();
        View = UIRoot.I.GetView<PauseView>();
        EventController.I.Subscribe("ResumeClicked", this);
        EventController.I.Subscribe("RestartClicked", this);
        EventController.I.Subscribe("MainMenuClicked", this);
        View.SetVisible(true);

        View.Resume.gameObject.SetActive(true);
        View.Restart.gameObject.SetActive(true);
        View.NextLevel.gameObject.SetActive(false);
        View.ExitToMainMenu.gameObject.SetActive(true);

        View.TopText.text = "Pause";

        Time.timeScale = 0;

        GA.I.LogScreen("Pause");
    }

    protected override void OnDeactivate()
    {
        base.OnDeactivate();
        View.SetVisible(false);
        EventController.I.Unsubscribe("ResumeClicked", this);
        EventController.I.Unsubscribe("RestartClicked", this);
        EventController.I.Unsubscribe("MainMenuClicked", this);
        Time.timeScale = 1;
    }

    public override void OnEvent(string EventName, GameObject Sender)
    {
        base.OnEvent(EventName, Sender);

        if (EventName == "ResumeClicked")
        {
            AppRoot.I.SetState(new PlayState());
        }
        if (EventName == "RestartClicked")
        {
            UIRoot.I.GetView<LoadingView>().SetVisible(true);
            //force the reload of scene
            AppRoot.I.SetState(new PlayState(true, true));
        }
        if (EventName == "MainMenuClicked")
        {
            AppRoot.I.SetState(new StateMainMenu());
        }
    }
}
