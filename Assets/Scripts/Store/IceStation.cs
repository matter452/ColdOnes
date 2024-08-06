using UnityEngine;

public class IceStation : Resource
{
    public IceStation() : base(Resources.Ice, 0, null) { }

    public override void UseResource()
    {
        Debug.Log("Using ice resource.");
    }
}