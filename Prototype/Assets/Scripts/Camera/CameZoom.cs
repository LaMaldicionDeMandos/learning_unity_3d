using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameZoom : MonoBehaviour
{
    private const float DEFAULT_DISTANCE = 20f;
    private const float MIN_DISTANCE = 10f;
    private const float MAX_DISTANCE = 40f;
    private const float ANGLE = 0.4475f;

    private const float WHEEL_SENSITIBITY = 10f;
    private Cinemachine3rdPersonFollow follower;

    void Start() {
        CinemachineVirtualCamera camera = GetComponent<CinemachineVirtualCamera>();
        var stage = camera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        if (stage is Cinemachine3rdPersonFollow) {
           follower = (Cinemachine3rdPersonFollow)stage;
           follower.CameraDistance = DEFAULT_DISTANCE;
           follower.VerticalArmLength = DEFAULT_DISTANCE*ANGLE;
        }
    }


    // Update is called once per frame
    void Update()
    {
        var fov = Input.GetAxis("Mouse ScrollWheel") * WHEEL_SENSITIBITY;
        if (fov != 0 ) {
            ChangeZoom(fov);
        }
    }

    private void ChangeZoom(float delta) {
        follower.CameraDistance = Mathf.Clamp(follower.CameraDistance - delta, MIN_DISTANCE, MAX_DISTANCE);
        follower.VerticalArmLength = follower.CameraDistance * ANGLE;
    }  
}
