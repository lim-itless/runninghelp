using System;

class Program
{
    static void Main()
    {
        Random random = new Random();

        Console.WriteLine("===== 주민등록번호 형식 문자열 생성기 =====");

        int year = InputYear();
        int month = ReadNumber("월 입력 : ", 1, 12);
        int maxDay = DateTime.DaysInMonth(year, month);
        int day = ReadNumber($"일 입력 (1 ~ {maxDay}) : ", 1, maxDay);
        bool isMale = InputGender(random);

        Console.WriteLine($"\n생성 결과 : {CreateNumber(year, month, day, isMale, random)}");

        int regenerateCount = 0;

        while (regenerateCount < 3)
        {
            Console.Write($"\n자동으로 다시 생성할까요? (Y / N, 남은 횟수 : {3 - regenerateCount}) : ");

            string choice = (Console.ReadLine() ?? "").Trim().ToUpper();

            if (choice == "N") break;

            if (choice != "Y")
            {
                Console.WriteLine("Y 또는 N 을 입력해주세요.");
                continue;
            }

            year = random.Next(1900, 2100);
            month = random.Next(1, 13);
            maxDay = DateTime.DaysInMonth(year, month);
            day = random.Next(1, maxDay + 1);
            isMale = random.Next(0, 2) == 0;

            regenerateCount++;

            Console.WriteLine($"\n자동 생성 {regenerateCount} / 3 : " +CreateNumber(year, month, day, isMale, random));
        }

        if (regenerateCount == 3)
        {
            Console.WriteLine("\n자동 재생성 3회를 모두 사용했습니다.");
        }

        Console.WriteLine("프로그램을 종료합니다.");
    }

    static int InputYear()
    {
        while (true)
        {
            Console.Write("\n생년 입력 (2자리 또는 4자리) : ");
            string input = (Console.ReadLine() ?? "").Trim();

            if (input.Length != 2 && input.Length != 4)
            {
                Console.WriteLine("예 : 05 또는 2005 처럼 입력해주세요.");
                continue;
            }

            bool digitsOnly = true;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] < '0' || input[i] > '9')
                {
                    digitsOnly = false;
                    break;
                }
            }

            if (!digitsOnly)
            {
                Console.WriteLine("숫자만 입력해주세요.");
                continue;
            }

            int year = int.Parse(input);

            if (input.Length == 4)
            {
                if (year < 1900 || year > 2099)
                {
                    Console.WriteLine("1900 ~ 2099년 사이로 입력해주세요.");
                    continue;
                }

                return year;
            }

            int defaultYear;

            if (year <= 24)
            {
                defaultYear = 2000 + year;
            }
            else
            {
                defaultYear = 1900 + year;
            }

            while (true)
            {
                Console.WriteLine($"1. {1900 + year}년");
                Console.WriteLine($"2. {2000 + year}년");
                Console.Write($"선택 (엔터 : 자동으로 {defaultYear}년) : ");

                string choice = (Console.ReadLine() ?? "").Trim();

                switch (choice)
                {
                    case "1":
                        return 1900 + year;

                    case "2":
                        return 2000 + year;

                    case "":
                        return defaultYear;

                    default:
                        Console.WriteLine("1, 2 또는 엔터를 입력해주세요.");
                        break;
                }
            }
        }
    }

    static int ReadNumber(string message, int min, int max)
    {
        while (true)
        {
            Console.Write(message);
            string input = (Console.ReadLine() ?? "").Trim();

            if (int.TryParse(input, out int number))
            {
                if (number >= min && number <= max)
                {
                    return number;
                }
            }

            Console.WriteLine($"{min} ~ {max} 사이의 정수를 입력해주세요.");
        }
    }

    static bool InputGender(Random random)
    {
        while (true)
        {
            Console.Write("성별 입력 (남자 : 1 또는 3 / 여자 : 2 또는 4 / 그 외 정수 : 무작위) : ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("정수로 입력해주세요.");
                continue;
            }

            switch (choice)
            {
                case 1:
                case 3:
                    return true;

                case 2:
                case 4:
                    return false;

                default:
                    bool isMale = random.Next(0, 2) == 0;
                    Console.WriteLine($"무작위 성별 : {(isMale ? "남자" : "여자")}");
                    return isMale;
            }
        }
    }

    static string CreateNumber(int year, int month, int day, bool isMale, Random random)
    {
        int genderCode;

        if (year < 2000)
        {
            genderCode = isMale ? 1 : 2;
        }
        else
        {
            genderCode = isMale ? 3 : 4;
        }

        string lastSix = "";
        int previousNumber = genderCode;

        for (int i = 0; i < 6; i++)
        {
            int number;

            do
            {
                number = random.Next(1, 10);
            }
            while (number == previousNumber);

            lastSix += number;
            previousNumber = number;
        }

        return $"{year % 100:D2}{month:D2}{day:D2} - {genderCode}{lastSix}";
    }
}