using System.Linq;
using Glide;
using UnityEngine;
using System;
using System.Collections;

public partial class AppRoot : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////////////

    #region Variables

    private static AppRoot _instance;

    #endregion

    ///////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////

    #region Interface

    public AppRoot()
    {
        _instance = this;
    }

    public void Start()
    {
        SetState(new StateMainMenu());
        new Tweener();
        new GameSaver();
        GameSaver.I.Load();
       // GameSaver.I.FinishLevel(20);
    }

    public void Update()
    {
        UpdateStates();
        Tweener.I.Update(Time.deltaTime);
    }

    public void LateUpdate()
    {
    }

    public void OnGUI()
    {
        OnGUIStates();
    }

    /// <summary>
    /// Handler from Unity3d
    /// </summary>
    /// <param name="level"></param>
    public void OnLevelWasLoaded(int level)
    {
        OnSceneLoadedStates();
    }

    #endregion

    ///////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////

    #region Implementation

    #endregion

    ///////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////

    #region Properties

    /// <summary>
    /// Gets instance of the AppRoot object
    /// </summary>
    public static AppRoot I
    {
        get { return _instance; }
    }

    #endregion

    ///////////////////////////////////////////////////////////////////////
}
