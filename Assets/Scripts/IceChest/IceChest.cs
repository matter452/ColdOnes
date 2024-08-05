using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class IceChest : MonoBehaviour
{
    private float _totalBeers;
    [SerializeField]private float _iceEnergyReductionAmount = 0.8335f;
    private Upgrades _upgrades;
    public float baseEnergy = 1.8335f;
    public float baseCapacity = 8f;
    public float baseCoolingRate = .1667f;
    private float _coolerEnergyLoad = 0f;
    private float _warmBeers;
    private float _cooledBeers;

    public float energy;
    public float capacity;
    public float coolingRate;
    private float _movementPenalty;
    private bool _iced;
    private float _iceCoolingBuffDuration = 4f;
    private float _iceBuffTimeRemaining;
    private bool _isOverheated;
    [SerializeField] float _overHeatCooldown = 5f;
    private float _overHeatCooldownTimeRemaining;
    [SerializeField] private float isIcedCoolingBuff = 0.03334f;
    private Coroutine _isCooledCoroutine;


    void Start()
    {
        _upgrades = new Upgrades(this);
        ResetToBaseValues();
    }

    void Update()
    {

    }
//todo: add logic to display message of too many beers.
    public void AddBeers()
    {   float amount = 1f;
        if(_totalBeers + amount > capacity)
        {
            Debug.Log("not enough capacity");
            return;
        }
        SetCoolerEnergyLoad(amount);
        _totalBeers+= amount;
        _warmBeers+= amount;
        if(_isCooledCoroutine == null)
        {
           _isCooledCoroutine = StartCoroutine(CoolBeers());
        }
    }

    public void AddIce()
    {   
        SetCoolerEnergyLoad(-_iceEnergyReductionAmount);
        _iced = true;
        _iceBuffTimeRemaining = _iceCoolingBuffDuration;
        StartCoroutine(IceCoolingBuffDuration());
    }

    public void ResetToBaseValues()
    {
        energy = baseEnergy;
        capacity = baseCapacity;
        coolingRate = baseCoolingRate;
        if (_isCooledCoroutine != null)
        {
            StopCoroutine(_isCooledCoroutine);
            _isCooledCoroutine = null;

        }
    }

    public void Upgrade(UpgradeType type)
    {
        _upgrades.Upgrade(type);
    }

    IEnumerator CoolBeers()
    {   
        float beersCooled = 0;
        while(_warmBeers > 0)
        {   
            if(_iced && !_isOverheated){
                beersCooled = (coolingRate+isIcedCoolingBuff) * Time.deltaTime;
            }
            if(_iced && _isOverheated)
            {
                beersCooled = isIcedCoolingBuff * Time.deltaTime;
            }
            if(!_iced && _isOverheated)
            {
                beersCooled = 0;
            }
            if(!_iced && !_isOverheated)
            {
                beersCooled = coolingRate * Time.deltaTime;
            }
            _warmBeers = Mathf.Max(0, _warmBeers-beersCooled);
            _cooledBeers += beersCooled;
            yield return null;
        }
        _isCooledCoroutine = null;
    }

    IEnumerator IceCoolingBuffDuration()
    {
        while(_iceBuffTimeRemaining > 0)
        {
            yield return new WaitForSeconds(1f);
            _iceBuffTimeRemaining -= 1f;
        }
        _iced = false;
    }

    IEnumerator OverheatCooldown()
    {   _overHeatCooldownTimeRemaining = _overHeatCooldown;
        while(_overHeatCooldownTimeRemaining > 0)
        {
            yield return new WaitForSeconds(1f);
            _overHeatCooldownTimeRemaining -= 1f;
        }
        _isOverheated = false;
    }

    public float GetTotalBeers()
    {
        return _totalBeers;
    }

    private void SetCoolerEnergyLoad(float loadAmount)
    {
        _coolerEnergyLoad += loadAmount;
        if(_coolerEnergyLoad > baseEnergy)
        {
            _isOverheated = true;
            StartCoroutine(OverheatCooldown());
        }
    }

    public void SetMovementPenalty(float penalty)
    {
        _movementPenalty = penalty;
    }

    public float GetMovementPenalty()
    {
        return _movementPenalty;
    }

    public int GetWarmOnes()
    {
        return Mathf.RoundToInt(_warmBeers);
    }

    public int GetColdOnes()
    {
        return Mathf.RoundToInt(_cooledBeers);
    }

    /* void Update()
    {
        // Example input handling for upgrades (for demonstration purposes)
        if (Input.GetKeyDown(KeyCode.U)) Upgrade(UpgradeType.Energy);
        if (Input.GetKeyDown(KeyCode.I)) Upgrade(UpgradeType.Capacity);
        if (Input.GetKeyDown(KeyCode.O)) Upgrade(UpgradeType.CoolingRate);
       
        Debug.Log($"Energy: {energy}, Capacity: {capacity}, CoolingRate: {coolingRate}");
    } */
}
