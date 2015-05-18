using UnityEngine;
using System.Collections;

public class ParkingCollider : MonoBehaviour
{
    public bool FirstCollider;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 1);
    }
}
