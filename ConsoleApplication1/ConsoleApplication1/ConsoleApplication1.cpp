#include <iostream>

void PrintItem(const std::string& name, const std::string& type, int damage, int price, const std::string& effect)
{
    std::cout << "------------------------------\n";
    std::cout << "이름   : " << name << '\n';
    std::cout << "종류   : " << type << '\n';
    std::cout << "데미지 : " << damage << '\n';
    std::cout << "가격   : " << price << " 골드\n";
    std::cout << "특성   : " << effect << '\n';
}

void CreateItems()
{
    std::cout << "\n===== 아이템 목록 =====\n";

    std::string swordName = "검";
    int swordDamage = 15;
    int swordPrice = 100;

    std::string bowName = "활";
    int bowDamage = 12;
    int bowPrice = 120;

    std::string armorName = "갑옷";
    int armorDamage = 0;
    int armorPrice = 200;

    std::string shieldName = "방패";
    int shieldDamage = 3;
    int shieldPrice = 80;

    std::string ringName = "반지";
    int ringDamage = 5;
    int ringPrice = 150;

    std::string necklaceName = "목걸이";
    int necklaceDamage = 8;
    int necklacePrice = 180;

    PrintItem(swordName, "무기", swordDamage, swordPrice, "근접 공격");
    PrintItem(bowName, "무기", bowDamage, bowPrice, "원거리 공격");
    PrintItem(armorName, "방어구", armorDamage, armorPrice, "방어력 +20");
    PrintItem(shieldName, "방어구", shieldDamage, shieldPrice, "방어력 +10, 패링");
    PrintItem(ringName, "악세서리", ringDamage, ringPrice, "추가 데미지 +5");
    PrintItem(necklaceName, "악세서리", necklaceDamage, necklacePrice, "추가 데미지 +8");
    std::cout << "------------------------------\n";
}

int RandomNumber(int min, int max)
{
    return rand() % (max - min + 1) + min;
}

void CreateRandomNumbers()
{
    int number1 = RandomNumber(1, 5);
    int number2 = RandomNumber(6, 20);
    int number3 = RandomNumber(150, 200);

    std::cout << "\n===== 랜덤 숫자 =====\n";
    std::cout << "1~5     : " << number1 << '\n';
    std::cout << "6~20    : " << number2 << '\n';
    std::cout << "150~200 : " << number3 << '\n';
}

void ReceiveBounty()
{
    int currentGold = 0;

    std::cout << "\n===== 현상금 받기 =====\n";

    while (true)
    {
        std::cout << "소지금을 입력하세요 (100~200) : ";

        if (std::cin >> currentGold)
        {
            if (currentGold >= 100 && currentGold <= 200)
            {
                break;
            }
        }
        else
        {
            std::cin.clear();
        }

        std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
        std::cout << "100~200 사이의 정수를 입력해주세요.\n";
    }

    int bountyGold = RandomNumber(50, 100);
    int totalGold = currentGold + bountyGold;

    int tax = totalGold / 10;
    int finalGold = totalGold - tax;

    int netBounty = finalGold - currentGold;

    std::cout << "기존 소지금       : " << currentGold << " 골드\n";
    std::cout << "현상금            : " << bountyGold << " 골드\n";
    std::cout << "소지금 + 현상금   : " << totalGold << " 골드\n";
    std::cout << "세금 (합계의 10%) : " << tax << " 골드\n";
    std::cout << "실제로 늘어난 골드: " << netBounty << " 골드\n";
    std::cout << "최종 소지금       : " << finalGold << " 골드\n";
}

void CreateStats()
{
    int strength = RandomNumber(1, 10);
    int dexterity = RandomNumber(1, 10);
    int intelligence = RandomNumber(1, 10);

    int totalStats = strength + dexterity + intelligence;

    std::cout << "\n===== 캐릭터 능력치 =====\n";
    std::cout << "STR     : " << strength << '\n';
    std::cout << "DEX     : " << dexterity << '\n';
    std::cout << "INT     : " << intelligence << '\n';
    std::cout << "총 스탯 : " << totalStats << '\n';
}

int main()
{
    srand(static_cast<unsigned int>(time(nullptr)));

    CreateItems();
    CreateRandomNumbers();
    ReceiveBounty();
    CreateStats();

    return 0;
}