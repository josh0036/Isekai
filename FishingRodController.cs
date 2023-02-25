using UnityEngine;

public class FishingRodController : MonoBehaviour
{
    public Transform lineStartPoint; // The point where the line starts
    public Transform lineEndPoint; // The point where the line ends
    public Transform lure; // The lure or bait
    public float castingPower = 10f; // The force applied when casting
    public float reelSpeed = 5f; // The speed at which the line is reeled in

    private bool isCasting = false; // True when the player is casting the line
    private bool isReeling = false; // True when the player is reeling in the line

    // Update is called once per frame
    void Update()
    {
        // Check for input to cast the line
        if (Input.GetKeyDown(KeyCode.Space) && !isCasting && !isReeling)
        {
            isCasting = true;
            // Apply a force to the lure to cast the line
            lure.GetComponent<Rigidbody>().AddForce(transform.forward * castingPower, ForceMode.Impulse);
        }

        // Check for input to reel in the line
        if (Input.GetKey(KeyCode.Space) && isCasting && !isReeling)
        {
            isReeling = true;
            // Move the lure towards the rod
            lure.position = Vector3.MoveTowards(lure.position, lineStartPoint.position, reelSpeed * Time.deltaTime);
        }

        // Stop reeling in the line if it reaches the rod
        if (isReeling && lure.position == lineStartPoint.position)
        {
            isCasting = false;
            isReeling = false;
        }
    }
}
