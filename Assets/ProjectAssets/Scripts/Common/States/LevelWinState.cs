using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.ProjectAssets.Scripts.Common.States
{
    class LevelWinState:PauseState
    {
        protected override void OnActivate()
        {
            base.OnActivate();

            AdMobAndroidEventListener.Instance.Request();
            AdMobAndroidEventListener.Instance.ShowAd(true);


            View.Resume.gameObject.SetActive(false);
            View.Restart.gameObject.SetActive(false);
            if (LevelControl.I.Levels.Count > LevelControl.CurrentLevel)
            {
                View.NextLevel.gameObject.SetActive(true);
            }
            else
            {
                View.NextLevel.gameObject.SetActive(false);
            }
            View.ExitToMainMenu.gameObject.SetActive(true);

            EventController.I.Subscribe("NextLevelClicked", this);


            View.TopText.text = "You won!";

            GA.I.LogScreen("Win");
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
            EventController.I.Unsubscribe("NextLevelClicked", this);
        }

        public override void OnEvent(string EventName, GameObject Sender)
        {
            base.OnEvent(EventName, Sender);

            if (EventName == "NextLevelClicked")
            {
                UIRoot.I.GetView<LoadingView>().SetVisible(true);

                AppRoot.I.SetState(new PlayState(true, true));
            }
        }
    }
}
