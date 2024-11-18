using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform playerView; //This is to attach the player's empty gameobject so that we can view the camera from its point.


    private void LateUpdate() //apparently LateUpdate is used best for camera related movement because it ensures the camera moves correctly along with your movement
    {
        if (playerView != null)
        {
            transform.position = playerView.position; //assigns the position to the position of the playerView empty gameobject
            transform.rotation = playerView.rotation; //assigns the rotation to the roation of the playerView rotation


        }
    }
}
