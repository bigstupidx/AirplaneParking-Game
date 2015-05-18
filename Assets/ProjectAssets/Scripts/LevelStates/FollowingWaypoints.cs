using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

internal class FollowingWaypoints : State
{
    private readonly List<WayPoint> _wayPoints;
    private int _current;

    public FollowingWaypoints(List<WayPoint> wayPoints)
    {
        _wayPoints = wayPoints;
        if (_wayPoints.Count == 0)
        {
            Ended = true;
            return;
        }

        foreach (var point in _wayPoints)
        {
            point.gameObject.SetActive(false);
        }
    }

    public override void Start()
    {
        base.Start();
        _wayPoints.First().gameObject.SetActive(true);
    }

    public override void Update()
    {
        float distance =
            (Player.I.transform.position - _wayPoints[_current].transform.position).magnitude;


        if (distance < _wayPoints[_current].Radius)
        {
            _wayPoints[_current].gameObject.SetActive(false);
            _current += 1;


            if (_current == _wayPoints.Count)
            {
                Ended = true;
            }
            else
            {
                _wayPoints[_current].gameObject.SetActive(true);

            }
        }
        if (_current != _wayPoints.Count)
        Player.I.Arrow.Target = _wayPoints[_current].transform;
        else
        {
            Player.I.Arrow.gameObject.SetActive(false);
        }

    }

    public Transform GetTarget()
    {
        return _wayPoints[_current].transform;
    }
}