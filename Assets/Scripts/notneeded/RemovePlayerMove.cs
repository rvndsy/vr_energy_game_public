using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float lookSpeed = 1f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        transform.Translate(UnityEngine.Vector3.right * Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
        transform.Translate(UnityEngine.Vector3.forward * -Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed);
        transform.Rotate(transform.up, Input.GetAxis("Mouse X") * Mathf.Rad2Deg * Time.deltaTime * lookSpeed);
        Camera.main.transform.Rotate(UnityEngine.Vector3.right, -Input.GetAxis("Mouse Y") * Mathf.Rad2Deg * Time.deltaTime * lookSpeed);
    }
}
