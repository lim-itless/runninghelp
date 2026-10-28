using System.Collections.Generic;
using UnityEngine;

public class CardDeckManager : MonoBehaviour
{
    [Header("도감")]
    public string[] masterCards = {"용사", "마법사", "궁수", "도둑", "기사", "힐러", "드래곤", "슬라임", "골렘", "암살자"};

    [Header("플레이어 덱")]
    public List<string> playerDeck = new List<string>();

    void Start()
    {
        RefillDeck();
        ShuffleDeck();
        PrintDeck();

        Debug.Log("Space : 드로우 / B : 슬라임 전부 버리기 / I : 긴급 카드 삽입 / R : 빈 덱 리필");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawCard();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            DeleteAll();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            InsertEmergencyCard();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (playerDeck.Count == 0)
            {
                RefillDeck();
                ShuffleDeck();
                PrintDeck();
            }
            else
            {
                Debug.Log("덱이 비었을 때만 리필할 수 있습니다.");
            }
        }
    }

    void RefillDeck()
    {
        if (masterCards == null || masterCards.Length != 10)
        {
            Debug.LogWarning("도감에는 정확히 10장의 카드를 설정해주세요.");
            return;
        }

        playerDeck.Clear();

        int index = 0;

        while (playerDeck.Count < 10)
        {
            if (index >= masterCards.Length)
            {
                index = 0;
            }

            playerDeck.Add(masterCards[index]);
            index++;
        }

        Debug.Log($"리필 완료! 카드 수 : {playerDeck.Count}");
    }

    void ShuffleDeck()
    {
        for (int i = playerDeck.Count - 1; i >= 1; i--)
        {
            int j = Random.Range(0, i + 1);
            Swap(playerDeck, i, j);
        }

        Debug.Log($"셔플 완료! 카드 수 : {playerDeck.Count}");
    }

    void Swap(List<string> list, int a, int b)
    {
        if (list == null)
        {
            return;
        }

        if (a < 0 || a >= list.Count || b < 0 || b >= list.Count)
        {
            return;
        }

        string temp = list[a];
        list[a] = list[b];
        list[b] = temp;
    }

    void PrintDeck()
    {
        Debug.Log($"===== 현재 덱 : {playerDeck.Count}장 =====");

        int number = 1;

        foreach (string card in playerDeck)
        {
            Debug.Log($"{number}번 : {card}");
            number++;
        }
    }

    void DrawCard()
    {
        if (playerDeck.Count == 0)
        {
            Debug.Log("덱이 비었습니다. R을 눌러 리필하세요.");
            return;
        }

        string drawnCard = playerDeck[0];

        Debug.Log($"뽑은 카드 : {drawnCard}");

        playerDeck.RemoveAt(0);

        Debug.Log($"남은 카드 수 : {playerDeck.Count}");
    }

    void DeleteAll()
    {
        int removedCount = 0;

        for (int i = playerDeck.Count - 1; i >= 0; i--)
        {
            if (playerDeck[i] == "슬라임")
            {
                playerDeck.RemoveAt(i);
                removedCount++;
            }
        }

        Debug.Log($"슬라임 {removedCount}장 제거! 남은 카드 수 : {playerDeck.Count}");
    }

    void InsertEmergencyCard()
    {
        playerDeck.Insert(0, "EmergencyHeal");

        Debug.Log($"EmergencyHeal을 맨 위에 넣었습니다! 현재 카드 수 : {playerDeck.Count}");
    }
}