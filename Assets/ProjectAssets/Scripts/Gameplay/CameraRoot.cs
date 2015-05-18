using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraRoot : MonoBehaviour
{
   public Transform Target;
    public float RotSpeed = 3;
    public float MoveSpeed = 3;

    public Transform Cam;

    public static CameraRoot I { get; private set; }

    public List<CamData> Datas;
    public int _currentCamera;
	IEnumerator Start ()
	{
        I = this;

	    transform.rotation = Target.rotation;
	    transform.position = Target.position;

	    yield return null;

        transform.rotation = Target.rotation;
        transform.position = Target.position;

	    NextCamera();
	}

    public void NextCamera()
    {
        Cam.transform.localPosition = Datas[_currentCamera].Pos;
        Cam.transform.localEulerAngles = Datas[_currentCamera].Rot;

        _currentCamera ++;

        if (_currentCamera >= Datas.Count)
        {
            _currentCamera = 0;
        }

    }

    void LateUpdate ()
	{
        transform.rotation = Quaternion.Lerp(transform.rotation, Target.rotation, Time.deltaTime * RotSpeed);
        transform.position = Vector3.Lerp(transform.position, Target.position, 1);// Time.deltaTime * MoveSpeed);
	}
}

[Serializable]
public class CamData
{
    public Vector3 Pos;
    public Vector3 Rot;
}
