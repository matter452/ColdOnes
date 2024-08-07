using System;
using System.Collections;
using UnityEngine;

public class IceChest : MonoBehaviour
{
    private int _totalBeers;
    [SerializeField] private float _iceEnergyReductionAmount = 0.8335f;
    private Upgrades _upgrades;
    public float baseEnergy = 11;
    public int baseCapacity = 42;
    public float baseCoolingRate = 1f / 2f; // 1 beer every 2 seconds
    private float _coolerEnergyLoad = 0f;
    public int TotalColdBeers { get; private set; }
    public int TotalWarmBeers { get; private set; }
    public int CurrentScore { get; private set; }
    public int LevelTargetScore { get; private set; }
    public event Action<int> onColdsChanged;
    public event Action<int> onWarmsChanged;
    public event Action<int> onScoreChanged;

    public float energy;
    public int capacity;
    public float coolingRate;
    private float _movementPenalty;
    private bool _iced;
    private float _iceCoolingBuffDuration = 4f;
    private float _iceBuffTimeRemaining;
    private bool _isOverheated;
    [SerializeField] private float _overHeatCooldown = 5f;
    private float _overHeatCooldownTimeRemaining;
    [SerializeField] private float isIcedCoolingBuff = 1f / 30f; // 1 beer every 30 seconds
    private Coroutine _isCooledCoroutine;
    private Transform _iceChestTransform;


    public int ScorePerColdOne = 10;
    public int ScorePerWarmOne = 5; 

    void Start()
    {
        _iceChestTransform = this.transform;
        _upgrades = new Upgrades(this);
        ResetToBaseValues();
    }

    void Update()
    {
    }

    public void AddBeers()
    {
        int amount = 6; // Add 6 beers each time
        if (_totalBeers + amount > capacity)
        {
            Debug.Log("Not enough capacity");
            return;
        }
        Debug.Log("Beeeeer Added");
        SetCoolerEnergyLoad(amount);
        Debug.Log("Cooler Energy load: "+_coolerEnergyLoad);
        _totalBeers += amount;
        TotalWarmBeers += amount;
        onWarmsChanged?.Invoke(TotalWarmBeers);
        if (_isCooledCoroutine == null)
        {
            _isCooledCoroutine = StartCoroutine(CoolBeers());
        }
    }

    public void AddIce()
    {
        SetCoolerEnergyLoad(-_iceEnergyReductionAmount);
        _iced = true;
        _iceBuffTimeRemaining = _iceCoolingBuffDuration;
        Debug.Log("Ice added");
        StartCoroutine(IceCoolingBuffDuration());
    }

    public void ResetToBaseValues()
    {
        energy = baseEnergy;
        capacity = baseCapacity;
        coolingRate = baseCoolingRate;
        TotalColdBeers = 0;
        TotalWarmBeers = 0;
        CurrentScore = 0;
        _totalBeers = 0;
        onColdsChanged?.Invoke(TotalColdBeers);
        onWarmsChanged?.Invoke(TotalWarmBeers);
        onScoreChanged?.Invoke(CurrentScore);
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
        while (TotalWarmBeers > 0)
        {
            if (_iced && !_isOverheated)
            {   Debug.Log("In coroutine: Cooling Down a beer");
                yield return new WaitForSeconds(1 / (coolingRate + isIcedCoolingBuff));
                _coolerEnergyLoad -= 1;
                Debug.Log("Cooler Energy load: "+_coolerEnergyLoad);
            }
            else if (_iced && _isOverheated)
            {
                yield return new WaitForSeconds(1 / isIcedCoolingBuff);
            }
            else if (!_iced && _isOverheated)
            {
                yield return null;
                continue;
            }
            else if (!_iced && !_isOverheated)
            {
                yield return new WaitForSeconds(1 / coolingRate);
            }

            UpdateWarmBeers(-1);
            UpdateColdBeers(1);
            UpdateScore(ScorePerColdOne);
            
            Debug.Log("Current Score: "+CurrentScore);
        }
        _isCooledCoroutine = null;
    }

    IEnumerator IceCoolingBuffDuration()
    {
        while (_iceBuffTimeRemaining > 0)
        {
            Debug.Log("We got a ice Cooling rate buff!");
            yield return new WaitForSeconds(1f);
            _iceBuffTimeRemaining -= 1f;
        }
        _iced = false;
    }

    IEnumerator OverheatCooldown()
    {
        _overHeatCooldownTimeRemaining = _overHeatCooldown;
        while (_overHeatCooldownTimeRemaining > 0)
        {
            yield return new WaitForSeconds(1f);
            _overHeatCooldownTimeRemaining -= 1f;
        }

        _isOverheated = false;
        Debug.Log("Overheat cooldown expired");
    }

    public void UpdateColdBeers(int amount)
    {
        TotalColdBeers += amount;
        onColdsChanged?.Invoke(TotalColdBeers);
    }

    public void UpdateWarmBeers(int amount)
    {
        TotalWarmBeers += amount;
        onWarmsChanged?.Invoke(TotalWarmBeers);
    }

    public void UpdateScore(int amount)
    {
        CurrentScore += amount;
        onScoreChanged?.Invoke(CurrentScore);
    }

    public bool ScoreMet()
    {
        return CurrentScore >= LevelTargetScore;
    }

    private void SetCoolerEnergyLoad(float loadAmount)
    {
        _coolerEnergyLoad += loadAmount;
        if (_coolerEnergyLoad > baseEnergy)
        {
            _isOverheated = true;
            Debug.Log("Uhh we overheated!: overheatbool = " + _isOverheated);
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

    public void SetTransform(Transform myTransform)
    {
        _iceChestTransform = myTransform;
    }

    public bool DrinkBeer()
    {
        if (TotalWarmBeers > 0)
        {
            UpdateWarmBeers(-1);
            UpdateScore(ScorePerWarmOne);
            return true;
        }
        if (TotalColdBeers > 0)
        {
            UpdateColdBeers(-1);
            UpdateScore(ScorePerColdOne);
            return true;
        }
        return false;
    }
}
