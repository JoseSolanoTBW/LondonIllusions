using UnityEngine;

public class Teleporter : MonoBehaviour
{
    // How long the user can gaze at this before the button is clicked.
    public float timerDuration = 3f;

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
                if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
                    Teleport(hit);
                else
                {
                    lookTimer += Time.deltaTime;

                    if (lookTimer > timerDuration)
                        Teleport(hit);
                }
            }
            else
                lookTimer = 0;
        }
    }
    private void Teleport(RaycastHit hit)
    {
        lookTimer = 0;
        Transform destination = hit.collider.transform.Find("Destination");
        transform.parent.parent.position = new Vector3(destination.position.x, transform.parent.parent.position.y, destination.position.z);
    }
}