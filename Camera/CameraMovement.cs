using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class CameraMovement : MonoBehaviour
{

    public GameObject camera;

    private void Update()
    {

        Vector3 inputDir = new Vector3(0, 0, 0);

        int edgeScrollSize = 10;
        float moveSpeed = 8f;

        if (camera.transform.position.x < 315)
        {
            if (Input.mousePosition.x > Screen.width - edgeScrollSize) inputDir.x = +1f;

        }

        if (camera.transform.position.x > 300)
        {
            if (Input.mousePosition.x < edgeScrollSize) inputDir.x = -1f;

        }

        if (camera.transform.position.y > 10)
        {
            if (Input.mousePosition.y > Screen.height - edgeScrollSize) inputDir.z = +1f;

        }

        if (camera.transform.position.y < 15)
        {
            if (Input.mousePosition.y < edgeScrollSize) inputDir.z = -1f;

        }

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

    }
}
