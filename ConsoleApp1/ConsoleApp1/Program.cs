using System;
using System.Text;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Random random = new Random();

        int[] deck = new int[52];

        for (int i = 0; i < deck.Length; i++)
        {
            deck[i] = i;
        }

        for (int i = deck.Length - 1; i > 0; i--)
        {
            int randomIndex = random.Next(i + 1);

            int temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }

        long money = 10000;
        int position = 0;
        int round = 0;

        Console.WriteLine("===== 월남뽕 =====");
        Console.WriteLine("A = 1 / J = 11 / Q = 12 / K = 13");
        Console.WriteLine("승리 : 배팅액 × 2 / 패배 : 배팅액 차감");
        Console.WriteLine("fold : 해당 판 포기");
        Console.WriteLine("next : 다음 판 첫 카드 확인");
        Console.WriteLine("list : 아직 사용하지 않은 카드 목록");

        while (money >= 1000 && deck.Length - position >= 3)
        {
            round++;

            int leftCard = deck[position];
            int hiddenCard = deck[position + 1];
            int rightCard = deck[position + 2];

            position += 3;

            int leftNumber = GetNumber(leftCard);
            int rightNumber = GetNumber(rightCard);
            int hiddenNumber = GetNumber(hiddenCard);

            int minNumber = Math.Min(leftNumber, rightNumber);
            int maxNumber = Math.Max(leftNumber, rightNumber);

            Console.WriteLine($"\n===== {round} / 17판 =====");
            Console.WriteLine($"소지금 : {money:N0}원");
            Console.WriteLine($"{GetCardName(leftCard)} / ? / {GetCardName(rightCard)}");
            Console.WriteLine($"[치트] 컴퓨터의 숨겨진 패 : {GetCardName(hiddenCard)}");

            if (leftNumber == rightNumber)
            {
                money -= 1000;

                Console.WriteLine("양쪽 숫자가 같습니다. 자동 패배!");
                Console.WriteLine("1,000원 차감");
                Console.WriteLine($"남은 소지금 : {money:N0}원");

                continue;
            }

            long bet = 0;
            bool folded = false;

            while (true)
            {
                Console.Write($"\n배팅액 (1,000 ~ {money:N0}원) 또는 fold / next / list : ");

                string input = (Console.ReadLine() ?? "").Trim().ToLower();

                if (input == "next")
                {
                    if (deck.Length - position >= 3)
                    {
                        Console.WriteLine($"[치트] 다음 판 첫 카드 : " +GetCardName(deck[position]));
                    }
                    else
                    {
                        Console.WriteLine("다음 판은 없습니다. 남은 1장은 버려집니다.");
                    }

                    continue;
                }

                if (input == "list")
                {
                    ShowRemainingCards(deck, position);
                    continue;
                }

                if (input == "fold")
                {
                    folded = true;
                    break;
                }

                if (!long.TryParse(input, out bet))
                {
                    Console.WriteLine("금액 또는 명령어를 입력해주세요.");
                    continue;
                }

                if (bet < 1000 || bet > money)
                {
                    Console.WriteLine("1,000원 이상, 소지금 이하로 배팅해주세요.");
                    continue;
                }

                break;
            }

            if (folded)
            {
                Console.WriteLine("[fold] 돈 차감 없이 이번 판을 종료합니다.");
                Console.WriteLine("이번 판 카드 3장은 버립니다.");
                continue;
            }

            Console.WriteLine("\n[카드 공개]");
            Console.WriteLine($"{GetCardName(leftCard)} / {GetCardName(hiddenCard)} / {GetCardName(rightCard)}");

            if (hiddenNumber > minNumber && hiddenNumber < maxNumber)
            {
                long reward = bet * 2;
                money += reward;

                Console.WriteLine($"승리! {reward:N0}원 획득!");
            }
            else
            {
                money -= bet;

                Console.WriteLine($"패배.. {bet:N0}원 차감!");
            }

            Console.WriteLine($"남은 소지금 : {money:N0}원");
        }

        Console.WriteLine("\n===== 게임 종료 =====");

        if (money == 0)
        {
            Console.WriteLine("소지금을 전부 잃었습니다.");
        }
        else if (deck.Length - position < 3)
        {
            Console.WriteLine("17판을 모두 진행했습니다.");
            Console.WriteLine("마지막 남은 카드 1장은 버립니다.");
        }
        else
        {
            Console.WriteLine("최소 배팅 금액인 1,000원이 부족합니다.");
        }

        Console.WriteLine($"진행한 판 수 : {round}");
        Console.WriteLine($"최종 소지금 : {money:N0}원");
    }

    static int GetNumber(int card)
    {
        return card % 13 + 1;
    }

    static string GetCardName(int card)
    {
        string[] suits = { "♠", "♦", "♣", "♥" };

        int suitIndex = card / 13;
        int number = GetNumber(card);

        string rank;

        switch (number)
        {
            case 1:
                rank = "A";
                break;

            case 11:
                rank = "J";
                break;

            case 12:
                rank = "Q";
                break;

            case 13:
                rank = "K";
                break;

            default:
                rank = number.ToString();
                break;
        }

        return $"{suits[suitIndex]} {rank}";
    }

    static void ShowRemainingCards(int[] deck, int position)
    {
        Console.WriteLine($"[치트] 남은 카드 : {deck.Length - position}장");
        Console.WriteLine("(덱에 놓인 순서대로 표시)");

        for (int i = position; i < deck.Length; i++)
        {
            Console.Write($"[{GetCardName(deck[i])}] ");

            if ((i - position + 1) % 5 == 0)
            {
                Console.WriteLine();
            }
        }

        Console.WriteLine();
    }
}