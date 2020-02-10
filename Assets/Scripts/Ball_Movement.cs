using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ball_Movement : MonoBehaviour
{
    public Rigidbody rigidbody;
    public Vector3 gameObjectSreenPoint;
    public Vector3 mousePreviousLocation;
    public Vector3 mouseCurLocation;
    void OnMouseDown()
    {
        //This grabs the position of the object in the world and turns it into the position on the screen
        gameObjectSreenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        //Sets the mouse pointers vector3
        mousePreviousLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z);
    }
   

    public Vector3 force;
    public Vector3 objectCurrentPosition;
    public Vector3 objectTargetPosition;
    public float topSpeed = 10;
    void OnMouseDrag()
    {
        mouseCurLocation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObjectSreenPoint.z);
        force = mouseCurLocation - mousePreviousLocation;
        mousePreviousLocation = mouseCurLocation;
        if (rigidbody.velocity.magnitude > topSpeed)
            force = rigidbody.velocity.normalized * topSpeed;
    }

    public void OnMouseUp()
    {
        if (rigidbody.velocity.magnitude > topSpeed)
            force = rigidbody.velocity.normalized * topSpeed;
    }


    public void FixedUpdate()
    {
        rigidbody.velocity = force;
    }
}
