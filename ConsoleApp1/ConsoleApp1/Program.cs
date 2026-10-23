using System;

class Program
{
    static int playerHP = 100;
    static int playerAtk = 10;
    static int gold = 0;
    static int trainingCount = 0;
    static bool isBossDefeated = false;
    static int explorationProgress = 0;

    static Random random = new Random();

    static void Main()
    {
        Console.WriteLine("=== 모험가 키우기 ===");
        Console.WriteLine("Space: 훈련 / H: 탐험 / B: 보스전");
        Console.WriteLine("R: 상태 확인 / Esc: 종료");

        while (true)
        {
            Console.Write("\n행동할 키를 누르세요: ");
            ConsoleKey key = Console.ReadKey(true).Key;
            Console.WriteLine();

            switch (key)
            {
                case ConsoleKey.Spacebar:
                    MorningTrain();
                    break;

                case ConsoleKey.H:
                    Explore();
                    break;

                case ConsoleKey.B:
                    FightBoss();
                    break;

                case ConsoleKey.R:
                    CheckStatus();
                    break;

                case ConsoleKey.Escape:
                    Console.WriteLine("프로그램을 종료합니다.");
                    return;

                default:
                    Console.WriteLine("안내된 키를 눌러주세요.");
                    break;
            }
        }
    }

    static void MorningTrain()
    {
        if (playerHP <= 0)
        {
            Console.WriteLine("체력이 없어 훈련할 수 없습니다.");
            return;
        }

        if (trainingCount >= 3)
        {
            Console.WriteLine("훈련은 하루에 3번까지만 가능합니다.");
            return;
        }

        Console.WriteLine("\n===== 훈련 시작 =====");

        for (int i = 1; i <= 10; i++)
        {
            Console.WriteLine($"칼을 휘두릅니다! ({i}회)");
        }

        playerAtk += 5;
        trainingCount++;

        Console.WriteLine($"훈련 완료! 현재 공격력 : {playerAtk}");
        Console.WriteLine($"오늘의 훈련 횟수 : {trainingCount} / 3");
    }

    static void Explore()
    {
        if (playerHP <= 0)
        {
            Console.WriteLine("체력이 없어 탐험할 수 없습니다.");
            return;
        }

        explorationProgress = 0;

        Console.WriteLine("\n===== 황야 탐험 시작 =====");

        while (explorationProgress < 100)
        {
            explorationProgress += 20;
            Console.WriteLine($"탐험 진행도 : {explorationProgress}%");

            if (random.Next(0, 100) < 20)
            {
                playerHP -= 10;

                if (playerHP < 0)
                {
                    playerHP = 0;
                }

                Console.WriteLine($"함정에 걸렸습니다! 체력 -10 / 현재 HP : {playerHP}");
            }

            if (playerHP <= 0)
            {
                Console.WriteLine("탐험 실패...");
                break;
            }
        }

        if (explorationProgress >= 100 && playerHP > 0)
        {
            gold += 50;
            Console.WriteLine($"탐험 성공! 골드 +50 / 보유 골드 : {gold}");
        }
    }

    static void FightBoss()
    {
        if (isBossDefeated)
        {
            Console.WriteLine("이미 보스를 처치했습니다.");
            return;
        }

        if (playerHP <= 0)
        {
            Console.WriteLine("체력이 없어 보스전에 도전할 수 없습니다.");
            return;
        }

        int bossHP = 100;
        bool isFirstHit = true;
        int battleTurn = 0;

        Console.WriteLine("\n===== 보스전 입장 =====");

        do
        {
            battleTurn++;

            Console.WriteLine($"\n[전투 {battleTurn}턴]");

            bossHP -= playerAtk;

            if (isFirstHit && bossHP <= 0)
            {
                bossHP = 1;
                Console.WriteLine("보스의 방어막을 부쉈습니다!");
            }

            isFirstHit = false;

            if (bossHP < 0)
            {
                bossHP = 0;
            }

            Console.WriteLine($"플레이어 공격! 데미지 : {playerAtk} / 보스 HP : {bossHP}");
            Console.WriteLine("보스가 반격합니다!");

            playerHP -= 20;

            if (playerHP < 0)
            {
                playerHP = 0;
            }

            Console.WriteLine($"플레이어 HP : {playerHP}");
        }
        while (bossHP > 0 && playerHP > 0);

        if (bossHP <= 0)
        {
            isBossDefeated = true;
            Console.WriteLine("\n보스를 처치했습니다!");
        }

        if (playerHP <= 0)
        {
            Console.WriteLine("플레이어가 사망했습니다...");
        }
    }

    static void CheckStatus()
    {
        Console.WriteLine("\n===== 현재 상태 =====");
        Console.WriteLine($"HP : {playerHP}");
        Console.WriteLine($"공격력 : {playerAtk}");
        Console.WriteLine($"골드 : {gold}");
        Console.WriteLine($"오늘의 훈련 횟수 : {trainingCount} / 3");
        Console.WriteLine($"보스 처치 여부 : {(isBossDefeated ? "처치 완료" : "미처치")}");

        if (playerHP <= 20)
        {
            ConsoleColor previousColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("휴식이 절실합니다...");
            Console.ForegroundColor = previousColor;
        }
    }
}