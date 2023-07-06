using System.Diagnostics;

Stopwatch sw = Stopwatch.StartNew();
int numberOfTeams = 4;
int[][][] schedule = BuildTournamentSchedule(numberOfTeams);
sw.Stop();
Console.WriteLine($"Build schedule of {numberOfTeams} teams in {sw.ElapsedMilliseconds}ms. Press any key to print the schedule...");
Console.ReadKey();
Console.WriteLine();
Print(schedule);

static int[][][] BuildTournamentSchedule(int numberOfTeams)
{
    int numberOfMatchesPerRound = numberOfTeams / 2;
    int numberOfRounds = numberOfTeams - 1;
    int[][][] schedule = InitializeSchedule(numberOfRounds, numberOfMatchesPerRound);

    if (numberOfTeams < 5000)
    {
        for (int round = 0; round < numberOfRounds; round++)
        {
            for (int match = 0; match < numberOfMatchesPerRound; match++)
            {
                schedule[round][match] = GetTeamNumbers(numberOfTeams, round, match);
            }
        }
    }
    else
    {
        _ = Parallel.For(0, numberOfRounds, round =>
        {
            for (int match = 0; match < numberOfMatchesPerRound; match++)
            {
                schedule[round][match] = GetTeamNumbers(numberOfTeams, round, match);
            }
        });
    }

    return schedule;
}

static int[][][] InitializeSchedule(int numberOfRounds, int numberOfMatchesPerRound)
{
    int[][][] schedule = new int[numberOfRounds][][];

    for (int round = 0; round < numberOfRounds; round++)
    {
        schedule[round] = new int[numberOfMatchesPerRound][];
    }

    return schedule;
}

static int[] GetTeamNumbers(int numberOfTeams, int round, int match)
{
    // These are the formulas that simulates clockwise rotation of teams array.
    // I made it this way to get rid of teams array and clockwise rotation method. So I could parallelize.
    int team1 = (round == 0 || match == 0 ? match : ((numberOfTeams - round - 2 + match) % (numberOfTeams - 1)) + 1) + 1;

    int mod = (numberOfTeams - round - 2 - match) % (numberOfTeams - 1);
    int team2 = (mod < 0 ? mod + numberOfTeams - 1 : mod) + 2;

    return new[] { team1, team2 };
}

static void Print(int[][][] schedule)
{
    for (int round = 0; round < schedule.Length; round++)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Round {round + 1}:");
        Console.ForegroundColor = ConsoleColor.White;

        for (int match = 0; match < schedule[round].Length; match++)
        {
            int team1 = schedule[round][match][0];
            int team2 = schedule[round][match][1];

            Console.WriteLine($"Match {match + 1}: Team {team1} vs Team {team2}");
        }

        Console.WriteLine();
    }
}