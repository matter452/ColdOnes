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
                    _iceChest.energy += _iceChest.baseEnergy *(0.1f*upgrade.Value);
                    break;
                case UpgradeType.Capacity:
                    _iceChest.capacity += upgrade.Value * 6; 
                    break;
                case UpgradeType.CoolingRate:
                    _iceChest.coolingRate += upgrade.Value * 2; 
                    break;
            }
        }

        ApplyDebuffs();
    }

    private void ApplyDebuffs()
    {
        _iceChest.energy -= _upgradeLevels[UpgradeType.CoolingRate] * 1;
        _iceChest.coolingRate *= 1-_upgradeLevels[UpgradeType.Capacity] * 0.05f; 
        _playerSpeedDebuff = _upgradeLevels[UpgradeType.Energy] * 0.05f;
        _iceChest.SetMovementPenalty(_playerSpeedDebuff);
    }
}