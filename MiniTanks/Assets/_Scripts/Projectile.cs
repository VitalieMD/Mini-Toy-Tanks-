using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float timeToDestroy = 10f;
    public float damage = 10;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        //Debug.Log("Collision");
    }
}
