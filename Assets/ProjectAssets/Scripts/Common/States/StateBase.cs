using UnityEngine;

public abstract class StateBase: IEventSubscriber
{
    ///////////////////////////////////////////////////////////////////////

    #region Variables

    private readonly string _stateSceneName;

    #endregion

    ///////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////

    #region Interface

    /// <summary>
    /// Creates new instance of the StateBase object
    /// </summary>
    public StateBase(string stateSceneName, bool forceReload = false)
    {
        _stateSceneName = stateSceneName;
        ForceReload = forceReload;
    }

    public void Activate()
    {
        OnActivate();
    }

    public void Deactivate()
    {
        OnDeactivate();
    }

    /// <summary>
    /// called when state gets activated
    /// </summary>
    protected virtual void OnActivate()
    {
    }

    /// <summary>
    /// called when state get deactivated
    /// </summary>
    protected virtual void OnDeactivate()
    {
    }

    /// <summary>
    /// called when app recived unity ongui event
    /// </summary>
    public virtual void OnGUI()
    {

    }

    /// <summary>
    /// called on unity update
    /// </summary>
    public virtual void Update()
    {
    }


    /// <summary>
    /// Called from EventController
    /// </summary>
    public virtual void OnEvent(string EventName, GameObject Sender)
    {
    }

    /// <summary>
    /// called when app finished loading scene
    /// </summary>
    public virtual void OnSceneLoaded()
    {

    }


    public virtual void OnApplicationPause(bool pauseStatus)
    {
        Debug.Log("Pause status: " + pauseStatus);
    }

    public virtual void OnApplicationQuit()
    {
    }

    #endregion

    ///////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////

    #region Properties

    public virtual string StateSceneName
    {
        get { return _stateSceneName; }
    }
    public bool ForceReload { get; private set; }

    #endregion

    ///////////////////////////////////////////////////////////////////////

}