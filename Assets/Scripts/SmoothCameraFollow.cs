using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    
    public Transform lookAt; //élément à suivre (player ou FollowTarget du player)
    public Transform camTransform;

    private void Start()
    {
        camTransform = transform;
        
    }

    private void LateUpdate()
    {
        Vector3 position = camTransform.position; //position de la camera
        Vector3 wantedPosition = lookAt.position; //position que l'on cherche à avoir (celle de l'élément à suivre)

        Quaternion rotation = camTransform.rotation; //rotation de la camera
        Quaternion wantedRotation = lookAt.rotation; //rotation que l'on cherche à avoir (celle de l'élément à suivre)
        

        position = Vector3.Lerp(position, wantedPosition, 0.03f);
        rotation = Quaternion.Lerp(rotation, wantedRotation, 0.02f);

        camTransform.position = position;
        camTransform.rotation = rotation;
    }
}
