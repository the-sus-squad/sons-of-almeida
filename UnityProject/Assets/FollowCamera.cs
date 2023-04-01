using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField] private GameObject camera;
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
        transform.position = Vector3.SmoothDamp(transform.position, camera.transform.position + (camera.transform.forward * 2), ref velocity, smoothTime);
    }
}
