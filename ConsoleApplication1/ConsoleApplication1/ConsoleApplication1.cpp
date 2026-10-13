#include <iostream>

void PracticeWhile()
{
    int number = 0;

    std::cout << "정수를 입력하세요. (-10~10) : ";

    if (!(std::cin >> number))
    {
        return;
    }

    if (number < -10 || number > 10)
    {
        return;
    }

    while (number >= -15)
    {
        std::cout << number << '\n';
        number -= 5;
    }
}

void PracticeDoWhile()
{
    int first = 0;
    int second = 0;

    std::cout << "두 정수를 입력하세요: ";

    if (!(std::cin >> first >> second))
    {
        return;
    }

    int minNum = first;
    int maxNum = second;

    if (minNum > maxNum)
    {
        int temp = minNum;
        minNum = maxNum;
        maxNum = temp;
    }

    long long current = minNum;
    long long count = 0;
    long long sum = 0;

    do
    {
        if (current % 3 == 0)
        {
            if (count > 0)
            {
                std::cout << ", ";
            }

            std::cout << current;
            ++count;
            sum += current;
        }

        ++current;
    }
    while (current <= maxNum);

    if (count == 0)
    {
        std::cout << "범위 안에 3의 배수 없음.";
    }

    std::cout << "\n총 개수 : " << count << '\n';
    std::cout << "합 : " << sum << '\n';
}

void PracticeFor()
{
    int dan = 0;

    std::cout << "단을 입력하세요.(2~9) : ";

    if (!(std::cin >> dan))
    {
        return;
    }

    if (dan < 2 || dan > 9)
    {
        return;
    }

    int sum = 0;

    for (int i = 1; i <= 9; ++i)
    {
        int result = dan * i;
        sum += result;

        std::cout << dan << " x " << i << " = " << result << '\n';
    }

    std::cout << "곱셈의 합 : " << sum << '\n';
}

int main()
{
    PracticeWhile();
    PracticeDoWhile();
    PracticeFor();

    return 0;
}