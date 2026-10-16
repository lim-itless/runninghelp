using System;
using System.Text;

class Program
{
    // 약 30분
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Random random = new Random();

        int money = 10000;

        string[] shapes = { "♠", "♦", "♣", "♥" };
        int[] cards = new int[52];

        for (int i = 0; i < cards.Length; i++)
        {
            cards[i] = i;
        }

        for (int i = cards.Length - 1; i > 0; i--)
        {
            int randomIndex = random.Next(i + 1);

            int temp = cards[i];
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }

        int firstCard = cards[0];
        int secondCard = cards[1];
        int thirdCard = cards[2];

        int firstShape = firstCard / 13;
        int firstNumber = firstCard % 13 + 1;

        int secondShape = secondCard / 13;
        int secondNumber = secondCard % 13 + 1;

        int thirdShape = thirdCard / 13;
        int thirdNumber = thirdCard % 13 + 1;

        Console.WriteLine("===== 월남뽕 =====");
        Console.WriteLine($"소지금 : {money}원");
        Console.WriteLine($"{shapes[firstShape]} {firstNumber} / ? / {shapes[thirdShape]} {thirdNumber}");
        Console.WriteLine($"[치트] 숨겨진 카드 : {shapes[secondShape]} {secondNumber}");
        Console.Write("배팅 금액 입력 (1000원~ ) : ");

        if (!int.TryParse(Console.ReadLine(), out int bet))
        {
            Console.WriteLine("숫자를 입력해주세요.");
            return;
        }

        if (bet < 1000 || bet > money)
        {
            Console.WriteLine("배팅할 수 없는 금액입니다.");
            return;
        }

        Console.WriteLine($"배팅 금액 : {bet}원");
    }
}