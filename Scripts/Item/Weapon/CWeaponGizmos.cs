using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeaponGizmos : MonoBehaviour
{
    private Color _color = Color.yellow;
    private float _radius = 0.1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawSphere(transform.position, _radius);
    }

}
