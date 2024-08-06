using System.Buffers.Text;
using UnityEngine;

public class Beer : Resource
{
    [SerializeField] private Beer beerPrefab;
    private Beer _beer;
    private AudioSource _audioSource;
    public bool isMovable = true;

    private void Start() {
        _audioSource = this.GetComponent<AudioSource>();
    }
    public Beer() : base(Resources.Beer, 0, null) { }

    public override void UseResource()
    {
        Debug.Log("Using beer resource.");
    }

    public override void PlayResourceSound()
    {
        if (_audioSource != null && _audioSource.clip != null)
        {
            _audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing on the Beer prefab.");
        }
    }

    public Beer GetBeer()
    {
        return _beer;
    }

    public void GrabBeer()
    {
        this.PlayResourceSound();
    }


}