using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    [SerializeField]private GameObject[]title;
    private Animator anim;
    private bool started = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        startButton.onClick.AddListener(StartMenu);
    }

    void StartMenu()
    {
        
            started = true;
            anim.SetTrigger("Start");
        
    }

    void OnDestroy()
    {
        startButton.onClick.RemoveAllListeners();
    }

    // CALLED BY ANIMATION EVENT
    public void LoadGameScene()
    {
        foreach(GameObject item in title){item.SetActive(false);}
        SceneManager.LoadScene("Player");
    }
}
