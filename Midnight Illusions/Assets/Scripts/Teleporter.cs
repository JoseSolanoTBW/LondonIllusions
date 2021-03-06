﻿using UnityEngine;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{
    // How long the user can gaze at this before the button is clicked.
    public float timerDuration = 3f;
    public Image timerImage;
    public Transform homeDestination;

    // Count time the player has been gazing at the button.
    private float lookTimer = 0f;

    void Update()
    {
        var ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50))
        {
            if (hit.collider.tag == "Teleport")
            {
                
                if (GvrControllerInput.ClickButtonDown || Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
                    Teleport(hit);
                else
                {
                    lookTimer += Time.deltaTime;
                    timerImage.GetComponent<Image>().fillAmount = lookTimer / timerDuration;

                    if (lookTimer > timerDuration)
                        Teleport(hit);
                }
            }
            else
            {
                lookTimer = 0;
                timerImage.GetComponent<Image>().fillAmount = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    private void Teleport(RaycastHit hit)
    {
        lookTimer = 0;

        Transform destination = hit.collider.transform.Find("Destination");

        if (destination == null)
            destination = homeDestination;

        transform.parent.parent.parent.position = new Vector3(destination.position.x, transform.parent.parent.parent.position.y, destination.position.z);
        transform.parent.parent.parent.rotation = Quaternion.Euler(destination.rotation.x, destination.rotation.y, destination.rotation.z);
    }
}