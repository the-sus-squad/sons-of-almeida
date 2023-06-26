using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField] private new GameObject camera;
    [SerializeField] private float distanceDown;
    [SerializeField] private float distanceForward;
    private float smoothTime = 0.7F;
    private Vector3 velocity = Vector3.zero;

    // Periodic movement every x seconds towards camera 

    // Start is called before the first frame update
    //void Start()
    //{
    //    InvokeRepeating("followCamera", 3f, 3f);
    //}

    //void followCamera()
    //{
    //    transform.rotation = camera.transform.rotation;
    //    transform.position = camera.transform.position;
    //    transform.position += transform.forward * 2;
    //}


    // Continuous movement towards camera

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.transform.rotation;
        transform.position = Vector3.SmoothDamp(transform.position, camera.transform.position - transform.up*distanceDown + (transform.forward*distanceForward), ref velocity, smoothTime);
    }
}
