using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script;
using Assets.Script.Boss;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoControl : MonoBehaviour
{
    [SerializeField] List<video> video = new();
    [SerializeField] VideoPlayer player;
    [SerializeField]BossStateControl _bossStateControl;
    [SerializeField ]GameObject _videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player.started += Play;
       var start = video.Find(x => x.name == "Intro");
        _videoPlayer.SetActive(true);
        player.clip = start.clip;
        player.Play();
        player.loopPointReached += EndVideo;
    }

    public void Play(VideoPlayer source)
    {
        SoundPlay.Instance.PlayBGM();
        player.started -= Play;
    }

    public void Win()
    {
        var start = video.Find(x => x.name == "Ending");
        _videoPlayer.SetActive(true);
        player.clip = start.clip;
        player.Play();
    }

    public void EndVideo(VideoPlayer vp)
    {
        if (vp.clip.name == "Intro")
        {
            _bossStateControl.start = true;
            _videoPlayer.SetActive(false);
            return;
        }
        _videoPlayer.SetActive(false);
        SceneManager.LoadScene(0);
    }

}

[Serializable]
public class video
{
    public string name;
    public VideoClip clip;
}
