using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPController : MonoBehaviour
{
    public GameObject cam;
    float speed = 0.1f;
    float Xsensitivity = 2;
    float Ysensitivity = 2;

    Rigidbody rb;
    CapsuleCollider capsule;

    Quaternion cameraRot;
    Quaternion characterRot;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        capsule = this.GetComponent<CapsuleCollider>();

        cameraRot = cam.transform.localRotation;
        characterRot = this.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {   

    }

    void FixedUpdate()
    {
        float yRot = Input.GetAxis("Mouse X") * Ysensitivity;
        float xRot = Input.GetAxis("Mouse Y") * Xsensitivity;

        cameraRot *= Quaternion.Euler(-xRot, 0, 0);
        characterRot *= Quaternion.Euler(0, yRot, 0);

        this.transform.localRotation = characterRot;
        cam.transform.localRotation = cameraRot;


        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            rb.AddForce(0, 300, 0);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.position += new Vector3(x * speed, 0, z * speed);
    }

    bool IsGrounded()
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, capsule.radius, Vector3.down, out hitInfo,
                (capsule.height / 2f) - capsule.radius + 0.1f))
        {
            return true;
        }
        return false;
    }
}
