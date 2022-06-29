using System.Collections;
using System.Collections.Generic;
using LightShaft.Scripts;
using TMPro;
using TriviaQuizGame;
using UnityEngine;
using UnityEngine.UI;

public class JagdishVideoLibrary : MonoBehaviour
{

    public YoutubePlayer youtubePlayer;

    public GameObject videoThumbPrefab;

    public RectTransform videoLibraryRoot;

    public GameObject YoutubeCanvas;
    public AudioSource MusicSource;

    private TQGGlobalMusic globalMusic;

   
    void Start()
    {
        GenerateButtons();
        globalMusic = GameObject.FindObjectOfType<TQGGlobalMusic>();
        MusicSource = globalMusic.GetComponent<AudioSource>();

    }

    public void GenerateButtons()
    {
        var videoData = VideoLinkResources.VideoLibrary;
        foreach (KeyValuePair<string,string> data in videoData)
        {
            GameObject g = GameObject.Instantiate(videoThumbPrefab, videoLibraryRoot);
            g.GetComponentInChildren<TextMeshProUGUI>().text = data.Key;
            g.GetComponent<Button>().onClick.AddListener(() =>
            {
                PlayVideo(data.Value);
            });
        }
    }

    public void PlayVideo(string videoLink)
    {
        MusicSource.Stop();
        YoutubeCanvas.SetActive(true);
        //youtubePlayer.videoUrl = videoLink;
        youtubePlayer.PreLoadVideo(videoLink);
        youtubePlayer.Play();
    }

    public void StopVideo()
    {
        youtubePlayer.Stop();
        MusicSource.Play();
        YoutubeCanvas.SetActive(false);
    }

    
}
