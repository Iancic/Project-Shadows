using UnityEngine;

public class SpotlightCollider : MonoBehaviour
{
    public Light spotlight;
    private Collider collider;

    void Start()
    {
        collider = GetComponent<Collider>();
        // Ensure the collider is enabled
        collider.enabled = true;
    }

    void Update()
    {
        // Update the position and size of the collider to match the spotlight
        if (spotlight != null)
        {
            collider.transform.position = spotlight.transform.position;
            // Adjust the collider's size based on the spotlight's range
            if (spotlight.type == LightType.Spot)
            {
                collider.transform.localScale = new Vector3(spotlight.range * 2f, spotlight.range * 2f, spotlight.range * 2f);
            }
            else
            {
                Debug.LogWarning("The attached light is not a spotlight.");
            }
        }
    }
}
