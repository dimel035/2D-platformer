using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Room Camera
    [SerializeField] private float speed;

    private float currentPosX;
    private Vector3 velocity = Vector3.zero;


    //Follow Player
    [SerializeField] private Transform player;
    [SerializeField] private float cameraSpeed;
    [SerializeField] public float yOffset = 1f;

    

    private void Update()
    {
        //Room Camera
        /*transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX,transform.position.y, transform.position.z), ref velocity,speed);*/

        //Follow Player
        Vector3 newPos = new Vector3(player.position.x, player.position.y+yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, cameraSpeed*Time.deltaTime);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX= _newRoom.position.x;
    }

}
