int[][][] schedule = BuildTournamentSchedule(6);
Print(schedule);

static int[][][] BuildTournamentSchedule(int numberOfTeams)
{
    int[] teams = Enumerable.Range(1, numberOfTeams).ToArray();

    int numberOfMatchesPerRound = numberOfTeams / 2;
    int numberOfRounds = numberOfTeams - 1;
    int[][][] schedule = new int[numberOfRounds][][];

    for (int round = 0; round < schedule.Length; round++)
    {
        schedule[round] = new int[numberOfMatchesPerRound][];

        for (int match = 0; match < schedule[round].Length; match++)
        {
            schedule[round][match] = new int[] { teams[match], teams[numberOfTeams - 1 - match] };
        }

        RotateTeamsClockwise(teams);
    }

    return schedule;
}

static void RotateTeamsClockwise(int[] teams)
{
    int lastTeam = teams[^1];

    for (int i = teams.Length - 1; 1 < i; i--)
    {
        teams[i] = teams[i - 1];
    }

    teams[1] = lastTeam;
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
