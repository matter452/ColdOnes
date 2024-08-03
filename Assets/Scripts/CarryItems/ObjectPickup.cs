using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    public Transform player;  // Reference to the player
    public Transform hand;    // Reference to the player's hand
    public float pickupRadius = 2.0f;  // Radius within which objects can be picked up

    private GameObject heldObject = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                // Try to pick up an object
                Collider[] colliders = Physics.OverlapSphere(player.position, pickupRadius);
                foreach (Collider collider in colliders)
                {
                    if (collider.gameObject.CompareTag("Pickup"))
                    {
                        PickupObject(collider.gameObject);
                        break;
                    }
                }
            }
            else
            {
                // Drop the held object
                DropObject();
            }
        }
    }

    void PickupObject(GameObject obj)
    {
        heldObject = obj;
        Rigidbody objRb = obj.GetComponent<Rigidbody>();
        if (objRb != null)
        {
            objRb.isKinematic = true; // Disable physics while holding
        }
        obj.transform.SetParent(hand);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
    }

    void DropObject()
    {
        if (heldObject != null)
        {
            Rigidbody objRb = heldObject.GetComponent<Rigidbody>();
            if (objRb != null)
            {
                objRb.isKinematic = false; // Re-enable physics
            }
            heldObject.transform.SetParent(null);
            heldObject = null;
        }
    }
}
