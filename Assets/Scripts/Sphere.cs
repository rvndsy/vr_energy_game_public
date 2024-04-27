using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    int time;
    Vector3 speed = new Vector3 (0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed += new Vector3(0, 0.0001f, 0);
        transform.position += speed;
    }
}
