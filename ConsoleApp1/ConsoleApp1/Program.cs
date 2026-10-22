using System;

GameData[] games = new GameData[]
{
    new GameData
    {
        Title = "스타듀밸리",
        Rating = AgeRating.All,
        Price = 16000,
        Score = 9.5,
        Notes = (false, false, false)
    },

    new GameData
    {
        Title = "마인크래프트",
        Rating = AgeRating.All,
        Price = 30000,
        Score = 9.2,
        Notes = (false, false, false)
    },

    new GameData
    {
        Title = "그레이브야드 키퍼",
        Rating = AgeRating.Adult,
        Price = 21000,
        Score = 8.5,
        Notes = (true, false, true)
    },

    new GameData
    {
        Title = "팰월드",
        Rating = AgeRating.Teen,
        Price = 32000,
        Score = 9.0,
        Notes = (false, true, true)
    },

    new GameData
    {
        Title = "오버쿡드 2",
        Rating = AgeRating.All,
        Price = 26000,
        Score = 8.8,
        Notes = (true, false, false)
    }
};

while (true)
{
    Console.WriteLine("\n===== 게임 데이터베이스 =====");
    Console.WriteLine("1. 모든 게임 정보 출력");
    Console.WriteLine("2. 특정 조건 게임 출력");
    Console.WriteLine("3. 종료");
    Console.Write("선택 : ");

    string menu = Console.ReadLine() ?? "";

    switch (menu)
    {
        case "1":
            for (int i = 0; i < games.Length; i++)
            {
                PrintGame(games[i]);
            }
            break;

        case "2":
            FilterGames(games);
            break;

        case "3":
            Console.WriteLine("프로그램을 종료합니다.");
            return;

        default:
            Console.WriteLine("1~3 중에서 선택해주세요.");
            break;
    }
}

static void PrintGame(GameData game)
{
    Console.WriteLine("\n------------------------------");
    Console.WriteLine($"타이틀 : {game.Title}");
    Console.WriteLine($"이용 등급 : {game.Rating}");
    Console.WriteLine($"가격 : {game.Price:N0}원");
    Console.WriteLine($"평점 : {game.Score:F1} / 10");
    Console.WriteLine("특이사항 :");

    bool hasNotes = false;

    if (game.Notes.DlcSoon)
    {
        Console.WriteLine("- DLC 곧 출시");
        hasNotes = true;
    }

    if (game.Notes.PoorOptimization)
    {
        Console.WriteLine("- 최적화 좋지 않음");
        hasNotes = true;
    }

    if (game.Notes.ManyBugs)
    {
        Console.WriteLine("- 버그 매우 많음");
        hasNotes = true;
    }

    if (!hasNotes)
    {
        Console.WriteLine("- 없음");
    }
}

static void FilterGames(GameData[] games)
{
    Console.WriteLine("\n===== 검색 조건 =====");
    Console.WriteLine("1. 평점 9.0 이상");
    Console.WriteLine("2. Adult 등급");
    Console.WriteLine("3. 특이사항이 있음");
    Console.Write("선택 : ");

    string condition = Console.ReadLine() ?? "";

    if (condition != "1" && condition != "2" && condition != "3")
    {
        Console.WriteLine("잘못된 조건입니다.");
        return;
    }

    int count = 0;

    for (int i = 0; i < games.Length; i++)
    {
        bool matches = false;

        switch (condition)
        {
            case "1":
                matches = games[i].Score >= 9.0;
                break;

            case "2":
                matches = games[i].Rating == AgeRating.Adult;
                break;

            case "3":
                matches = games[i].Notes.DlcSoon || games[i].Notes.PoorOptimization || games[i].Notes.ManyBugs;
                break;
        }

        if (matches)
        {
            PrintGame(games[i]);
            count++;
        }
    }

    Console.WriteLine($"\n검색 결과 : {count}개");
}

enum AgeRating
{
    All,
    Teen,
    Adult
}

struct GameData
{
    public string Title;
    public AgeRating Rating;
    public int Price;
    public double Score;

    public (bool DlcSoon, bool PoorOptimization, bool ManyBugs) Notes;
}