using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{

    public Text scoreText;
    private int score = 0;

    // Update is called once per frame
    void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score;
        if (score >= 12)
        {
            scoreText.text = "Win! Score:" + score;
            GameObject.Find("enemy").SetActive(false);
        }
    }
    public void addScore()
    {
        UpdateScore();
    }

}
