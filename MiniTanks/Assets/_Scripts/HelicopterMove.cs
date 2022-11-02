using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterMove : MonoBehaviour
{
    public float speedFly;
    public float rotation;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += transform.forward * speedFly * Time.deltaTime;
        transform.Rotate(0, rotation, 0 * Time.deltaTime);
    }
}
