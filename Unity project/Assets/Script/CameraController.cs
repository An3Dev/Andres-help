using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //room camera
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    //follow player
    /*[SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    private float lookAhead;*/

    private void Update()
    {
        //room camera
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX + 3.6f, 
            transform.position.y, transform.position.z), ref velocity, speed);

        //follow player 
        //transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, 
            //transform.position.z);

        //lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localPosition.x), 
            //Time.deltaTime * cameraSpeed);
    }
    //moves the camera to a new room when player collides with door
    public void MoveToNewRoom(Transform _newRoom) {
        currentPosX = _newRoom.position.x;
    }
}
