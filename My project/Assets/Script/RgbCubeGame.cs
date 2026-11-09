using UnityEngine;

public class RgbCubeGame : MonoBehaviour
{
    [SerializeField] private Renderer targetCube;
    [SerializeField] private Renderer[] choiceCubes = new Renderer[3];

    private readonly Color[] colors = {Color.red, Color.green, Color.blue};
    private readonly string[] colorNames = { "R", "G", "B" };
    private readonly int[] choiceColors = new int[3];

    private int targetIndex;
    private int score;

    private void Start()
    {
        if (targetCube == null || choiceCubes == null || choiceCubes.Length != 3 || choiceCubes[0] == null || choiceCubes[1] == null || choiceCubes[2] == null)
        {
            Debug.LogError("목표 큐브 1개와 선택 큐브 3개를 연결해주세요.", this);
            enabled = false;
            return;
        }

        ShuffleChoices();
        ShowTarget();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectCube(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectCube(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectCube(2);
        }
    }

    private void SelectCube(int cubeIndex)
    {
        if (choiceColors[cubeIndex] != targetIndex)
        {
            targetIndex = 0;
            Debug.Log("오답.. R부터 다시 시작합니다.", this);
            ShowTarget();
            return;
        }

        targetIndex++;

        if (targetIndex == colors.Length)
        {
            score++;
            targetIndex = 0;
            ShuffleChoices();
        }

        ShowTarget();
    }

    private void ShuffleChoices()
    {
        for (int i = 0; i < choiceColors.Length; i++)
        {
            choiceColors[i] = i;
        }

        for (int i = choiceColors.Length - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i + 1);

            int temp = choiceColors[i];
            choiceColors[i] = choiceColors[j];
            choiceColors[j] = temp;
        }

        for (int i = 0; i < choiceCubes.Length; i++)
        {
            choiceCubes[i].material.color = colors[choiceColors[i]];
        }
    }

    private void ShowTarget()
    {
        targetCube.material.color = colors[targetIndex];
        Debug.Log($"현재 목표 : {colorNames[targetIndex]} / 점수 : {score}", this);
    }

    private void OnGUI()
    {
        if (!enabled) return;

        GUI.Label(new Rect(15, 10, 300, 25), $"현재 목표 : {colorNames[targetIndex]}   |   점수 : {score}");
        GUI.Label(new Rect(15, 35, 300, 25), "순서 : R → G → B   |   선택: 1 / 2 / 3");
    }
}