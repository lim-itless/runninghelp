using System;

class Program
{
    public static int[] monsterDeck = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

    static Random random = new Random();

    static void Main()
    {
        Console.WriteLine("===== 몬스터 카드 덱 매니저 =====");
        Console.WriteLine("W : 첫 카드와 마지막 카드 교체");
        Console.WriteLine("S : 전체 덱 섞기");
        Console.WriteLine("SPACE : 카드 대결");
        Console.WriteLine("ESC : 종료");

        ShowDeck();

        while (true)
        {
            Console.Write("\n행동할 키를 입력하세요 : ");
            ConsoleKey key = Console.ReadKey(true).Key;
            Console.WriteLine();

            switch (key)
            {
                case ConsoleKey.W:
                    SwapFirstAndLast();
                    break;

                case ConsoleKey.S:
                    Shuffle();
                    Console.WriteLine("덱을 섞었습니다.");
                    ShowDeck();
                    break;

                case ConsoleKey.Spacebar:
                    Battle();
                    break;

                case ConsoleKey.Escape:
                    Console.WriteLine("프로그램을 종료합니다.");
                    return;

                default:
                    Console.WriteLine("W, S, SPACE 또는 ESC를 눌러주세요.");
                    break;
            }
        }
    }

    static void ShowDeck()
    {
        Console.Write("현재 덱 : ");

        for (int i = 0; i < monsterDeck.Length; i++)
        {
            Console.Write($"[{monsterDeck[i]}] ");
        }

        Console.WriteLine();
    }

    static void SwapFirstAndLast()
    {
        int lastIndex = monsterDeck.Length - 1;

        int temp = monsterDeck[0];
        monsterDeck[0] = monsterDeck[lastIndex];
        monsterDeck[lastIndex] = temp;

        Console.WriteLine("첫 카드와 마지막 카드를 교체했습니다!");
        ShowDeck();
    }

    static void Shuffle()
    {
        for (int i = 0; i < monsterDeck.Length; i++)
        {
            int randomIndex = random.Next(0, monsterDeck.Length);

            int temp = monsterDeck[i];
            monsterDeck[i] = monsterDeck[randomIndex];
            monsterDeck[randomIndex] = temp;
        }
    }

    static void Battle()
    {
        int playerPower = monsterDeck[0];
        int bossPower = random.Next(1, 11);

        Console.WriteLine("\n===== 카드 대결 =====");
        Console.WriteLine($"내 카드 공격력 : {playerPower}");
        Console.WriteLine($"보스 공격력 : {bossPower}");

        if (playerPower > bossPower)
        {
            Console.WriteLine("승리!");
        }
        else if (playerPower < bossPower)
        {
            Console.WriteLine("패배!");
        }
        else
        {
            Console.WriteLine("무승부!");
        }

        Shuffle();

        Console.WriteLine("\n대결 후 덱을 다시 섞었습니다.");
        ShowDeck();
    }
}