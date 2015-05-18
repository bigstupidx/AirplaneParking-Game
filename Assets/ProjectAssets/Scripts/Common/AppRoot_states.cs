using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class AppRoot
{
    ///////////////////////////////////////////////////////////////////////

    #region Variables

    private readonly Stack<StateBase> _previousStates = new Stack<StateBase>();
    private StateBase _currentState;

    /// <summary>
    /// To display progress of load scene data
    /// </summary>
    private AsyncOperation _aoLoadScene;

    #endregion

    ///////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////

    #region Interface

    public void SetState(StateBase state, bool canGoBack = true)
    {
        if (canGoBack)
        {
            if (_currentState != null)
            {
                _previousStates.Push(_currentState);
            }
        }

        Debug.Log("Try to set state: " + state.GetType().Name);

        // 
        StartCoroutine(SetStateCoroutine(state));
    }

    /// <summary>
    /// returns to previously set state
    /// </summary>
    public void GoToPreviousState()
    {
        if (_previousStates.Any())
        {
            SetState(_previousStates.Pop(), false);
        }
    }

    /// <summary>
    /// adds previous state without setting current
    /// </summary>
    public void InjectPreviousState(StateBase type)
    {
        _previousStates.Push(type);
    }


    /// <summary>
    /// clears all previous states
    /// </summary>
    public void ResetPreviousStates()
    {
        _previousStates.Clear();
    }


    public void OnApplicationPause(bool pauseStatus)
    {
        if (_currentState != null)
        {
            _currentState.OnApplicationPause(pauseStatus);
        }
    }

    public void OnApplicationQuit()
    {
        _currentState.OnApplicationQuit();
    }

    #endregion

    ///////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////

    #region Implementation


    /// <summary>
    /// Sets current state and loads new scene if needed
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private IEnumerator SetStateCoroutine(StateBase state)
    {
        //
        if (_currentState != null)
        {
            _currentState.Deactivate();
            _currentState = null;
        }

        _currentState = state;

        if (!string.IsNullOrEmpty(_currentState.StateSceneName))
        {
            if (Application.loadedLevelName != _currentState.StateSceneName || _currentState.ForceReload)
            {
                string curState = _currentState.StateSceneName;
                yield return null;
                _aoLoadScene = Application.LoadLevelAsync(curState);
                Debug.Log("Start load: " + _currentState.StateSceneName);
                yield return _aoLoadScene;
                Debug.Log("End load: " + _currentState.StateSceneName);
                _aoLoadScene = null;
            }
        }

        _currentState.Activate();
    }

    private void UpdateStates()
    {
        if (_currentState != null)
        {
            _currentState.Update();
        }
    }


    private void OnGUIStates()
    {
        if (_currentState != null)
        {
            _currentState.OnGUI();
        }
    }

    private void OnSceneLoadedStates()
    {
        if (_currentState != null)
        {
            _currentState.OnSceneLoaded();
        }
    }

    #endregion

    ///////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////

    #region Properties

    /// <summary>
    /// Gets current state
    /// </summary>
    public StateBase CurrentState
    {
        get { return _currentState; }
    }

    #endregion

    ///////////////////////////////////////////////////////////////////////
}
