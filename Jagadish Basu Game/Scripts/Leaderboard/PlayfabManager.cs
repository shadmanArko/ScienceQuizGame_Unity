using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class PlayfabManager : MonoBehaviour
{
    public TMP_InputField nameInputField;

    public GameObject leaderBoardParent;

    public GameObject leaderBoardItem;

    //public ScoreHolder scoreHolder;

    public GameObject leaderBoardStuffs;

    public String myPlayfabID;

    public GameObject yourPreviusScoreisBetterthanYourCurrentScorePanel;

    public GameObject enterYourNamePanel;
    // Start is called before the first frame update
    void Start()
    {
        Login();
    }
    
    public void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnError);
    }

    public void SendLeaderBoard(int cost, string levelNumber)
    {
        Debug.Log("Sent leader");
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = levelNumber,
                    Value = cost
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, OnError);
    }

    [ContextMenu("Get Leader board")]
    public void GetLeaderBoard(string levelNumber)
    {
        Debug.Log(levelNumber);
        var request = new GetLeaderboardRequest()
        {
            StatisticName = levelNumber.ToString(),
            StartPosition = 0,
            MaxResultsCount = 20
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);

    }

    private void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform child in leaderBoardParent.transform)
        {
            Destroy(child.gameObject);
        }
        int positionNumber = 0;
        Debug.Log("Getting Leaderboard");
        Debug.Log(result.Leaderboard.Count);
        for (int i = result.Leaderboard.Count - 1 ; i >= 0; i--)
        {
            positionNumber++;
            GameObject LBitem = Instantiate(leaderBoardItem, leaderBoardParent.transform);

            
            //LBitem.transform.GetChild(0).GetComponent<TMP_Text>().text = positionNumber.ToString();
            LBitem.transform.GetChild(0).GetComponent<TMP_Text>().text = result.Leaderboard[i].DisplayName;
            LBitem.transform.GetChild(1).GetComponent<TMP_Text>().text = result.Leaderboard[i].StatValue.ToString();
            
            Debug.Log(result.Leaderboard[i].Position + "" + result.Leaderboard[i].DisplayName + "" + result.Leaderboard[i].StatValue);
        }
        
    }
    
    
    
    
    
    void GetAccountInfo() {
        GetAccountInfoRequest request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, result => { Debug.Log("playfabId "+result.AccountInfo.PlayFabId); }, OnError);
    }
 
 
    void Successs (GetAccountInfoResult result)
    {      
		
        myPlayfabID = result.AccountInfo.PlayFabId;
 
    }

    public void ChangeDisplayName(String displayName)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = displayName,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
        
    }
    

    private void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult obj)
    {
       Debug.Log("Display Name Updated");
    }


    #region SuccessMassageMethods

    private void OnLoginSuccess(LoginResult result)
    {
        //#if UNITY_EDITOR
        
        Debug.Log("Successful Login / Account Created");
        
        
        //#endif
        
    }
    
    private void OnLeaderBoardUpdate(UpdatePlayerStatisticsResult obj)
    {
        Debug.Log("Successfully Leaderboard Sent");
    }

    #endregion


    #region ErrorMassageMethods

    private void OnError(PlayFabError error)
    {
        //#if UNITY_EDITOR
        Debug.Log(error.GenerateErrorReport());
        
        //#endif
        
    }

    #endregion
    
    
}
