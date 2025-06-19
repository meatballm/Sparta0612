using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Gameover : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string sceneName;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
            UIScript.Instance.gameover = false;
        }
        else
        {
            Debug.LogWarning("Scene name is not assigned.");
        }
    }
}
