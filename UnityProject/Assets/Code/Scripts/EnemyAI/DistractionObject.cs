using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vector3 = UnityEngine.Vector3;

public class DistractionObject : MonoBehaviour
{

    public float soundRadius = 5f;
    public Rigidbody rigidBody;
    private float velocityTrashold = 0.1f;

    public UnityEvent<Vector3> OnSoundEmmitted;

    private Vector3 previousVelocity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = rigidBody.velocity;
        Vector3 difference = GetAbsoluteVal(velocity - previousVelocity);
        if (difference.x > velocityTrashold || difference.y > velocityTrashold || difference.z > velocityTrashold) {
            OnSoundEmmitted.Invoke(transform.position);
        }

        previousVelocity = velocity;

    }

    Vector3 GetAbsoluteVal (Vector3 vector) {
        return new Vector3(Mathf.Abs(vector.x), Mathf.Abs(vector.y), Mathf.Abs(vector.z));
    }

}
