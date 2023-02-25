using UnityEngine;

public class FishController : MonoBehaviour
{
    public float swimSpeed = 2f; // The speed at which the fish swims
    public float rotationSpeed = 5f; // The speed at which the fish rotates
    public float biteDistance = 0.5f; // The distance at which the fish will bite the lure
    public float tensionForce = 5f; // The force applied to the line when the fish is caught
    public GameObject caughtEffect; // The particle effect played when the fish is caught

    private Transform target; // The target for the fish to swim towards
    private bool isBiting = false; // True when the fish is biting the lure
    private bool isCaught = false; // True when the fish is caught by the player

    // Start is called before the first frame update
    void Start()
    {
        // Find the player object and set it as the target for the fish
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the fish is within biting distance of the lure
        if (Vector3.Distance(transform.position, target.position) < biteDistance && !isBiting && !isCaught)
        {
            isBiting = true;
            // Rotate the fish to face the lure
            transform.LookAt(target);
        }

        // Swim towards the target if the fish is biting or caught
        if (isBiting || isCaught)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, swimSpeed * Time.deltaTime);
        }

        // Apply tension to the line if the fish is caught
        if (isCaught)
        {
            target.GetComponent<Rigidbody>().AddForce((target.position - transform.position).normalized * tensionForce);
        }
    }

    // Called when the fish collides with another object
    void OnTriggerEnter(Collider other)
    {
        // Check if the fish collides with the lure and is biting
        if (other.CompareTag("Lure") && isBiting && !isCaught)
        {
            isCaught = true;
            isBiting = false;
            // Spawn the caught effect at the fish's position
            Instantiate(caughtEffect, transform.position, Quaternion.identity);
        }
    }
}
