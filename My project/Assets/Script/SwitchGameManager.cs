using UnityEngine;

public class SwitchGameManager : MonoBehaviour
{
    public Renderer[] switches = new Renderer[3];

    private int answerIndex;
    private int currentRound = 0;
    private int correctCount = 0;

    private bool waitingForNextRound = false;
    private bool gameEnded = false;
    private float nextRoundTime;

    private const int TotalRounds = 10;

    void Start()
    {
        if (switches == null || switches.Length != 3)
        {
            Debug.LogError("스위치 배열 크기를 3 으로 설정해주세요.");
            enabled = false;
            return;
        }

        for (int i = 0; i < switches.Length; i++)
        {
            if (switches[i] == null)
            {
                Debug.LogError($"{i + 1}번 스위치의 Renderer를 연결해주세요.");
                enabled = false;
                return;
            }
        }

        StartRound();
    }

    void Update()
    {
        if (gameEnded)
        {
            return;
        }

        if (waitingForNextRound)
        {
            if (Time.time >= nextRoundTime)
            {
                if (currentRound >= TotalRounds)
                {
                    EndGame();
                }
                else
                {
                    StartRound();
                }
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log($"[치트] 정답 인덱스 : {answerIndex} / 선택 키 : {answerIndex + 1}");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectSwitch(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectSwitch(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectSwitch(2);
        }
    }

    void StartRound()
    {
        currentRound++;
        waitingForNextRound = false;

        for (int i = 0; i < switches.Length; i++)
        {
            switches[i].material.color = Color.gray;
        }

        answerIndex = Random.Range(0, 3);

        Debug.Log($"===== {currentRound} / {TotalRounds} 라운드 ===== 1, 2, 3 중 하나를 선택하세요.");
    }

    void SelectSwitch(int selectedIndex)
    {
        bool isCorrect = selectedIndex == answerIndex;
        Color resultColor;

        if (isCorrect)
        {
            correctCount++;
            resultColor = Color.green;
            Debug.Log("정답!!");
        }
        else
        {
            resultColor = Color.red;
            Debug.Log($"오답.. 정답은 {answerIndex + 1}번 스위치였습니다.");
        }

        switches[selectedIndex].material.color = resultColor;

        Debug.DrawLine(transform.position, switches[selectedIndex].bounds.center, resultColor, 1f, false);

        waitingForNextRound = true;
        nextRoundTime = Time.time + 1f;
    }

    void EndGame()
    {
        gameEnded = true;
        float accuracy = (float)correctCount / TotalRounds * 100f;

        Debug.Log($"===== 게임 종료 ===== 정답 : {correctCount} / {TotalRounds} / 정답률 : {accuracy:F1}%");
    }
}