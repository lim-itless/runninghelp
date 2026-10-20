using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n===== 문자열 활용 =====");
            Console.WriteLine("1) 문자열 거꾸로 출력");
            Console.WriteLine("2) 짝수 번째 문자끼리 뒤집기");
            Console.WriteLine("3) 숫자만 출력");
            Console.WriteLine("4) 특정 문자 개수 세기");
            Console.WriteLine("5) 주민번호의 하이픈과 공백 제거");
            Console.WriteLine("0) 종료");
            Console.Write("선택 : ");

            string menu = Console.ReadLine() ?? "";

            switch (menu)
            {
                case "1":
                    ReverseText();
                    break;

                case "2":
                    ReverseEvenPositions();
                    break;

                case "3":
                    PrintNumbersOnly();
                    break;

                case "4":
                    CountCharacter();
                    break;

                case "5":
                    RemoveHyphen();
                    break;

                case "0":
                    Console.WriteLine("프로그램을 종료합니다.");
                    return;

                default:
                    Console.WriteLine("0 ~ 5 중에서 선택해주세요.");
                    break;
            }
        }
    }

    static void ReverseText()
    {
        Console.Write("문자열 입력 : ");
        string text = Console.ReadLine() ?? "";

        Console.Write("결과 : ");

        for (int i = text.Length - 1; i >= 0; i--)
        {
            Console.Write(text[i]);
        }

        Console.WriteLine();
    }

    static void ReverseEvenPositions()
    {
        Console.Write("문자열 입력 : ");
        string text = Console.ReadLine() ?? "";

        char[] characters = text.ToCharArray();

        int left = 1;
        int right = text.Length - 1;

        if (right % 2 == 0)
        {
            right--;
        }

        while (left < right)
        {
            char temp = characters[left];
            characters[left] = characters[right];
            characters[right] = temp;

            left += 2;
            right -= 2;
        }

        Console.Write("결과 : ");

        for (int i = 0; i < characters.Length; i++)
        {
            Console.Write(characters[i]);
        }

        Console.WriteLine();
    }

    static void PrintNumbersOnly()
    {
        Console.Write("문자열 입력 : ");
        string text = Console.ReadLine() ?? "";

        Console.Write("결과 : ");

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] >= '0' && text[i] <= '9')
            {
                Console.Write(text[i]);
            }
        }

        Console.WriteLine();
    }

    static void CountCharacter()
    {
        Console.Write("문자열 입력 : ");
        string text = Console.ReadLine() ?? "";
        char target;

        while (true)
        {
            Console.Write("찾을 문자 하나 입력 : ");
            string input = Console.ReadLine() ?? "";

            if (input.Length == 1)
            {
                target = input[0];
                break;
            }

            Console.WriteLine("문자를 정확히 하나 입력해주세요.");
        }

        int count = 0;

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == target)
            {
                count++;
            }
        }

        Console.WriteLine($"결과 : {count}개");
    }

    static void RemoveHyphen()
    {
        Console.Write("주민번호 입력 : ");
        string text = Console.ReadLine() ?? "";

        Console.Write("결과 : ");

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] != '-' && text[i] != ' ')
            {
                Console.Write(text[i]);
            }
        }

        Console.WriteLine();
    }
}