using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public float wheelRotationSpeedX;
    public float wheelRotationSpeedY;
    public float wheelRotationSpeedZ;
    public TankPlayer tankPlayerScript;
    void Update()
    {
        if (tankPlayerScript.IsMoving == true)
        {
            transform.Rotate(wheelRotationSpeedX, 0, 0 * Time.deltaTime);
        }

    }
}
