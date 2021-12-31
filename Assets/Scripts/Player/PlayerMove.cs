using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Cached Variables
    [Header("Player Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Mouse Look Settings")]
    [SerializeField] private float viewSpeed = 2f;

    //Cached References
    private Rigidbody myBody;

    //Game States
    private bool isLooking;

    //Second Task Run by before the Start Function
    private void OnEnable()
    {
        myBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        LockMouse(true);
    }

    //method that gets called every frame
    private void Update()
    {
        FreeLook();
        MoveVertical();
        MoveHorizontal();
    }

    //Method that handles the camera view
    private void FreeLook()
    {
        if (isLooking)
        {
            LookAround();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            isLooking = !isLooking;
        }

    }
    //change the state of the mouse lock state for menu use
    public void LockMouse(bool changeLooking)
    {
        isLooking = changeLooking;
        Cursor.visible = !isLooking;
        if (Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //Use the axis of the mouse to rotate the view of the player
    private void LookAround()
    {
        float newRotationX = transform.localEulerAngles.y + Input.GetAxis(Tags.AXIS_MOUSE_X) * viewSpeed;
        float newRotationY = transform.localEulerAngles.x - Input.GetAxis(Tags.AXIS_MOUSE_Y) * viewSpeed;

        transform.localEulerAngles = new Vector3(newRotationY, newRotationX, Mathf.Epsilon);
    }

    //move the character forward and backward
    private void MoveVertical()
    {
        float moveForward = Input.GetAxis(Tags.AXIS_VERTI);

        Vector3 moveVerti = transform.forward * moveForward;

        transform.position = transform.position + (moveVerti * moveSpeed * Time.deltaTime);
    }

    //move the character left or right
    private void MoveHorizontal()
    {
        float moveRight = Input.GetAxis(Tags.AXIS_HORIZON);

        Vector3 moveHori = transform.right * moveRight;

        transform.position = transform.position + (moveHori * moveSpeed * Time.deltaTime);
    }
}
