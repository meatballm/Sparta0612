using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public SpriteRenderer spriteRenderer;
    [SerializeField] protected float maxHP;
    [SerializeField] protected float enemyHP;
    [SerializeField] private GameObject damageUIPrefab;
    [SerializeField] private Transform canvasTransform;
    protected float chaseRange;
    protected float attackRange;
    protected float speed;
    protected float damage;
    protected float defense;
    protected BoxCollider2D boxCollider;
    protected PlayerStat playerStat;
    public StateMachine_enemy stateMachine {get; private set;}

    private Battle spawnSource;

    private EnemyCondition enemyCondition;

    private bool dead=false;

    public void Init(Battle source)
    {
        Debug.Log(source);
        spawnSource = source;
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerStat = GameObject.FindWithTag("Player").GetComponent<PlayerController>().stats;
        stateMachine = new StateMachine_enemy();
        stateMachine.ChangeState(new IdleState_enemy(this));
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        enemyCondition = GetComponent<EnemyCondition>();
        if (enemyCondition != null)
            enemyCondition.UpdateHealthBar((int)enemyHP, (int)enemyHP); // UI 동기화
    }

    private void Update()
    {
        stateMachine.Update();
    }

    // 플레이어 감지 범위
    public bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, player.position) < chaseRange;
    }

    // 플레이어 공격 범위
    public bool IsPlayerInAttackRange()
    {
        return Vector3.Distance(transform.position, player.position) < attackRange;
    }

    // 움직이는 방향 지정
    public virtual void MoveTowards()
    {
    }

    // 공격
    public virtual void Attack()
    {
    }
    
    // 공격시 데미지
    public void AttackDamag()
    {
        //player.hp - damage;
        //데미지를 입었을 때
    }

    // 피격
    public void TakeDamage(float amount)
    {
        if(dead) return;
        enemyHP -= amount;

        if (enemyCondition != null)
        {
            if (!enemyCondition.IsBarActive())
                enemyCondition.ShowHealthBar();

            enemyCondition.UpdateHealthBar((int)enemyHP, (int)maxHP);
        }

        // 데미지 UI
        if (damageUIPrefab != null)
        {
            Vector3 worldPos = transform.position + Vector3.up * 1.2f;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            var dmgUI = Instantiate(damageUIPrefab, canvasTransform);
            dmgUI.GetComponent<DamageUI>().Initialize((int)amount, screenPos);
        }

        if (enemyHP <= 0)
        {
            dead = true;
            if (enemyCondition != null)
                enemyCondition.HideAndDestroyBar(); // 죽으면 체력바 삭제

            StartCoroutine(Die());
        }
    }

    // 죽음
    protected IEnumerator Die()
    {
        float duration = 1.2f; // Enemy가 사라지는데 걸리는 시간
        float time = 0f;

        Color originalColor = spriteRenderer.color;
        boxCollider.enabled = false;

        while (time < duration)
        {   
            float alpha = Mathf.Lerp(1f, 0f, time / duration); // 점점 투명해지게
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            time += Time.deltaTime;
            yield return null;
        }

        // 마지막에 완전히 투명하게
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        spawnSource?.OnMonsterKilled();
        Destroy(gameObject);
    }

}