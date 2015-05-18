using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour
{

    public static Level Instance { get; private set; }

    public float TimeToEnd = 120;

    public Transform StartPos;

    public List<WayPoint> WayPoints;

    private List<State> _states;
    private int _currentState;
    public float _elapsed;

    public void NextState()
    {
        _currentState += 1;

        if (CurrentState != null)
        {
            if (CurrentState.Ended)
            {
                NextState();
            }
            else
            {
                CurrentState.Start();
            }
        }
        else
        {
            //EventController.Instance.PostEvent("MissionFinished", gameObject);
        }

    }

    void Start()
    {
        Instance = this;
        _elapsed = 0;
        _states = new List<State>
	    {
	        new FollowingWaypoints(WayPoints),
	       // new Parking()
	    };

        CurrentState.Start();

        _Start();

       // EventController.Instance.PostEvent("MissionChangeTarget", gameObject);

    }

    private bool _failed;
    void Update()
    {

        bool death = true;

        if (!(CurrentState is FollowingWaypoints))
        {

        }

        if (_elapsed > TimeToEnd)
        {
            LevelControl.I.FailLevel(true);
            _elapsed = 0;
        }

        _elapsed += Time.deltaTime;

//        if (CurrentState is FollowingWaypoints && _currentState <= 2)
//        {
//            {
//                if (DeathOutside)
//                {
//                    foreach (var wayPoint in WayPoints)
//                    {
//                        if (
//                            Vector3.Distance(wayPoint.transform.position,
//                                AirplaneController.Instance.transform.position) <
//                            wayPoint.SafeZoneAround)
//                        {
//                            death = false;
//                            break;
//                        }
//                    }
//                }
//                else
//                {
//                    death = false;
//                }
//            }
//        }
//        else
//        {
//            death = false;
//        }


        if (death)
        {
            if (!_failed)
            {
//                EventController.Instance.PostEvent("MissionFailed", null);
                _failed = true;
            }
        }

        _Update();
    }


    protected virtual void _Start()
    {
    }

    protected virtual void _Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();

            if (CurrentState.Ended)
            {
                NextState();
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                NextState();
            }
        }
    }

    public State CurrentState
    {
        get
        {
            if (_currentState > _states.Count - 1) return null;
            return _states[_currentState];
        }
    }


}