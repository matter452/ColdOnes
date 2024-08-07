using UnityEngine;
using UnityEngine.UIElements;

public class UpgradesUI : MonoBehaviour
{
    private UIManager _uiManager;
    private VisualElement _root;
    private Label _currentCapacity;
    private Label _currentEnergy;
    private Label _currentCooling;
    private Button _capacityButton;
    private Button _energyButton;
    private Button _coolingButton;
    private Button _nextUi;
    private IceChest _iceChest;
    private PlayerController _player;

    void Start()
    {   
        _player = GameManager.Instance.GetPlayer();
        _iceChest = _player.GetPlayerIceChest();
        InitElements();
    }
    public void SetRootElement(UIManager uIManager,VisualElement activeUIRoot)
    {   _uiManager = uIManager;
        _root = activeUIRoot;
    }

    private void InitElements()
    {
        _currentCapacity = _root.Q<Label>("currentCapacity");
        _currentEnergy = _root.Q<Label>("currentEnergy");
        _currentCooling = _root.Q<Label>("currentCooling");

        _currentCapacity.text = _iceChest.capacity.ToString();
        _currentEnergy.text = _iceChest.energy.ToString();
        _currentCooling.text = _iceChest.coolingRate.ToString();

        _capacityButton = _root.Q<Button>("capacityButton");
        _energyButton = _root.Q<Button>("energyButton");
        _coolingButton = _root.Q<Button>("coolingButton");
        _nextUi = _root.Q<Button>("nextUi");

        _capacityButton.clicked += CapacityButtonClicked;
        _energyButton.clicked += EnergyButtonClicked;
        _coolingButton.clicked += CoolingButtonCLicked;
        _nextUi.clicked += NextUI;
    }

    private void CapacityButtonClicked()
    {
        _iceChest.Upgrade(UpgradeType.Capacity);
        NextUI();
    }
    private void EnergyButtonClicked()
    {
        _iceChest.Upgrade(UpgradeType.Energy);
        NextUI();
    }
    private void CoolingButtonCLicked()
    {
        _iceChest.Upgrade(UpgradeType.CoolingRate);
        NextUI();
    }
    private void NextUI()
    {   
        GameManager.Instance.StartNextLevel();
        _player.SetTransform(GameManager.Instance.transform);
       _iceChest.SetTransform(_player.IceChestSpawn);

        _uiManager.DisplayUI(UIManager.e_UiDocuments.LevelStartUI);
    }
}