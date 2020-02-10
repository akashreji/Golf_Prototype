using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball_Force : MonoBehaviour
{
    public int sliderCount;
    public Slider powerBarSlider;
    //public Camera mainCamera;
    public float speed;
    public float force;
    public float speedNew;
    Vector3 pointToLook;
    Vector3 direction;
    Vector3 point;
    Quaternion targetRotation;
    bool ballSelected;
    void Start()
    {
        speedNew = 2f;
    }
   
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(force<500)
            force += 10;
            powerBarSlider.value = force;
            print(force);
        }
        if (Input.GetMouseButtonUp(0))
        {
            gameObject.GetComponent<Rigidbody>().AddForce(-direction * force);
            force = 0;
            powerBarSlider.value = force;
        }
    }

    void FixedUpdate()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0.0f;
        if (playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            var heading = transform.position - targetPoint;
            var distance = heading.magnitude;
            direction = heading / distance;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedNew * Time.deltaTime);
        }
    }
}
