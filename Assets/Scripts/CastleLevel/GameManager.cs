using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] float finishY;
    [SerializeField] GameObject levelOverPanel;
    private int livesLeft;
    private int currentGold;
    private TextMeshProUGUI livesText;
    private TextMeshProUGUI goldText;
    private TextMeshProUGUI soulsText;
    private float globalGoldMult;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this);
        } else {
            Instance = this;
        }

        livesText = GameObject.Find("/Canvas/LivesImg/LivesText").GetComponent<TextMeshProUGUI>();
        goldText = GameObject.Find("/Canvas/GoldImg/GoldText").GetComponent<TextMeshProUGUI>();
        soulsText = GameObject.Find("/Canvas/SoulsImg/SoulsText").GetComponent<TextMeshProUGUI>();

        currentGold = DataManager.Instance.GetStartingGold();
        livesLeft = DataManager.Instance.GetStartingLives();
        globalGoldMult = 1f;

        livesText.SetText(livesLeft.ToString());
        goldText.SetText(currentGold.ToString());
        soulsText.SetText(DataManager.Instance.GetSouls().ToString());
    }

    public void RemoveLives(int lives) {
        if (livesLeft <= lives) {
            livesLeft = 0;
            livesText.SetText(livesLeft.ToString());
            GameOver(false, 0);
        } else {
            livesLeft -= lives;
            livesText.SetText(livesLeft.ToString());
        }
    }

    public void ChangeGold(int amt) {
        currentGold += (int)(amt * globalGoldMult);
        goldText.SetText(currentGold.ToString());
        Actions.Instance.GoldChanged();
    }

    public void AddToGoldMult(float amt) {
        globalGoldMult += amt;
    }

    public void AddSouls(int amt) {
        DataManager.Instance.ChangeSouls(amt);
        soulsText.SetText(DataManager.Instance.GetSouls().ToString());
    }

    public int GetGold() {
        return currentGold;
    }

    public void GameOver(bool won, int levelIndex) {
        if (won) {
            levelOverPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Victory!";
            levelOverPanel.SetActive(true);
            DataManager.Instance.SaveOnGameOver(levelIndex);
        } else {
            levelOverPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Defeat";
            levelOverPanel.SetActive(true);
            DataManager.Instance.SaveOnGameOver(0);
        }
    }

    public float GetFinishY() {
        return finishY;
    }
}
