using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public static UIScript Instance;
    public GameObject GameoverPanel;
    public bool gameover;

    private void Awake()
    {
        gameover = false;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }
    public void Gameover()
    {
        gameover = true;
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GameoverPanel.SetActive(true);
    }
}
