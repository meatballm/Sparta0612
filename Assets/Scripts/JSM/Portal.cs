using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Tooltip("이동할 씬 이름")]
    public string nextSceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        SceneManager.LoadScene(nextSceneName);
        if (nextSceneName == "Stage1Scene")
        {
            Camera.main.backgroundColor = HexToColor("474747");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            AudioManager.Instance.PlayBGM(2);
            if (player != null)
            {
                player.transform.position = new Vector3(-3, 2, 0);
            }
        }
        if (nextSceneName == "TownMapEndingScene")
        {
            Camera.main.backgroundColor = HexToColor("474747");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            AudioManager.Instance.PlayBGM(5);
            if (player != null)
            {
                player.transform.position = new Vector3(2.7f, 3.6f, 0);
            }
        }
        if (nextSceneName == "EndingScene")
        {
            GameObject ui = GameObject.Find("UI");
            GameObject dot = GameObject.Find("[DOTween]");
            GameObject character = GameObject.FindWithTag("Player");
            AudioManager.Instance.PlayBGM(1);

            if (ui != null) Destroy(ui);
            if (dot != null) Destroy(dot);
            if (character != null) Destroy(character);
        }
    }
    Color HexToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString("#" + hex, out Color color))
            return color;
        return Color.black; // 실패 시 기본값
    }
}
