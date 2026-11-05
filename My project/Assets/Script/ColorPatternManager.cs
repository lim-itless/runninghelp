using UnityEngine;

public class ColorPatternManager : MonoBehaviour
{
    enum GameState
    {
        ShowingPattern,
        PlayerInput,
        RoundResult,
        GameOver
    }

    private GameState state;

    private readonly Color[] colors = {Color.red, Color.green, Color.blue, Color.yellow};
    private readonly string[] colorNames = {"빨강", "초록", "파랑", "노랑"};

    private int[] pattern = new int[4];

    private Renderer cubeRenderer;

    private int round;
    private int correctRounds;
    private int showIndex;
    private int inputIndex;
    private float timer;
    private float displayDuration;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();

        if (cubeRenderer == null)
        {
            enabled = false;
            return;
        }

        RestartGame();
    }

    void Update()
    {
        switch (state)
        {
            case GameState.ShowingPattern:
                UpdatePatternDisplay();
                break;

            case GameState.PlayerInput:
                UpdatePlayerInput();
                break;

            case GameState.RoundResult:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    round++;
                    StartRound();
                }
                break;

            case GameState.GameOver:
                if (Input.GetKeyDown(KeyCode.R))
                {
                    RestartGame();
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    QuitGame();
                }
                break;
        }
    }

    void RestartGame()
    {
        round = 1;
        correctRounds = 0;

        Debug.Log("===== 색 패턴 기억 게임 =====");
        Debug.Log("1 : 빨강 / 2 : 초록 / 3 : 파랑 / 4 : 노랑");

        StartRound();
    }

    void StartRound()
    {
        switch (round)
        {
            case 1:
                displayDuration = 1.5f;
                break;

            case 2:
                displayDuration = 1.0f;
                break;

            default:
                displayDuration = 0.5f;
                break;
        }

        CreatePattern();

        showIndex = 0;
        inputIndex = 0;
        timer = 0f;

        state = GameState.ShowingPattern;

        cubeRenderer.material.color = colors[pattern[showIndex]];

        Debug.Log($"===== {round} / 3 라운드 ===== 색 하나당 {displayDuration:F1}초. 패턴을 기억하세요.");
    }

    void CreatePattern()
    {
        for (int i = 0; i < pattern.Length; i++)
        {
            pattern[i] = i;
        }

        for (int i = pattern.Length - 1; i >= 1; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = pattern[i];
            pattern[i] = pattern[randomIndex];
            pattern[randomIndex] = temp;
        }
    }

    void UpdatePatternDisplay()
    {
        timer += Time.deltaTime;

        if (timer >= displayDuration)
        {
            timer -= displayDuration;
            showIndex++;

            if (showIndex >= pattern.Length)
            {
                cubeRenderer.material.color = Color.gray;
                state = GameState.PlayerInput;

                Debug.Log("패턴 표시 완료! 1~4 키로 순서대로 입력하세요.");
            }
            else
            {
                cubeRenderer.material.color = colors[pattern[showIndex]];
            }
        }
    }

    void UpdatePlayerInput()
    {
        int selectedColor = -1;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedColor = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedColor = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedColor = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedColor = 3;
        }

        if (selectedColor == -1)
        {
            return;
        }

        cubeRenderer.material.color = colors[selectedColor];

        Debug.Log($"{inputIndex + 1}번째 입력 : {colorNames[selectedColor]}");

        if (selectedColor != pattern[inputIndex])
        {
            FinishRound(false);
            return;
        }

        inputIndex++;

        if (inputIndex >= pattern.Length)
        {
            FinishRound(true);
        }
    }

    void FinishRound(bool success)
    {
        if (success)
        {
            correctRounds++;
            cubeRenderer.material.color = Color.green;

            Debug.Log("정답! 패턴을 모두 맞혔습니다.");
        }
        else
        {
            cubeRenderer.material.color = Color.red;

            Debug.Log("오답. 이번 라운드를 종료합니다.");
        }

        if (round >= 3)
        {
            state = GameState.GameOver;

            Debug.Log($"===== 게임 종료 ===== 성공 : {correctRounds} / 3 라운드");
            Debug.Log("R : 전체 재시작 / ESC : 게임 종료");
        }
        else
        {
            state = GameState.RoundResult;
            Debug.Log("Enter 를 누르면 다음 라운드를 시작합니다.");
        }
    }

    void QuitGame()
    {
        Debug.Log("게임을 종료합니다.");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}