using UnityEngine;
using System.Collections.Generic;

public class Upgrades
{
    private Dictionary<UpgradeType, int> _upgradeLevels;
    private IceChest _iceChest;
    private float _playerSpeedDebuff;

    public Upgrades(IceChest iceChest)
    {
        this._iceChest = iceChest;
        _upgradeLevels = new Dictionary<UpgradeType, int>
        {
            { UpgradeType.Energy, 0 },
            { UpgradeType.Capacity, 0 },
            { UpgradeType.CoolingRate, 0 }
        };
    }

    public int GetUpgradeLevel(UpgradeType type)
    {
        return _upgradeLevels[type];
    }

    public void Upgrade(UpgradeType type)
    {
        _upgradeLevels[type]++;
        ApplyUpgradeEffects();
    }

    private void ApplyUpgradeEffects()
    {
        // Reset to base values before applying upgrades
        _iceChest.ResetToBaseValues();

        foreach (var upgrade in _upgradeLevels)
        {
            switch (upgrade.Key)
            {
                case UpgradeType.Energy:
                    _iceChest.energy += _iceChest.baseEnergy *(0.1f*upgrade.Value); // Example calculation
                    break;
                case UpgradeType.Capacity:
                    _iceChest.capacity += upgrade.Value * 5; // Example calculation
                    break;
                case UpgradeType.CoolingRate:
                    _iceChest.coolingRate += upgrade.Value * 2; // Example calculation
                    break;
            }
        }

        ApplyDebuffs();
    }

    private void ApplyDebuffs()
    {
        // Example: Reducing effectiveness of others based on upgrade levels
        _iceChest.energy -= _upgradeLevels[UpgradeType.Capacity] * 1; // Reduce energy for each capacity upgrade
        _iceChest.coolingRate *= _upgradeLevels[UpgradeType.Capacity] * 0.1f; // Reduce coolingRate for each energy upgrade
        // Add more adjustments as needed
        _playerSpeedDebuff = _upgradeLevels[UpgradeType.Energy] * 0.05f;
        _iceChest.SetMovementPenalty(_playerSpeedDebuff);
    }
}