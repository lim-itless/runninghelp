using UnityEngine;

public class MonsterDeckManager : MonoBehaviour
{
    [Header("몬스터 덱")]
    public int[] monsterDeck = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!HasCards())
            {
                return;
            }

            SwapAt(0, monsterDeck.Length - 1);

            Debug.Log("첫 카드와 마지막 카드를 교체했습니다.");
            ShowDeck();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            ShuffleFisherYates();

            Debug.Log("덱을 섞었습니다.");
            ShowDeck();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Battle();
        }
    }

    void SwapAt(int i, int j)
    {
        if (monsterDeck == null)
        {
            return;
        }

        if (i < 0 || i >= monsterDeck.Length || j < 0 || j >= monsterDeck.Length)
        {
            return;
        }

        if (i == j)
        {
            return;
        }

        int temp = monsterDeck[i];
        monsterDeck[i] = monsterDeck[j];
        monsterDeck[j] = temp;
    }

    void ShuffleFisherYates()
    {
        if (monsterDeck == null || monsterDeck.Length <= 1)
        {
            return;
        }

        for (int i = monsterDeck.Length - 1; i >= 1; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            SwapAt(i, randomIndex);
        }
    }

    void Battle()
    {
        if (!HasCards())
        {
            return;
        }

        int playerPower = monsterDeck[0];
        int bossPower = Random.Range(1, 11);

        Debug.Log($"내 카드 공격력 : {playerPower} / 보스 공격력 : {bossPower}");

        if (playerPower > bossPower)
        {
            Debug.Log("승리!");
        }
        else if (playerPower < bossPower)
        {
            Debug.Log("패배...");
        }
        else
        {
            Debug.Log("무승부!");
        }

        ShuffleFisherYates();

        Debug.Log("대결 후 덱을 다시 섞었습니다.");
        ShowDeck();
    }

    bool HasCards()
    {
        if (monsterDeck == null || monsterDeck.Length == 0)
        {
            Debug.LogWarning("덱에 카드를 넣어주세요.");
            return false;
        }

        return true;
    }

    void ShowDeck()
    {
        if (!HasCards())
        {
            return;
        }

        string result = "현재 덱 : ";

        for (int i = 0; i < monsterDeck.Length; i++)
        {
            result += $"[{monsterDeck[i]}] ";
        }

        Debug.Log(result);
    }
}