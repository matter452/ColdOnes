using UnityEngine;

public abstract class Resource : MonoBehaviour
{
    public Resources Type { get; private set; }
    public int Quantity { get; private set; }
    public AudioClip ResourceSound { get; private set; }

    public Resource(Resources type, int quantity, AudioClip resourceSound)
    {
        Type = type;
        Quantity = quantity;
        ResourceSound = resourceSound;
    }

    public virtual void PlayResourceSound()
    {
        if (ResourceSound != null)
        {
            AudioSource.PlayClipAtPoint(ResourceSound, transform.position);
        }
    }

    public void DestroyResource()
    {
        Destroy(gameObject);
    }

    public abstract void UseResource();

    public Transform GetResourceTransform()
    {
        return transform;
    }
}
