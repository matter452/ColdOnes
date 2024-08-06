using UnityEngine;

public class Ice : Resource
{   
    [SerializeField] private Ice iceBag;
    private Ice _iceBag;
    public bool isMovable = true;

    private void Start() {
        _iceBag = this;
    }

    public Ice() : base(Resources.Ice, 0, null) { }

    public override void UseResource()
    {
        Debug.Log("Using ice resource.");
    }

    public Ice GetIce()
    {
        return _iceBag;
    }


}
