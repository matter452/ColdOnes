using UnityEngine;

abstract class Resource : MonoBehaviour
{   
    public Resources Type { get; private set; }
    public int Quantity { get; private set; }
    public AudioClip ResourceSound { get; private set; }
    public Resource (Resources type, int quantity)
    {
        Type = type;
        Quantity = quantity;
    }

    public void PlayResourceSound()
    {
        
    }

    public void DestroyResource()
    {
        Destroy(this);
    }

    abstract public void UseResource();


}