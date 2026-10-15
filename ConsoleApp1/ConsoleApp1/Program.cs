using System;

class Program
{
    static void Main()
    {
        Random random = new Random();

        while (true)
        {
            int[] answer = new int[3];

            for (int i = 0; i < answer.Length; i++)
            {
                answer[i] = random.Next(0, 10);

                for (int j = 0; j < i; j++)
                {
                    if (answer[i] == answer[j])
                    {
                        i--;
                        break;
                    }
                }
            }

            int attempts = 0;

            Console.WriteLine("\n===== 숫자 야구 =====");
            Console.WriteLine("중복 없는 숫자 3개를 공백으로 구분해 입력하세요.");
            Console.WriteLine("입력 예시 : 3 6 9");

            while (true)
            {
                Console.Write("\n숫자 3개 입력 (치트 - cheat 입력) : ");
                string input = Console.ReadLine() ?? "";

                if (input.Trim().ToLower() == "cheat")
                {
                    Console.WriteLine($"[치트] 컴퓨터 숫자 : {answer[0]} {answer[1]} {answer[2]}");
                    continue;
                }

                string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 3)
                {
                    Console.WriteLine("숫자 3개를 공백으로 구분해 입력해주세요.");
                    continue;
                }

                int[] num = new int[3];
                bool validInput = true;

                for (int i = 0; i < num.Length; i++)
                {
                    if (!int.TryParse(parts[i], out num[i]))
                    {
                        validInput = false;
                        break;
                    }

                    if (num[i] < 0 || num[i] > 9)
                    {
                        validInput = false;
                        break;
                    }
                }

                if (!validInput)
                {
                    Console.WriteLine("각 숫자는 0~9 사이의 정수여야 합니다.");
                    continue;
                }

                if (num[0] == num[1] || num[0] == num[2] || num[1] == num[2])
                {
                    Console.WriteLine("중복 없이 입력해주세요.");
                    continue;
                }

                attempts++;

                int strike = 0;
                int ball = 0;
                int outCount = 0;

                for (int i = 0; i < num.Length; i++)
                {
                    bool found = false;

                    for (int j = 0; j < answer.Length; j++)
                    {
                        if (num[i] == answer[j])
                        {
                            found = true;

                            if (i == j)
                            {
                                strike++;
                            }
                            else
                            {
                                ball++;
                            }

                            break;
                        }
                    }

                    if (!found)
                    {
                        outCount++;
                    }
                }

                Console.WriteLine($"{strike} S / {ball} B / {outCount} O");

                if (strike == 3)
                {
                    Console.WriteLine($"정답입니다! {attempts} 번 만에 맞혔습니다.");
                    break;
                }
            }

            bool restart;

            while (true)
            {
                Console.Write("\n다시 하시겠습니까? (1 - 재시작 / 0 - 종료) : ");
                string choice = Console.ReadLine() ?? "";

                if (choice == "1")
                {
                    restart = true;
                    break;
                }

                if (choice == "0")
                {
                    restart = false;
                    break;
                }

                Console.WriteLine("1 또는 0 을 입력해주세요.");
            }

            if (!restart)
            {
                Console.WriteLine("게임을 종료합니다.");
                break;
            }
        }
    }
}