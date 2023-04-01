using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField] private new GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("followCamera", 2f, 5f);
    }

    void followCamera()
    {
        transform.rotation = camera.transform.rotation;
        transform.position = camera.transform.position;
        //Debug.Log(transform.forward);
        transform.position += transform.forward * 2;
    }
}
