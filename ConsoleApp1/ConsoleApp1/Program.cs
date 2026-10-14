using System;

class Program
{
    static void Main()
    {
        PrintMonthDays();
        PlayRockPaperScissors();
    }
    static void PrintMonthDays()
    {
        int count = 0;

        Console.WriteLine("===== 일수 출력기 =====");

        while (count < 3)
        {
            Console.Write("월을 입력하세요.(1~12) : ");

            if (!int.TryParse(Console.ReadLine(), out int month))
            {
                Console.WriteLine("정수로 입력하세요.\n");
                continue;
            }

            int days;

            switch (month)
            {
                case 1:
                case 2:
                    days = 28;
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                    days = 30;
                    break;
                case 12:
                    days = 31;
                    break;
                default:
                    Console.WriteLine("월을 입력하세요.\n");
                    continue;
            }

            count++;

            Console.WriteLine($"{month}월은 {days}일까지 있습니다.");
            Console.WriteLine($"입력 횟수 : {count} / 3\n");
        }

        Console.WriteLine("일수 출력기를 종료합니다.\n");
    }

    static void PlayRockPaperScissors()
    {
        Random random = new Random();

        long money = 10000;
        int round = 0;

        Console.WriteLine("===== 가위바위보 =====");
        Console.WriteLine("승리   : 배팅액 × 3 획득");
        Console.WriteLine("패배   : 배팅액 × 7 차감");
        Console.WriteLine("무승부 : 배팅액 × 5 획득");

        int cheatChoice = ReadInt("1 - 치트 사용 / 0 - 사용 안 함 : ", 0, 1);

        bool cheatEnabled = cheatChoice == 1;

        while (round < 5 && money > 0)
        {
            if (money < 1000)
            {
                Console.WriteLine("최소 배팅액이 부족하여 종료합니다.");
                break;
            }

            Console.WriteLine($"\n----- {round + 1}번째 판 -----");
            Console.WriteLine($"현재 소지금 : {money:N0}원");

            long bet = ReadBet(money);

            int computer = random.Next(1, 4);

            if (cheatEnabled)
            {
                Console.WriteLine($"치트 - 컴퓨터의 선택 : {GetHandName(computer)}");
            }

            int player = ReadInt("선택하세요 (1 - 가위 / 2 - 바위 / 3 - 보) : ", 1, 3);

            int result = 0;

            switch (player)
            {
                case 1:
                    if (computer == 3)
                    {
                        result = 1;
                    }
                    else if (computer == 2)
                    {
                        result = -1;
                    }
                    break;

                case 2:
                    if (computer == 1)
                    {
                        result = 1;
                    }
                    else if (computer == 3)
                    {
                        result = -1;
                    }
                    break;

                case 3:
                    if (computer == 2)
                    {
                        result = 1;
                    }
                    else if (computer == 1)
                    {
                        result = -1;
                    }
                    break;
            }

            Console.WriteLine($"나 : {GetHandName(player)} / 컴퓨터 : {GetHandName(computer)}");

            switch (result)
            {
                case 1:
                    long winGold = bet * 3;
                    money += winGold;
                    Console.WriteLine($"승리! {winGold:N0}원 획득!");
                    break;

                case -1:
                    long lossGold = bet * 7;

                    if (lossGold > money)
                    {
                        lossGold = money;
                    }

                    money -= lossGold;
                    Console.WriteLine($"패배! {lossGold:N0}원 차감...");
                    break;

                case 0:
                    long drawGold = bet * 5;
                    money += drawGold;
                    Console.WriteLine($"무승부! {drawGold:N0}원 획득.");
                    break;
            }

            round++;
            Console.WriteLine($"남은 소지금 : {money:N0}원");
        }

        Console.WriteLine("\n===== 게임 종료 =====");

        if (money == 0)
        {
            Console.WriteLine("소지금을 전부 잃었습니다.");
        }
        else if (round == 5)
        {
            Console.WriteLine("5판을 모두 진행했습니다.");
        }

        Console.WriteLine($"진행한 판 수 : {round}");
        Console.WriteLine($"최종 소지금 : {money:N0}원");
    }

    static int ReadInt(string message, int min, int max)
    {
        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out int value))
            {
                if (value >= min && value <= max)
                {
                    return value;
                }
            }

            Console.WriteLine($"{min}~{max} 사이의 정수를 입력해주세요.");
        }
    }

    static long ReadBet(long money)
    {
        while (true)
        {
            Console.Write($"배팅액 (1,000~{money:N0}원) : ");

            if (long.TryParse(Console.ReadLine(), out long bet))
            {
                if (bet >= 1000 && bet <= money)
                {
                    return bet;
                }
            }

            Console.WriteLine("1,000원 이상, 현재 소지금 이하로 입력해주세요.");
        }
    }

    static string GetHandName(int hand)
    {
        switch (hand)
        {
            case 1:
                return "가위";

            case 2:
                return "바위";

            case 3:
                return "보";

            default:
                return "잘못된 선택";
        }
    }
}