using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeedX;
    public float rotationSpeedY;
    public float rotationSpeedZ;
    //public TankPlayer tankPlayerScript;
    void Update()
    {
        this.transform.Rotate(0, rotationSpeedY, 0 * Time.deltaTime);

    }
}
