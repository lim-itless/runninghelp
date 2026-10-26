using System;

class Program
{
    static string[] inventory = new string[5];
    static string[] fieldItems = {"물약", "녹슨 검", "금화", "독사과", "방패"};

    static int fieldIndex = 0;

    static void Main()
    {
        Start();

        while (true)
        {
            Console.Write("\n행동할 키를 누르세요 : ");
            ConsoleKey key = Console.ReadKey(true).Key;
            Console.WriteLine();

            switch (key)
            {
                case ConsoleKey.I:
                    ShowInventory();
                    break;

                case ConsoleKey.G:
                    GetItem();
                    break;

                case ConsoleKey.U:
                    UseItem();
                    break;

                case ConsoleKey.S:
                    DiscardPoisonApples();
                    break;

                case ConsoleKey.Escape:
                    Console.WriteLine("프로그램을 종료합니다.");
                    return;

                default:
                    Console.WriteLine("I, G, U, S 또는 Esc를 눌러주세요.");
                    break;
            }
        }
    }

    static void Start()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i] = "비어있음";
        }

        Console.WriteLine("===== 인벤토리 관리 시스템 =====");
        Console.WriteLine("I : 인벤토리 확인");
        Console.WriteLine("G : 필드 아이템 하나 획득");
        Console.WriteLine("U : 1번 슬롯 아이템 사용");
        Console.WriteLine("S : 독사과 모두 버리기");
        Console.WriteLine("ESC : 종료");
    }

    static void ShowInventory()
    {
        Console.WriteLine("\n===== 인벤토리 =====");

        for (int i = 0; i < inventory.Length; i++)
        {
            Console.WriteLine($"[{i + 1}번 슬롯] : {inventory[i]}");
        }
    }

    static void GetItem()
    {
        bool acquired = false;
        bool hasEmptySlot = false;

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == "비어있음")
            {
                hasEmptySlot = true;

                if (fieldIndex < fieldItems.Length)
                {
                    inventory[i] = fieldItems[fieldIndex];

                    Console.WriteLine($"{inventory[i]}(을)를 획득했습니다. [{i + 1}번 슬롯]");

                    fieldIndex++;
                    acquired = true;
                }

                break;
            }
        }

        if (!hasEmptySlot)
        {
            Console.WriteLine("가방이 가득 찼습니다!");
        }
        else if (!acquired)
        {
            Console.WriteLine("필드에 남아 있는 아이템이 없습니다.");
        }
    }

    static void UseItem()
    {
        if (inventory[0] == "비어있음")
        {
            Console.WriteLine("1번 슬롯이 비어있습니다.");
            return;
        }

        Console.WriteLine(inventory[0] + "(을)를 사용했습니다!");

        inventory[0] = "비어있음";

        for (int i = 0; i < inventory.Length - 1; i++)
        {
            inventory[i] = inventory[i + 1];
        }

        inventory[inventory.Length - 1] = "비어있음";
    }

    static void DiscardPoisonApples()
    {
        int count = 0;

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == "독사과")
            {
                inventory[i] = "비어있음";
                count++;

                Console.WriteLine($"[{i + 1}번 슬롯] 의 독사과를 버렸습니다!");
            }
        }

        if (count == 0)
        {
            Console.WriteLine("가방에 독사과가 없습니다.");
        }
    }
}