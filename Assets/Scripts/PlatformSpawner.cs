using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour {
    public GameObject platformPrefab; // 생성할 발판의 원본 프리팹
    public int count = 3; // 생성할 발판의 개수

    public float timeBetSpawnMin = 1.25f; // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 2.25f; // 다음 배치까지의 시간 간격 최댓값
    private float timeBetSpawn; // 다음 배치까지의 시간 간격

    public float yMin = -3.5f; // 배치할 위치의 최소 y값
    public float yMax = 1.5f; // 배치할 위치의 최대 y값
    private float xPos = 20f; // 배치할 위치의 x 값

    private GameObject[] platforms; // 미리 생성한 발판들
    private int currentIndex = 0; // 사용할 현재 순번의 발판

    private Vector2 poolPosition = new Vector2(0, -25); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치
    private float lastSpawnTime; // 마지막 배치 시점


    void Start() {
        // 변수들을 초기화하고 사용할 발판들을 미리 생성

        platforms = new GameObject[count];

        for (int i=0; i<count; i++)
        {
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity);
        }
        //배치 시점 초기화
        lastSpawnTime = 0f;
        //다음 배치까지 시간간격 초기화
        timeBetSpawn = 0f;

    }

    void Update() {
        // 순서를 돌아가며 주기적으로 발판을 배치

        if (GameManager.instance.isGameover)
        {
            return;
        }

        if(Time.time >= lastSpawnTime + timeBetSpawn)
        {
            //마지막배치 시점 현재시점으로 갱신
            lastSpawnTime = Time.time;
            
            //다음 배치 시간 랜덤
            timeBetSpawn = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            
            //높이 랜덤
            float yPos = Random.Range(yMin, yMax);
            
            //현재 순번 발판 비활성화후 다시 활성화 ???? ok
            // 발판의 Platform 컴포넌트 OnEnable 메서드 실행됨
            platforms[currentIndex].SetActive(false);
            platforms[currentIndex].SetActive(true);

            //현재 순번 발판 오른쪽 재배치
            platforms[currentIndex].transform.position = new Vector2(xPos, yPos);
            //순번넘기기
            currentIndex++;

            //순번 리셋
            if(currentIndex >= count)
            {
                currentIndex = 0;
            }
        }

    }
}