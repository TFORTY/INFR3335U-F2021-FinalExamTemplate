using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Joystick moveStick;
    public Joystick camStick;

    Vector3 direction;

    float jHorizontal = 0f;
    float jVertical = 0f;

    float CameraAngle;
    float CameraAngleSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        CameraControls();
      
    }

    public void MovePlayer()
    {
        jHorizontal = moveStick.Horizontal * speed;
        jVertical = moveStick.Vertical * speed;
        direction = new Vector3(jHorizontal, 0f, jVertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; //+cam.eulerAngles.y
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    public void CameraControls()
    {
        CameraAngle += camStick.Horizontal * CameraAngleSpeed;

        cam.position = transform.position + Quaternion.AngleAxis(CameraAngle, Vector3.up) * new Vector3(0f, 3.54f, -9.4f);
        cam.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - cam.position, Vector3.up);
    }
}
