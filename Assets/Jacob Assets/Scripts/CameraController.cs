using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject pauseMenu;

    //Constants
    private const float Y_ANGLE_MIN = -60f;
    private const float Y_ANGLE_MAX = 60f;
    private const float MAX_ZOOM = 6.0f;
    private const float MIN_ZOOM = 2.0f;
    //Public variables

    //public Transform target;
    public float distance;
    public float sensitivityX;
    public float sensitivityY;
    //Private variables
    private Transform target;
    private float currentDistance = 0.0f;
    private float distToCol = Mathf.Infinity;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private Vector3 offset = new Vector3(0, 2f, 0);
    private bool freeMouse = false;



    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentDistance = distance;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (pauseMenu.GetComponent<PauseMenu>().isPaused == true)
        {
            return;
        }
        if (Input.GetButtonDown("Zoom"))
        {
            zoom();
        }
        if (freeMouse)
        {
            Cursor.visible = true;
            return;
        }
        else
        {
            Cursor.visible = false;
        }
        float h = Input.GetAxisRaw("Horizontal1");
        float v = Input.GetAxisRaw("Vertical1");
        currentX += h * sensitivityX;
        currentY += v * sensitivityY;
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    private void LateUpdate()
    {
        if (freeMouse)
        {
            return;
        }
        Vector3 dir = new Vector3(0, 0, -1 * currentDistance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        Vector3 targetPos = target.position + offset + rotation * dir;
        transform.position = targetPos;
        transform.LookAt(target.position + offset);
        if (toCamera())
        {
            currentDistance = distToCol - 0.2f;
        }
        currentDistance = Mathf.Lerp(currentDistance, distance, 0.1f);
    }

    private void zoom()
    {
        if (distance < MAX_ZOOM)
        {
            distance = MAX_ZOOM;
        }
        else
        {
            distance = MIN_ZOOM;
        }
    }

    private bool toCamera()
    {
        var layerMask = (1 << 9);
        layerMask |= (1 << 8);
        layerMask = ~layerMask;

        RaycastHit hit;
        bool collision = Physics.Raycast(target.position + offset, transform.position - (target.position + offset), out hit, distance, layerMask);
        if (collision)
        {
            distToCol = hit.distance;
        }
        else
        {
            distToCol = Mathf.Infinity;
        }
        return collision;
    }
}
