#include <iostream>

void MiningGold(int& gold)
{
    int earnedGold = 10;

    if (gold >= 30)
    {
        earnedGold = 20;
    }

    gold += earnedGold;

    std::cout << "획득 : " << earnedGold << " / 보유 골드 : " << gold << '\n';
}

void SwapValue(int a, int b)
{
    int temp = a;
    a = b;
    b = temp;
}

void SwapReference(int& a, int& b)
{
    int temp = a;
    a = b;
    b = temp;
}

bool TryContext(int gold, int cost, int& remainingGold)
{
    remainingGold = gold;

    if (cost < 0 || gold < cost)
    {
        return false;
    }

    remainingGold = gold - cost;
    return true;
}

void Start()
{
    int currentGold = 0;

    std::cout << "=== 채굴 ===\n";

    for (int i = 0; i < 5; ++i)
    {
        MiningGold(currentGold);
    }

    std::cout << "\n=== Swap ===\n";

    int a = 10;
    int b = 20;

    SwapValue(a, b);
    std::cout << "값 전달 후 : a = " << a << ", b = " << b << '\n';

    SwapReference(a, b);
    std::cout << "참조 전달 후 : a = " << a << ", b = " << b << '\n';

    std::cout << "\n=== 구매 시도 ===\n";

    int remainingGold = 0;
    int itemPrice = 50;

    if (TryContext(currentGold, itemPrice, remainingGold))
    {
        currentGold = remainingGold;
        std::cout << "구매 성공! 남은 골드 : " << currentGold << '\n';
    }
    else
    {
        std::cout << "구매 실패...\n";
    }

    if (TryContext(currentGold, itemPrice, remainingGold))
    {
        currentGold = remainingGold;
        std::cout << "구매 성공! 남은 골드 : " << currentGold << '\n';
    }
    else
    {
        std::cout << "구매 실패...\n";
    }
}


int main()
{
    Start();
    return 0;
}