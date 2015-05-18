using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PreCloseScreen : MonoBehaviour, IEventSubscriber
{
    public GameObject Items;

	public List<PreCloseScreenImageElement> Images;
    [NonSerialized]
    public PreCloseScreen Instance;

    public bool IsMainMenu = false;

    private bool CanOpen = true;
	
	private bool _placedImages;
    private float _timeScaleBefore;
	void Start()
	{
	    Instance = this;
	    _placedImages = false;
		if(PrecloseScreenIAS.Instance.preReady)
		{
			PlaceImages();
		}

		EventController.I.Subscribe("LoadedIAS", this);
        EventController.I.Subscribe("OnPrecloseAdClick", this);
        EventController.I.Subscribe("OnClosePreCloseScreen", this);
        EventController.I.Subscribe("RateGame", this);
        EventController.I.Subscribe("OnQuitGame", this);
        EventController.I.Subscribe("OnShowMainMenu", this);
        EventController.I.Subscribe("OnShowAirplaneSelecting", this);
        EventController.I.Subscribe("LevelSelected", this);
	}

	void Update()
	{
	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
	        if (Items.activeSelf)
	        {
	            Close();
	        }
	        else
	        {
	            Show();
	        }
	    }

	    if(!_placedImages)
		{
            if (PrecloseScreenIAS.Instance != null)
			if(PrecloseScreenIAS.Instance.preReady)
			{
				PlaceImages();
			}
		}
	}

	public void OnEvent (string EventName, GameObject Sender)
	{
        if (EventName == "LevelSelected")
        {
            EventController.I.Unsubscribe("LoadedIAS", this);
            EventController.I.Unsubscribe("OnPrecloseAdClick", this);
            EventController.I.Unsubscribe("OnClosePreCloseScreen", this);
            EventController.I.Unsubscribe("RateGame", this);
            EventController.I.Unsubscribe("OnQuitGame", this);
            EventController.I.Unsubscribe("OnShowMainMenu", this);
            EventController.I.Unsubscribe("OnShowAirplaneSelecting", this);
            EventController.I.Unsubscribe("LevelSelected", this);
        }

	    if (this == null)
	    {
            return;
	    }

	    if(EventName == "OnQuitGame")
		{
			Debug.Log("Quit game");
			Application.Quit();
		}
		else if(EventName == "RateGame")
		{
            Application.OpenURL("market://details?id=com.i6.AirplaneTruck");
		}
		else if(EventName == "OnPrecloseAdClick")
		{
            ShowAdd(Sender);
		}
        else if (EventName == "OnClosePreCloseScreen")
		{
			Close ();
		}
        else if (EventName == "OnShowMainMenu")
        {
            CanOpen = true;
        }
        else if (EventName == "OnShowAirplaneSelecting")
        {
            CanOpen = false;
        }
	}


    void ShowAdd(GameObject sender)
    {
        PreCloseScreenImageElement element = Images.Find(p => p.GuiObject.gameObject == sender);

        GA.I.LogEvent("IAS Click", "URL: " + element.Url.Replace("https://play.google.com/store/apps/details?id=", ""), "IAS Click", 1);

        Application.OpenURL(element.Url);


    }

    private void PlaceImages()
    {
        _placedImages = true;
        for (int i = 0; i < PrecloseScreenIAS.Instance.preBannerTextures.Count; i++)
        {
            var texture = PrecloseScreenIAS.Instance.preBannerTextures[i];
            var url = PrecloseScreenIAS.Instance.preBannerURL[i];
            var imageElement = Images.Find(p => !p.Placed);
            if (imageElement == null)
            {
                break;
            }
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.0f), 1.0f);
            imageElement.GuiObject.sprite = sprite;
            imageElement.Url = url;
            imageElement.Placed = true;
        }
    }

    private void Show()
    {
        if (!CanOpen)
        {
            return;
        }

        _timeScaleBefore = Time.timeScale;
        Time.timeScale = 0;
        Items.SetActive(true);

        if (!IsMainMenu)
        {
            AudioListener.volume = 0;
        }

        EventController.I.PostEvent("OnShowPrecloseScreen", null);
    }

    void Close()
    {
        Time.timeScale = _timeScaleBefore;
        Items.SetActive(false);
        AudioListener.volume = 1;
    }
}
