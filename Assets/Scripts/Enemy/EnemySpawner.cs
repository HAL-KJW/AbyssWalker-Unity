using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

/// <summary>
/// 적 스폰 클래스
/// 현재기준 플레이어 주변에 일정 간격으로 적을 스폰하는 방식
/// 화면 밖 + 바닥 위에 정확히 스폰되도록 처리
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 설정")]
    [SerializeField] private float spawnInterval = 2f;   // 스폰 간격
    [SerializeField] private float spawnRadius = 15f;    // 플레이어로부터의 스폰거리
    [SerializeField] private float minSpawnRadius = 10f; // 플레이어로부터의 최소 스폰거리
    [SerializeField] private float maxEnemyCount = 2f;   // 최대 적 수
    [SerializeField] private float spawnHeight = 2f;     // 바닥 체크용 Raycast 시작 높이

    [Header("웨이브 설정")]
    [SerializeField] private float difficultyInterval = 30;     // 난이도 상승 간격
    [SerializeField] private float spawnRateIncrease = 0.1f;    // 스폰 간격 감소량

    private Transform _playerTransform;
    private float _spawnTimer;
    private float _difficultyTimer;
    private int _currentEnemyCount;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // 태그로 플레이어 찾기
        if (player != null)
        {
            _playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player(Tag)를 찾을수없습니다.");
        }
    }

    private void Update()
    {
        if(_playerTransform == null) return;
        if(EnemyPool.Instance == null) return;

        UpdateDifficulty();
        UpdateSpawn();
    }

    /// <summary>
    /// 시간이 지날수록 스폰 간격 감소(난이도 상승용)
    /// </summary>
    private void UpdateDifficulty()
    {
        _difficultyTimer += Time.deltaTime;
        if (_difficultyTimer >= difficultyInterval)
        {
            _difficultyTimer = 0f;
            spawnInterval = Mathf.Max(0.3f, spawnInterval - spawnRateIncrease);
            Debug.Log($"[Spawner] 난이도 상승,스폰 간격: {spawnInterval:F1}초");
        }
    }

    private void UpdateSpawn()
    {
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer < spawnInterval) return; 
        _spawnTimer = 0f;

        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        // 플레이어 주변 랜덤 위치 계산
        Vector3 spawnPosition;
        if (!TryGetSpawnPosition(out spawnPosition)) return; 

        EnemyPool.Instance.Spawn(spawnPosition); // 오브젝트 풀에서 적 스폰

    }

    /// <summary>
    /// 바닥 위에 스폰되는 위치를 계산
    /// Raycast를 이용하여 바닥 체크
    /// </summary>
    private bool TryGetSpawnPosition(out Vector3 position)
    {
        position = Vector3.zero; // 초기값

        // 플레이어 주변 원형으로 랜덤 방향계산
        float angle = Random.Range(0f, 360f);
        float distance = Random.Range(minSpawnRadius, spawnRadius);
        Vector3 offset = new Vector3(
             Mathf.Cos(angle * Mathf.Deg2Rad) * distance,
             0f,
             Mathf.Sin(angle * Mathf.Deg2Rad) * distance
         );

        Vector3 rawPosition = _playerTransform.position + offset; // 스폰 위치의 높이를 spawnHeight로 설정

        // 위에서 아래로 Raycast쏴서 바닥 체크
        Vector3 rayOrigin =rawPosition + Vector3.up * spawnHeight;
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, spawnHeight * 2f))
        {
            // 바닥 표면 위에 캐릭터 높이만큼 올려서 스폰
            position = hit.point + Vector3.up * 1f; // 1f = 캡슐 절반 높이
            return true;
        }

        // 바닥을 못 찾으면 플레이어와 같은 Y에 스폰 
        position = new Vector3(rawPosition.x, _playerTransform.position.y, rawPosition.z);
        return true;
    }

}