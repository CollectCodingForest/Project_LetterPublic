using UnityEngine;

public class CircleManager : MonoBehaviour
{
    [SerializeField] private Circle[] circles;
    private int currentIndex;

    private void Start()
    {
        currentIndex = circles.Length - 1; // 초기 인덱스를 배열의 마지막 인덱스로 설정
        GameSceneManager.Instance.eventSubject.OnCounter += IncrementCount;
    }

    private void OnDisable()
    {
        GameSceneManager.Instance.eventSubject.OnCounter -= IncrementCount;  // 씬 전환시 실행됨.
    }

    public void IncrementCount()
    {
        if (currentIndex >= 0 && !circles[currentIndex].IsFilled)
        {
            circles[currentIndex].IncrementCount();

            if (circles[currentIndex].IsFilled)
            {
                currentIndex--;
            }
        }
        else if (currentIndex < 0)
        {
            // 위에서 3번 틀렸으니 이제 벌금카운트 실행
            GameSceneManager.Instance.playData.FineCount++;

        }
    }


}
