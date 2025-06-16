using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
    [Header("몬스터 리스트")]
    public GameObject[] monsterPrefabs;
    [Header("방 클리어시 보상 리스트")]
    public GameObject[] rewardItemPrefabs;
    [Header("방 클리어시 보상 생성 확률")]
    public float rewardChance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
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
    public void SpawnReward(Vector3 playerPosition)
    {
        if (Random.value > rewardChance)
            return;

        if (rewardItemPrefabs == null || rewardItemPrefabs.Length == 0)
        {
            Debug.LogWarning("보상 아이템이 등록되지 않았습니다.");
            return;
        }

        int index = Random.Range(0, rewardItemPrefabs.Length);
        GameObject reward = Instantiate(rewardItemPrefabs[index], playerPosition, Quaternion.identity);

        Debug.Log("보상 아이템 생성됨: " + reward.name);
    }
}
