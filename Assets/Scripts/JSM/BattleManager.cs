using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public GameObject[] monsterPrefabs;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public GameObject SpawnMonster(int monsterIndex, Vector3 position)
    {
        if (monsterIndex < 0 || monsterIndex >= monsterPrefabs.Length)
        {
            Debug.LogError($"몬스터 인덱스 {monsterIndex} 오류");
            return null;
        }

        return Instantiate(monsterPrefabs[monsterIndex], position, Quaternion.identity);
    }
}
