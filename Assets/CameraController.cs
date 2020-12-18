using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    private Camera playerCamera;

    private Vector3 offset;

    private bool dpadInput = false;

    private float targetZoom;
    private readonly float zoomFactor = 3f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCamera = gameObject.GetComponent<Camera>();
        offset = transform.position - player.transform.position;
        targetZoom = playerCamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        CameraZoom();
        transform.position = player.transform.position + offset;
    }

    private void CameraZoom()
    {
        if (Input.GetAxis("DPAD Y") == 0.0)
        {
            dpadInput = true;
        }

        float dpadX = Input.GetAxisRaw("DPAD Y");

        if (dpadX == -1f && dpadInput)
        {
            StartCoroutine(DpadControl(false));
        }
        else if (dpadX == 1f && dpadInput)
        {
            StartCoroutine(DpadControl(true));
        }
    }

    IEnumerator DpadControl(bool input)
    {
        dpadInput = false;

        yield return new WaitForSeconds(0.5f);
        if (input == false)
        {
            targetZoom += zoomFactor;
            Debug.Log(targetZoom);
            var zoomChange = Mathf.Lerp(playerCamera.orthographicSize, targetZoom, Time.deltaTime);
            if (zoomChange < 7)
            {
                playerCamera.orthographicSize = Mathf.Lerp(playerCamera.orthographicSize, targetZoom, Time.deltaTime);
            }
            else
            {
                targetZoom = 0;
            }
        }
        if (input == true) 
        {
            targetZoom -= zoomFactor;
            Debug.Log(targetZoom);
            var zoomChange = Mathf.Lerp(playerCamera.orthographicSize, targetZoom, Time.deltaTime);
            if (zoomChange > 5)
            {
                playerCamera.orthographicSize = Mathf.Lerp(playerCamera.orthographicSize, targetZoom, Time.deltaTime);
            }
            else
            {
                targetZoom = 0;
            }
        }

        StopCoroutine(nameof(DpadControl));
    }
}
