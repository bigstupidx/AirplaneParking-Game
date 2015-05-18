using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;
using UnityEngine.UI;

public class Ads:MonoBehaviour, IEventSubscriber
{
    public int BlockID = -1;
    public string ImageURL = "";
    public string OpenURL = "";
    public static string JSONText = "";
    protected Texture2D AdTexture = null;
    public static readonly string IASURL = "http://ias.i6.com/ad/30.json";

    void Start()
    {
        if (JSONText == "")
        {
            JSONText = "loading...";
            StartCoroutine(LoadJSON());
        }

        if (JSONText != "loading..." && JSONText != "")
            EventController.I.PostEvent("OnLoadJson", null);


        GetComponent<Image>().color = new Color(1,1,1,0);
    }

    protected void Awake()
    {
        EventController.I.Subscribe("OnLoadJson", this);
        EventController.I.Subscribe("OnErrorJson", this);
        EventController.I.Subscribe("OnLoadTexture", this);
        EventController.I.Subscribe("OnLoadedMenuScene", this);
        EventController.I.Subscribe("OnPressObject", this);
    }

    public void OnEvent(string EventName, GameObject Sender)
    {
        if (this == null)
        {
            return;
        }

        switch (EventName)
        {
            case "OnLoadJson":
                if (gameObject.activeInHierarchy)
                {
                    StartCoroutine(LoadTexture());
                }
                break;
                
            case "OnErrorJson":
                if (JSONText == "")
                {
                    JSONText = "loading...";
                    if (gameObject.activeInHierarchy)
                    {
                        StartCoroutine(LoadJSON(10));
                    }
                }
                break;
                
            case "OnLoadTexture":
                if (Sender == gameObject && AdTexture)
                {
                    AdTexture.filterMode = FilterMode.Trilinear;


                    var image = GetComponent<Image>();

                    Sprite sprite = Sprite.Create(AdTexture, new Rect(0, 0, AdTexture.width, AdTexture.height), new Vector2(0.5f, 0.0f), 1.0f);
                    image.sprite = sprite;
                    GetComponent<Image>().color = new Color(1, 1, 1, 1);

                }
                break;

            case "OnLoadedMenuScene":
//                if (JSONText == "")
//                {
//                    JSONText = "loading...";
//                    StartCoroutine(LoadJSON());
//                }
                break;

            case "OnPressObject":
                if (Sender == gameObject && this.OpenURL != "")
                {
                    GA.I.LogEvent("IAS Click", "URL: " + OpenURL.Replace("https://play.google.com/store/apps/details?id=", ""), "IAS Click", 1);

                    Application.OpenURL(this.OpenURL);
                }
                break;

            case "OnHideGUI":
                    StartCoroutine(LoadTexture());
                break;
        }
    }

    IEnumerator LoadJSON(float WaitTime = 0) 
    {
        yield return new WaitForSeconds(WaitTime);
        WWW w = new WWW(IASURL);
        yield return w;
        if (w.error != null)
        {
            JSONText="";
            EventController.I.PostEvent("OnErrorJson", null);
        }
        else
        {
            JSONText = w.text;
            EventController.I.PostEvent("OnLoadJson", null);
        }
    }

    private int GetNext(string Name,int Max)
    {
        if (PlayerPrefs.HasKey(Name))
        {
            int num = PlayerPrefs.GetInt(Name);
            num++;
            if (num >= Max)
                num = 0;
            PlayerPrefs.SetInt(Name, num);
            return num;
        } else
        {
            PlayerPrefs.SetInt(Name,0);
            return 0;
        }
    }

    IEnumerator LoadTexture()
    {
        JSONNode j = JSON.Parse(JSONText);
        List<JSONNode> l = new List<JSONNode>();
        if (j != null)
        {
            JSONNode n = j ["slots"];
            for (int i=0; i<n.Count; i++)
                if (n [i] ["slotid"].Value.StartsWith(BlockID.ToString()))
                    l.Add(n [i]);
            n = l [GetNext(BlockID.ToString(), l.Count)];
            OpenURL = n ["adurl"];
            WWW www = new WWW(ImageURL = n ["imgurl"]);
            yield return www;
            if (www.error == null)
            {
                AdTexture = www.texture;
                EventController.I.PostEvent("OnLoadTexture", gameObject);
            } else
            {
                EventController.I.PostEvent("OnLoadTexture", gameObject);
            }
        }
    }
}
