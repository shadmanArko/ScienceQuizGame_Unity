using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TriviaQuizGame;

public class LeaderboardManager : MonoBehaviour
{
    public TMP_InputField playerName;
    public TQGGameController gameController;
    public GameObject leaderBoardButton;
    public PlayfabManager playfabManager;
    public string levelName;
    public TMP_InputField nameInputField;
    public void AddScoreToLeaderboard()
    {
        if (string.IsNullOrEmpty(playerName.text))
        {
            return;
        }
        //dl.AddScore(playerName.text , int.Parse(gameController.players[0].score.ToString()));
        playfabManager.SendLeaderBoard((int)gameController.players[0].score, levelName);
        playfabManager.ChangeDisplayName(nameInputField.text);
        leaderBoardButton.SetActive(false);
    }
}
