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
        if (nextSceneName == "Stage1Scene")
        {
            Camera.main.backgroundColor = HexToColor("474747");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = new Vector3(-3, 2, 0);
            }
        }
        SceneManager.LoadScene(nextSceneName);
    }
    Color HexToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString("#" + hex, out Color color))
            return color;
        return Color.black; // 실패 시 기본값
    }
}
