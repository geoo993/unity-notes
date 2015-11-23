﻿using UnityEngine;
using System.Collections;

public class FirstPersonCamera : MonoBehaviour
{
    public float speed = 15;

    public float xSensitivity = 2f;
    public float ySensitivity = 2f;

    public bool clampVerticalRotation = true;

    private CursorLockMode wantedMode;

    // Use this for initialization
    void Start()
    {
        wantedMode = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        updateCursor();

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            rotateCamera();

            moveCamera();
        }
    }

    private void updateCursor()
    {
        // Release cursor on escape keypress
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            wantedMode = CursorLockMode.None;
        }

        // Lock cursor on click
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            wantedMode = CursorLockMode.Locked;
        }

        // Apply requested cursor state
        Cursor.lockState = wantedMode;
        // Hide cursor when locking
        Cursor.visible = (CursorLockMode.Locked != wantedMode);
    }

    private void rotateCamera()
    {
        float yRot = Input.GetAxis("Mouse X") * xSensitivity;
        float xRot = -1 * Input.GetAxis("Mouse Y") * ySensitivity;

        if(clampVerticalRotation)
        {
            xRot = clampXAxisRotation(xRot);
        }

        transform.Rotate(new Vector3(xRot, 0f, 0f), Space.Self);
        transform.Rotate(new Vector3(0f, yRot, 0f), Space.World);
    }

    private float clampXAxisRotation(float xRot)
    {
        float curXRot = transform.localEulerAngles.x;
        float newXRot = curXRot + xRot;

        if (newXRot > 90 && newXRot < 270)
        {
            if (xRot > 0)
            {
                xRot = 90 - curXRot;
            }
            else
            {
                xRot = 270 - curXRot;
            }
        }

        return xRot;
    }

    private void moveCamera()
    {
        float vertKey = Input.GetAxisRaw("Vertical");
        vertKey = (vertKey == 0) ? 0 : vertKey;

        float horKey = Input.GetAxisRaw("Horizontal");
        horKey = (horKey == 0) ? 0 : horKey;

        Vector3 moveDir = transform.right * horKey + transform.forward * vertKey;

        if(Input.GetButton("Jump"))
        {
            moveDir += transform.up;
        }

        transform.position += moveDir.normalized * speed * Time.deltaTime;
    }
}