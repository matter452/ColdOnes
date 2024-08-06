using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayUI : MonoBehaviour
{
  private UIManager _uiManager;
  private VisualElement _root;
  private Label _coldOnes;
  private Label _warmOnes;
  private Label _targetScore;
  private Label _currentScore;
  private ProgressBar _chaseBar;
  private ScoreManager _scoreManager;
  private PlayerManager _playerManager;
  private VisualElement _playerIcon;
  private VisualElement _targetIcon;
  private VisualElement _chaserIcon;
  public Transform playerTransform;
  public Transform targetTransform;
  public Transform tempchaserTransform;
  public Transform chaserTransform;

/// <summary>
/// Start is called on the frame when a script is enabled just before
/// any of the Update methods is called the first time.
/// </summary>
  void Start()
  {
      InitElements();
      _playerManager = GameManager.GetPlayerManager();
      _scoreManager = GameManager.GetScoreManager();
      if (_playerManager == null || _scoreManager == null)
    {
        Debug.LogError("PlayerManager or ScoreManager is null");
        return;
    }
      playerTransform = _playerManager.GetPlayer().gameObject.transform;
      _chaseBar.highValue = _scoreManager.CurrentLevelTargetScore;
      _chaseBar.lowValue = _scoreManager.CurrentScore;
      tempchaserTransform = playerTransform;
      tempchaserTransform.Translate(new Vector3(0f,0f,-20f));
      chaserTransform = tempchaserTransform;
      _targetScore.text =  _scoreManager.CurrentScore.ToString() + " / " +_scoreManager.CurrentLevelTargetScore.ToString();
      UpdateColds(_playerManager.Colds);
      UpdateWarms(_playerManager.Warms);
      _playerManager.onColdsChanged += UpdateColds;
      _playerManager.onWarmsChanged += UpdateWarms;
  }

void Update()
{
    _coldOnes.text = "Cold ones: ";/*+ _playerManager.GetColdOnes().ToString(); */
    _warmOnes.text = "Warm ones: "; /*+ _playerManager.GetWarmOnes().ToString();  */
    _targetScore.text =  _scoreManager.CurrentScore.ToString() + " / " +_scoreManager.CurrentLevelTargetScore.ToString();
    _scoreManager.CurrentScore = CalculateCurrentScore();
     UpdateProgressBar();
}
  public void SetRootElement(UIManager uIManager,VisualElement activeUIRoot)
    {   _uiManager = uIManager;
        _root = activeUIRoot;
    }
  

  private void UpdateColds(int newCountColds)
  {
    _coldOnes.text = "Cold ones: " + newCountColds.ToString();
  }

    private void UpdateWarms(int newCountWarms)
  {
    _warmOnes.text = "Warm ones: " + newCountWarms.ToString();
    
  }
    public void InitElements()
    {

      if (_root == null)
    {
        Debug.LogError("_root is null");
        return;
    }
      _coldOnes = _root.Q<Label>("coldOnes");
      _warmOnes = _root.Q<Label>("warmOnes");
      _targetScore = _root.Q<Label>("targetScore");
      _currentScore = _root.Q<Label>("currentScore");
      _chaseBar = _root.Q<ProgressBar>("chaseBar");
      _playerIcon = _root.Q<VisualElement>("playerIcon");
      _targetIcon = _root.Q<VisualElement>("targetIcon");
      _chaserIcon = _root.Q<VisualElement>("chaserIcon");
      if (_coldOnes == null || _warmOnes == null || _targetScore == null || _chaseBar == null)
      {
        Debug.LogError("UI elements not found");
      }

    }

    private int CalculateCurrentScore()
    {   
        int currentScoreSum = 0;

        if(_scoreManager.CurrentScore >= 0){
          int coldScore = _playerManager.Colds * _scoreManager.ScorePerColdOne;
          int warmScore = _playerManager.Warms * _scoreManager.ScorePerWarmOne;
          currentScoreSum = coldScore + warmScore;
          _scoreManager.CurrentScore += currentScoreSum;
          return currentScoreSum;
        }

        return currentScoreSum;
    }

  private void UpdateIconPosition(VisualElement icon, float progress)
    {
        float barWidth = _chaseBar.resolvedStyle.width;
        icon.style.left = progress * barWidth;
    }
    private void UpdateProgressBar()
    {
        if (targetTransform != null && playerTransform != null)
        {
            float totalDistance = Vector3.Distance(playerTransform.position, targetTransform.position);
            float chaserDistance = Vector3.Distance(playerTransform.position, chaserTransform.position);

            float progress = 1f - Mathf.Clamp01(totalDistance / Vector3.Distance(playerTransform.position, targetTransform.position));

            // Update progress bar
            _chaseBar.value = progress * 100;

            // Update icos
            UpdateIconPosition(_playerIcon, progress);
            UpdateIconPosition(_targetIcon, 1f);
            UpdateIconPosition(_chaserIcon, chaserDistance / totalDistance);
        }
        else
        {
            _chaseBar.value = 0;
        }
    }
}