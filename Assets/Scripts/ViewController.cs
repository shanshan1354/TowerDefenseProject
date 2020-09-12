using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    public float scrollwheelSpeed = 100;
    public float speed = 10;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(h,0,v) * Time.deltaTime*speed, Space.World);
        transform.Translate(new Vector3(0,0, mouse) * Time.deltaTime* scrollwheelSpeed);
    }
}
