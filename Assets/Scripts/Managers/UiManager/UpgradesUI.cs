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

    void Start()
    {
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

        _capacityButton = _root.Q<Button>("capacityButton");
        _energyButton = _root.Q<Button>("energyButton");
        _coolingButton = _root.Q<Button>("coolingButton");
        _nextUi = _root.Q<Button>("nextUi");

        _capacityButton.clicked += CapacityButtonClicked;
        _energyButton.clicked += EnergyButtonClicked;
        _coolingButton.clicked += CoolingButtonCLicked;
        _nextUi.clicked += NextUIClicked;
    }

    private void CapacityButtonClicked()
    {
        
    }
    private void EnergyButtonClicked()
    {
        
    }
    private void CoolingButtonCLicked()
    {
        
    }
    private void NextUIClicked()
    {
        
    }
}