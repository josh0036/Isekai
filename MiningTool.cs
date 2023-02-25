using UnityEngine;

public class MiningTool : MonoBehaviour
{
    public float miningRange = 2f;
    public float miningPower = 1f;

    void Update()
    {
        // Check if the player is pressing the mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast to find any resources within the mining range
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, miningRange))
            {
                // Check if the object is a resource object
                ResourceObject resourceObject = hit.collider.GetComponent<ResourceObject>();
                if (resourceObject != null)
                {
                    // Mine the resource
                    resourceObject.Mine(miningPower);
                }
            }
        }
    }
}
