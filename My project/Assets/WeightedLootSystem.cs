using System.Collections.Generic;
using UnityEngine;

public class WeightedLootSystem : MonoBehaviour
{
    private readonly string[] monsterPool = { "늑대", "고블린", "슬라임", "해골 병사", "오크" };
    private readonly string[] roomPool = { "어두운 복도", "버려진 도서관", "지하 감옥", "수정 동굴", "제단의 방" };
    private readonly string[] itemNames = { "쓰레기", "일반 검", "희귀 방패", "전설의 보석" };
    private readonly int[] weights = { 700, 200, 90, 10 };

    private Queue<string> monsterQueue = new Queue<string>();
    private Stack<string> travelHistory = new Stack<string>();
    private Stack<string> lootHistory = new Stack<string>();

    private int undoLeft = 3;
    private int drawCount = 0;

    private void Start()
    {
        Debug.Log("===== 던전 탐험 시작 =====");
        Debug.Log("S : 정찰 / A : 전투 / M : 이동 / B : 퇴각 / D : 아이템 뽑기 / U : Undo");
        Debug.Log("현재 위치 : 던전 입구 / Undo는 실행 중 총 3번 가능합니다.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) Scout();
        if (Input.GetKeyDown(KeyCode.A)) Battle();
        if (Input.GetKeyDown(KeyCode.M)) MoveRoom();
        if (Input.GetKeyDown(KeyCode.B)) Retreat();
        if (Input.GetKeyDown(KeyCode.D)) Draw();
        if (Input.GetKeyDown(KeyCode.U)) Undo();
    }

    private void Scout()
    {
        string monster = monsterPool[Random.Range(0, monsterPool.Length)];
        monsterQueue.Enqueue(monster);
        Debug.Log($"[조우 예고] 저 멀리서 {monster}이/가 나타났습니다! 대기 중 : {monsterQueue.Count}마리");
    }

    private void Battle()
    {
        if (monsterQueue.Count == 0)
        {
            Debug.Log("[전투] 발견된 몬스터가 없습니다. 정찰이 필요합니다.");
            return;
        }

        string monster = monsterQueue.Dequeue();
        Debug.Log($"[전투] {monster}을/를 처치했습니다! 남은 적 : {monsterQueue.Count}마리");
    }

    private void MoveRoom()
    {
        string room = roomPool[Random.Range(0, roomPool.Length)];
        travelHistory.Push(room);
        Debug.Log($"[이동] {room}에 진입했습니다. (총 이동 거리 : {travelHistory.Count})");
    }

    private void Retreat()
    {
        if (travelHistory.Count == 0)
        {
            Debug.Log("던전 입구까지 돌아왔습니다. 더 이상 퇴각할 수 없습니다.");
            return;
        }

        string exitedRoom = travelHistory.Pop();
        Debug.Log($"[퇴각] {exitedRoom}에서 나왔습니다.");

        if (travelHistory.Count > 0)
        {
            string previousRoom = travelHistory.Peek();
            Debug.Log($"[퇴각] {previousRoom}으로 돌아왔습니다.");
        }
        else
        {
            Debug.Log("던전 입구까지 돌아왔습니다. 더 이상 퇴각할 수 없습니다.");
        }
    }

    //---------------------------------------- 심화 도전

    private void Draw()
    {
        if (itemNames.Length == 0 || itemNames.Length != weights.Length)
        {
            Debug.LogWarning("아이템 배열과 가중치 배열을 확인하세요.");
            return;
        }

        int totalWeight = 0;

        for (int i = 0; i < weights.Length; i++)
        {
            if (weights[i] < 0)
            {
                Debug.LogWarning("가중치는 음수일 수 없습니다.");
                return;
            }

            totalWeight += weights[i];
        }

        if (totalWeight <= 0)
        {
            Debug.LogWarning("가중치 합계는 0보다 커야 합니다.");
            return;
        }

        int r = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        for (int i = 0; i < weights.Length; i++)
        {
            cumulativeWeight += weights[i];

            if (cumulativeWeight > r)
            {
                string item = itemNames[i];
                lootHistory.Push(item);
                drawCount++;

                Debug.Log($"[뽑기 {drawCount}회] 획득 : {item} (r={r}) / 보유 아이템 : {lootHistory.Count}개");
                return;
            }
        }
    }

    private void Undo()
    {
        if (undoLeft <= 0)
        {
            Debug.Log("[Undo 불가] Undo 3회를 모두 사용했습니다.");
            return;
        }

        if (lootHistory.Count == 0)
        {
            Debug.Log("[Undo 불가] 취소할 획득 기록이 없습니다.");
            return;
        }

        string cancelledItem = lootHistory.Pop();
        undoLeft--;

        Debug.Log($"취소 : {cancelledItem} / 남은 Undo : {undoLeft}");

        Draw();
    }
}
