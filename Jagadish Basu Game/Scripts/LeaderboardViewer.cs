using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardViewer : SerializedMonoBehaviour
{
    private dreamloLeaderBoard leaderBoardObject;
    public GameObject detailPrefab;
    public RectTransform rootForPrefab;
    public GameObject loadingPanel;

    public bool initializer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        leaderBoardObject = GetComponent<dreamloLeaderBoard>();
        leaderBoardObject.GetScores();
        if(initializer)
        {
            Show();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        
        List<dreamloLeaderBoard.Score> scoreList = leaderBoardObject.ToListHighToLow();
        if (scoreList == null)
        {
            loadingPanel.SetActive(false);
            Debug.Log("No Data");
        }
        else
        {
            Debug.Log("Data Count - " + scoreList.Count);
            int maxToDisplay = 20;
            int count = 0;
            loadingPanel.SetActive(true);
            for(int i = rootForPrefab.transform.childCount;i>0;i--)
            {
                DestroyImmediate(rootForPrefab.GetChild(0).gameObject);
            }
            foreach (dreamloLeaderBoard.Score currentScore in scoreList)
            {
                GameObject g = GameObject.Instantiate(detailPrefab, rootForPrefab);
                g.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = currentScore.playerName;
                g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentScore.score.ToString();


                if (count >= maxToDisplay) break;
            }
            loadingPanel.SetActive(false);
        }
    }
}


