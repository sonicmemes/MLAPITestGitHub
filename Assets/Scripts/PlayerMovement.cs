using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    CharacterController cc;
    public Transform cameraTransform;
    float pitch = 0f;
    public float gravity = 20.0f;
    // Start is called before the first frame update
    public bool canMove = true;
    Vector3 moveDirection = Vector3.zero;
    public float jumpSpeed = 8.0f;
    void Start()
    {
        if (!IsLocalPlayer)
        {
            cameraTransform.GetComponent<AudioListener>().enabled = false;
            cameraTransform.GetComponent<Camera>().enabled = false;

        }
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocalPlayer)
        {
            MovePlayer();
            Look();
            if (Input.GetButton("Jump") && canMove && cc.isGrounded)
            {
                moveDirection.y = jumpSpeed;
            }
        }
    }

    void MovePlayer()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = Vector3.ClampMagnitude(move, 1f);
        move = transform.TransformDirection(move);
        cc.SimpleMove(move * 5f);
    }
    void Look()
    {
        float mousex = Input.GetAxis("Mouse X") * 3f;
        transform.Rotate(0, mousex, 0);
        pitch -= Input.GetAxis("Mouse Y") * 3f;
        pitch = Mathf.Clamp(pitch, -45f, 45f);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
