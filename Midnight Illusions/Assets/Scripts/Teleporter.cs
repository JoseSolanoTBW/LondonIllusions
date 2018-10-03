using System;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform rayTransform;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
        {
            var ray = new Ray(rayTransform.position, rayTransform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 50))
            {
                if (hit.collider.tag == "Teleport")
                {
                    Transform destination = hit.collider.transform.Find("Destination");
                    rayTransform.parent.parent.position = new Vector3(destination.position.x, rayTransform.parent.parent.position.y, destination.position.z);
                }
            }
        }
    }
}