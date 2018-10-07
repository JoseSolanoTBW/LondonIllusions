using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform rayTransform;

    // Whether the Google Cardboard user is gazing at this button.
    private bool isLookedAt = false;

    // How long the user can gaze at this before the button is clicked.
    public float timerDuration = 3f;

    // Count time the player has been gazing at the button.
    private float lookTimer = 0f;
    
    void Update()
    {
        Debug.Log(isLookedAt.ToString());
        if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Teletransported by click!");
            LoadNextScene();
        }

        if (isLookedAt)
        {
            lookTimer += Time.deltaTime;

            if (lookTimer > timerDuration)
            {
                lookTimer = 0f;

                Debug.Log("Teletransported by seeing it!");

                LoadNextScene();
            }
        }
    }

    //Load Destination
    private void LoadNextScene()
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

    // True if the user is seeing the element
    public void SetGazedAt(bool gazedAt)
    {
        isLookedAt = gazedAt;
    }
}