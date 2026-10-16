using System;

class Program
{
    static void Main()
    {
        Random random = new Random();

        int playerHp = 100;
        int monsterHp = 200;
        int turn = 1;

        bool isInvincible = false;
        bool isCharging = false;

        string[] playerSkills = { "일반 공격", "베기", "찌르기", "내려찍기" };

        int[] playerDamage = { 10, 50, 30, 70 };

        string[] monsterSkills = { "일반 공격", "기 모으기" };

        Console.WriteLine("===== 턴제 미니 RPG =====");
        Console.WriteLine("[치트] god - 무적 전환 / kill - 몬스터 즉사");

        while (playerHp > 0 && monsterHp > 0)
        {
            Console.WriteLine($"\n===== {turn}턴 =====");
            Console.WriteLine($"플레이어 HP : {playerHp} / 100");
            Console.WriteLine($"몬스터 HP : {monsterHp} / 200");

            if (isInvincible)
            {
                Console.WriteLine("[무적 상태]");
            }

            if (isCharging)
            {
                Console.WriteLine("[경고] 몬스터의 다음 공격이 강화됩니다.");
            }

            for (int i = 0; i < playerSkills.Length; i++)
            {
                Console.WriteLine($"{i}: {playerSkills[i]} (데미지 {playerDamage[i]})");
            }

            Console.Write("스킬 번호 or 치트 입력 : ");
            string input = (Console.ReadLine() ?? "").Trim().ToLower();

            if (input == "god")
            {
                isInvincible = !isInvincible;

                Console.WriteLine(isInvincible ? "무적 ON" : "무적 OFF");
                continue;
            }

            if (input == "kill")
            {
                monsterHp = 0;
                Console.WriteLine("[치트] 몬스터를 즉사시켰습니다.");
                break;
            }

            if (!int.TryParse(input, out int skill))
            {
                Console.WriteLine("스킬 번호 or 치트를 입력해주세요.");
                continue;
            }

            if (skill < 0 || skill >= playerSkills.Length)
            {
                Console.WriteLine("스킬 번호는 0~3 입니다.");
                continue;
            }

            int damage = playerDamage[skill];
            monsterHp -= damage;

            if (monsterHp < 0)
            {
                monsterHp = 0;
            }

            Console.WriteLine($"\n플레이어의 {playerSkills[skill]}! {damage} 데미지!");
            Console.WriteLine($"몬스터 남은 HP : {monsterHp}");

            if (monsterHp == 0)
            {
                break;
            }

            int monsterSkill;

            if (isCharging)
            {
                monsterSkill = 0;
            }
            else
            {
                monsterSkill = random.Next(0, monsterSkills.Length);
            }

            switch (monsterSkill)
            {
                case 0:
                    {
                        int monsterDamage = 10;

                        if (isCharging)
                        {
                            monsterDamage = monsterDamage * 3 / 2;
                            isCharging = false;

                            Console.WriteLine("공격을 강화합니다.");
                        }

                        bool isCritical = random.Next(0, 100) < 25;

                        if (isCritical)
                        {
                            monsterDamage *= 2;
                            Console.WriteLine("치명타 발생!");
                        }

                        if (isInvincible)
                        {
                            monsterDamage = 0;
                            Console.WriteLine("[무적] 피해를 받지 않습니다.");
                        }

                        playerHp -= monsterDamage;

                        if (playerHp < 0)
                        {
                            playerHp = 0;
                        }

                        Console.WriteLine($"몬스터의 {monsterSkills[0]}! {monsterDamage} 데미지!");
                        Console.WriteLine($"플레이어 남은 HP : {playerHp}");
                        break;
                    }

                case 1:
                    {
                        isCharging = true;

                        Console.WriteLine($"몬스터의 {monsterSkills[1]}! 이번 턴에는 공격하지 않습니다.");
                        break;
                    }
            }

            turn++;
        }

        Console.WriteLine("\n===== 전투 종료 =====");

        if (playerHp == 0)
        {
            Console.WriteLine("플레이어가 사망했습니다. 게임 오버...");
        }
        else
        {
            Console.WriteLine("몬스터를 처치했습니다! 승리!");
        }
    }
}