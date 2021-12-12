using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
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
    Vector3 moveDir;

    float jHorizontal = 0f;
    float jVertical = 0f;

    float CameraAngleSpeed = 2f;

    CinemachineFreeLook cinCam;

    public PhotonView view;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            MovePlayer();
            CameraControls();
        }
    }

    public void MovePlayer()
    {
        jHorizontal = moveStick.Horizontal * speed;
        jVertical = moveStick.Vertical * speed;
        direction = new Vector3(jHorizontal, 0f, jVertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    public void CameraControls()
    {
        // Gets cinemachine's x-axis value and moves based on the camera joystick's horizontal input
        cinCam.m_XAxis.Value += camStick.Horizontal * CameraAngleSpeed;
    }

    public void SetJoysticks(GameObject camera)
    {
        Joystick[] tempJoystickList = camera.GetComponentsInChildren<Joystick>();

        foreach(Joystick temp in tempJoystickList)
        {
            if (temp.tag == "Joystick Movement")
            {
                moveStick = temp;
            }
            else if (temp.tag == "Joystick Camera")
            {
                camStick = temp;
            }
        }

        cam = camera.transform;

        // Tells the cinemachine camera what the follow and look at targets are when instantiated
        cinCam = cam.GetComponentInChildren<CinemachineFreeLook>();
        cinCam.Follow = GameObject.FindGameObjectWithTag("Player").transform;
        cinCam.LookAt = GameObject.FindGameObjectWithTag("LookAtTarget").transform;
    }
}