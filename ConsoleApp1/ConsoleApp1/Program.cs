using System;

class Program
{
    // 약 75분?
    static void Main()
    {
        Random random = new Random();

        Console.WriteLine("===== 로또 당첨기 =====");

        int mode;

        while (true)
        {
            Console.Write("1. 사용자 입력 / 2. 자동 : ");

            if (int.TryParse(Console.ReadLine(), out mode))
            {
                if (mode == 1 || mode == 2)
                {
                    break;
                }
            }

            Console.WriteLine("1 또는 2 를 입력해주세요.");
        }

        int[] playerNumbers;

        if (mode == 1)
        {
            playerNumbers = InputNumbers();
        }
        else
        {
            playerNumbers = CreateNumbers(random, 6);
            Console.WriteLine("자동 번호를 생성했습니다.");
        }

        int[] winningNumbers = CreateNumbers(random, 7);

        int bonusNumber = winningNumbers[6];

        int matchCount = 0;

        for (int i = 0; i < playerNumbers.Length; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (playerNumbers[i] == winningNumbers[j])
                {
                    matchCount++;
                    break;
                }
            }
        }

        bool bonusMatch = false;

        for (int i = 0; i < playerNumbers.Length; i++)
        {
            if (playerNumbers[i] == bonusNumber)
            {
                bonusMatch = true;
                break;
            }
        }

        Array.Sort(playerNumbers);
        Array.Sort(winningNumbers, 0, 6);

        Console.WriteLine("\n===== 추첨 결과 =====");
        Console.Write("내 번호 : ");
        PrintNumbers(playerNumbers, 6);

        Console.Write("당첨번호 : ");
        PrintNumbers(winningNumbers, 6);

        Console.WriteLine($"보너스 번호 : {bonusNumber}");
        Console.WriteLine($"당첨번호 일치 : {matchCount}개");
        Console.WriteLine($"보너스 일치 : {(bonusMatch ? "O" : "X")}");

        switch (matchCount)
        {
            case 6:
                Console.WriteLine("축하합니다! 1등입니다!!!");
                break;

            case 5:
                if (bonusMatch)
                {
                    Console.WriteLine("축하합니다! 2등입니다.");
                }
                else
                {
                    Console.WriteLine("축하합니다! 3등입니다.");
                }
                break;

            case 4:
                Console.WriteLine("축하합니다! 4등입니다.");
                break;

            case 3:
                Console.WriteLine("축하합니다! 5등입니다.");
                break;

            default:
                Console.WriteLine("낙첨입니다...");
                break;
        }
    }

    static int[] InputNumbers()
    {
        int[] numbers = new int[6];

        Console.WriteLine("\n1 ~ 45 사이의 번호를 하나씩 입력해주세요.");

        for (int i = 0; i < numbers.Length;)
        {
            Console.Write($"{i + 1} 번째 번호 : ");

            if (!int.TryParse(Console.ReadLine(), out int number))
            {
                Console.WriteLine("정수만 입력할 수 있습니다.");
                continue;
            }

            if (number < 1 || number > 45)
            {
                Console.WriteLine("1 ~ 45 사이로 입력해주세요.");
                continue;
            }

            bool duplicate = false;

            for (int j = 0; j < i; j++)
            {
                if (numbers[j] == number)
                {
                    duplicate = true;
                    break;
                }
            }

            if (duplicate)
            {
                Console.WriteLine("이미 입력한 번호입니다.");
                continue;
            }

            numbers[i] = number;
            i++;
        }

        return numbers;
    }

    static int[] CreateNumbers(Random random, int count)
    {
        int[] numbers = new int[count];

        for (int i = 0; i < numbers.Length;)
        {
            int number = random.Next(1, 46);
            bool duplicate = false;

            for (int j = 0; j < i; j++)
            {
                if (numbers[j] == number)
                {
                    duplicate = true;
                    break;
                }
            }

            if (duplicate)
            {
                continue;
            }

            numbers[i] = number;
            i++;
        }

        return numbers;
    }

    static void PrintNumbers(int[] numbers, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Console.Write($"{numbers[i]} ");
        }

        Console.WriteLine();
    }
}