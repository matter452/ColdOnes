using UnityEngine;

public class BeerStation : Resource
{
    public BeerStation() : base(Resources.Beer, 0, null) { }

    public override void UseResource()
    {
        Debug.Log("Using beer resource.");
    }
}