#include <iostream>

int main()
{
    int first = 0;
    int second = 0;

    std::cout << "정수 두 개를 입력하세요 : ";
    std::cin >> first >> second;

    std::cout << "덧셈 : " << first + second << '\n';
    std::cout << "뺄셈 : " << first - second << '\n';
    std::cout << "곱셈 : " << first * second << '\n';

    if (second != 0)
    {
        std::cout << "나눗셈 : " << first / second << '\n';
        std::cout << "나머지 : " << first % second << '\n';
    }
    else
    {
        std::cout << "0 으로 나누거나 나머지를 구할 수 없음.\n";
    }

    int num1 = 0;
    int num2 = 0;
    int num3 = 0;

    std::cout << "정수 세 개를 입력하세요 : ";
    std::cin >> num1 >> num2 >> num3;

    if (num1 != 0)
    {
        int result1 = (num1 + num2) * (num3 + num1) % num1;

        std::cout << "(num1 + num2) * (num3 + num1) % num1 = " << result1 << '\n';
    }
    else
    {
        std::cout << "첫 번째 수식 : num1 = 0 은 계산할 수 없음.\n";
    }

    if (num2 != 0)
    {
        int result2 = (num3 % num2) + (num1 * 2);

        std::cout << "(num3 % num2) + (num1 * 2) = " << result2 << '\n';
    }
    else
    {
        std::cout << "두 번째 수식 : num2 = 0 은 계산할 수 없음.\n";
    }

    if (num3 != -1)
    {
        int result3 = (num1 + num2 + num3) % (num3 + 1);

        std::cout << "(num1 + num2 + num3) % (num3 + 1) = " << result3 << '\n';
    }
    else
    {
        std::cout << "세 번째 수식 : num3 + 1 = 0 은 계산할 수 없음.\n";
    }

    int a = 0;
    int b = 0;

    std::cout << "a와 b를 입력하세요 (a >= 0, b > 0) : ";
    std::cin >> a >> b;

    if (a >= 0 && b > 0)
    {
        int q = a / b;
        int r = a % b;
        int restoredA = b * q + r;

        std::cout << "몫 : " << q << ", 나머지 : " << r << '\n';
        std::cout << "원래 숫자 a : " << a << '\n';
        std::cout << "계산된 값 : " << restoredA << '\n';
    }
    else
    {
        std::cout << "a는 0 이상, b는 양수로 입력해주세요.\n";
    }

    return 0;
}