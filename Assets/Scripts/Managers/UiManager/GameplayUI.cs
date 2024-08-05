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

/// <summary>
/// Start is called on the frame when a script is enabled just before
/// any of the Update methods is called the first time.
/// </summary>
  void Start()
  {
      InitElements();
      _scoreManager = GameManager.ScoreManager;
      _playerManager = GameManager.GetPlayerManager();
      _currentScore.text = _scoreManager.CurrentScore.ToString();
      _targetScore.text = _scoreManager.CurrentLevelTargetScore.ToString();
  }

void Update()
{
    _coldOnes.text = _playerManager.GetColdOnes().ToString();
    _warmOnes.text = _playerManager.GetWarmOnes().ToString();
    _currentScore.text = CalculateCurrentScore().ToString();
    _scoreManager.CurrentScore = CalculateCurrentScore();
}
  public void SetRootElement(UIManager uIManager,VisualElement activeUIRoot)
    {   _uiManager = uIManager;
        _root = activeUIRoot;
    }

    public void InitElements()
    {
      _coldOnes = _root.Q<Label>("coldOnes");
      _warmOnes = _root.Q<Label>("warmOnes");
      _targetScore = _root.Q<Label>("targetScore");
      _currentScore = _root.Q<Label>("currentScore");
      _chaseBar = _root.Q<ProgressBar>("chaseBar");


    }

    private int CalculateCurrentScore()
    {   
        int currentScoreSum = 0;

        if(_scoreManager.CurrentScore >= 0){
          int coldScore = _playerManager.GetColdOnes() * _scoreManager.ScorePerBeer;
          int warmScore = _playerManager.GetWarmOnes() * _scoreManager.ScorePerWarmOne;
          currentScoreSum = coldScore + warmScore;
          _scoreManager.CurrentScore += currentScoreSum;
          return currentScoreSum;
        }

        return currentScoreSum;
    }

}