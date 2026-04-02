using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;

namespace AshiisArcadeConsole
{
    internal class Program
    {
        // Option A: Direct IP and Port (Most reliable since 1433 is set in IPAll)
        static string connString = @"Server=192.168.0.52,1433;Database=ArcadeBlockade;User Id=sa;Password=Password1;TrustServerCertificate=True;"; 
        static void Main(string[] args)
        {
            MainMenu();
        }
        // menus
        static void MainMenu()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("                 ___         __    _ _ _      \r\n                /   |  _____/ /_  (_|_| )_____\r\n               / /| | / ___/ __ \\/ / /|// ___/\r\n              / ___ |(__  ) / / / / /  (__  ) \r\n           __/_/  |_/____/_/ /_/_/_/  /____/  \r\n          /   |  ______________ _____/ /__    \r\n         / /| | / ___/ ___/ __ `/ __  / _ \\   \r\n        / ___ |/ /  / /__/ /_/ / /_/ /  __/   \r\n       /_/  |_/_/   \\___/\\__,_/\\__,_/\\___/    \r\n                                              ");
                Console.WriteLine(" ");
                Console.WriteLine($"                  --- MAIN MENU ---");
                Console.WriteLine("======================================================");
                Console.WriteLine($"SYSTEM TIME: {DateTime.Now:dd/MM/yyyy HH:mm} | STATUS: OPERATIONAL");
                Console.WriteLine("======================================================");
                Console.WriteLine(" [1] All entries           [4] Games counter");
                Console.WriteLine(" [2] Search for game       [5] oldest + newest release");
                Console.WriteLine(" [3] Search for console    [6] timespan search");
                Console.WriteLine(" ");
                Console.WriteLine(" [9] Creation Menu         [0] Exit App");
                Console.WriteLine("======================================================");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AllEntries();
                        break;
                    case "2":
                        SearchForGame();
                        break;
                    case "3":
                        SearchForConsole();
                        break;
                    case "4":
                        GamesCounter();
                        break;
                    case "5":
                        OldestNewest();
                        break;
                    case "6":
                        PeriodSearch();
                        break;
                    case "9": // goes to Creation Menu
                        CreationMenu();
                        break;
                    case "0": // exits app
                        running = false;
                        Console.WriteLine("Shutting down...");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again..");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void CreationMenu()
        {
            bool running = true;

            while (running)
            {

                Console.Clear();
                Console.WriteLine("                 ___         __    _ _ _      \r\n                /   |  _____/ /_  (_|_| )_____\r\n               / /| | / ___/ __ \\/ / /|// ___/\r\n              / ___ |(__  ) / / / / /  (__  ) \r\n           __/_/  |_/____/_/ /_/_/_/  /____/  \r\n          /   |  ______________ _____/ /__    \r\n         / /| | / ___/ ___/ __ `/ __  / _ \\   \r\n        / ___ |/ /  / /__/ /_/ / /_/ /  __/   \r\n       /_/  |_/_/   \\___/\\__,_/\\__,_/\\___/    \r\n                                              ");
                Console.WriteLine(" ");
                Console.WriteLine($"                --- CREATION MENU ---");
                Console.WriteLine("======================================================");
                Console.WriteLine($"SYSTEM TIME: {DateTime.Now:dd/MM/yyyy HH:mm} | STATUS: OPERATIONAL");
                Console.WriteLine("======================================================");
                Console.WriteLine(" [1] Add game              [4] Delete Games");
                Console.WriteLine(" [2] Add console           [5] Delete Consoles");
                Console.WriteLine(" [3] Console IDs");
                Console.WriteLine(" ");
                Console.WriteLine(" [9] Return to previous    [0] Exit App");
                Console.WriteLine("======================================================");

                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddNewGame();
                        break;
                    case "2":
                        AddNewConsole();
                        break;
                    case "3":
                        ConsoleIDs();
                        break;
                    case "4":
                        DeleteGames();
                        break;
                    case "5":
                        DeleteConsoles();
                        break;
                    case "9":
                        return; // returns to Main Menu
                    case "0":
                        Environment.Exit(0); // exits app
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again..");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // main menu functions
        static void AllEntries()
        {
            Console.Clear();
            Console.WriteLine("                --- All Entries ---");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine(" ");
            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    string sql = @"
                        select Game.Title, Console.Console, Game.Release
                        from [ArcadeBlockade].[dbo].[Game] join [ArcadeBlockade].[dbo].[Console]
                        on Game.ConID = Console.ConID";

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // This aligns columns: 30 characters for Title, 15 for Console
                            Console.WriteLine($"{reader["Title"].ToString().Trim().PadRight(45)} | {reader["Console"].ToString().Trim().PadRight(15)} | {reader["Release"]}");
                            //Console.WriteLine($"{reader["Title"].ToString().Trim()}, {reader["Release"]}, {reader["Console"].ToString().Trim()}");
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
            }
            Console.WriteLine(" ");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }
        static void SearchForGame()
        {
            Console.Clear();
            Console.WriteLine("                --- Game Search ---");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine(" search Games with only a part of the name needed");
            Console.WriteLine(" Example: 'Petshop' for Littlest Petshop");
            Console.WriteLine("---------------------------------------------------------");

            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    string sql = @"
                        select Game.Title, Console.Console, Game.Release
                        from [ArcadeBlockade].[dbo].[Game] join [ArcadeBlockade].[dbo].[Console]
                        on Game.ConID = Console.ConID
                        where Game.Title like '%' + @search + '%'
                        order by Release asc";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        Console.Write("Enter game title to search: ");
                        string searchTerm = Console.ReadLine();
                        cmd.Parameters.AddWithValue("@search", searchTerm);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader["Title"].ToString().Trim().PadRight(45)} | {reader["Console"].ToString().Trim().PadRight(15)} | {reader["Release"]}");
                            }
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
            }
            Console.WriteLine(" ");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }
        static void SearchForConsole()
        {
            Console.Clear();
            Console.WriteLine("              --- Console Search ---");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine(" search games by Consoles! ");
            Console.WriteLine(" Example: 'Gameboy' to get all Gameboy consoles");
            Console.WriteLine("---------------------------------------------------------");
            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    string sql = @"
                        select g.Title, c.Console, g.Release
                        from [ArcadeBlockade].[dbo].[Game] g
                        join [ArcadeBlockade].[dbo].[Console] c on g.ConID = c.ConID
                        where c.Console like '%' + @search + '%'
                        order by Release asc";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        Console.Write("Enter console name to search: ");
                        string searchTerm = Console.ReadLine();
                        cmd.Parameters.AddWithValue("@search", searchTerm);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader["Title"].ToString().Trim().PadRight(45)} | {reader["Console"].ToString().Trim().PadRight(15)} | {reader["Release"]}");
                            }
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
            }
            Console.WriteLine(" ");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }
        static void GamesCounter()
        {
            Console.Clear();
            Console.WriteLine("               --- Games Counter ---");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine(" With this we can see how many games we have");
            Console.WriteLine(" Pr Console");
            Console.WriteLine("---------------------------------------------------------");
            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    string sql = @"
                        select
                            c.Console, 
                            count(g.Title) AS TotalGames
                        from [ArcadeBlockade].[dbo].[Console] c
                        left join [ArcadeBlockade].[dbo].[Game] g on c.ConID = g.ConID
                        group by c.Console
                        order by TotalGames desc;";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["Console"].ToString().Trim().PadRight(30)} | {reader["TotalGames"]}");
                        }
                    }

                }
                catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
            }
            Console.WriteLine(" ");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }

        static void OldestNewest()
        {
            Console.Clear();
            Console.WriteLine("           --- Oldest & Newest Games ---");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine(" Here we see oldest & newest releases!");
            Console.WriteLine("---------------------------------------------------------");
            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    string sql = @"
                        with RankedGames as (
                            select
                                Game.Title, 
                                Console.Console, 
                                Game.Release,
                                row_number() over(partition by Console.ConID order by Game.Release asc) as oldest_rank,
                                row_number() over(partition by Console.ConID order by Game.Release desc) as newest_rank
                            from [ArcadeBlockade].[dbo].[Game]
                            join [ArcadeBlockade].[dbo].[Console] on Game.ConID = Console.ConID
                        )
                        select 
                            Console, 
                            Title, 
                            Release,
                            case 
                                when oldest_rank = 1 then 'Oldest' 
                                when newest_rank = 1 then 'Newest' 
                            end as status 
                        from RankedGames
                        where oldest_rank = 1 or newest_rank = 1
                        order by Console, Release;";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["Console"].ToString().Trim().PadRight(30)} | {reader["Title"].ToString().Trim().PadRight(45)} | {reader["Release"]} | {reader["status"]}");
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
            }
            Console.WriteLine(" ");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }
        static void PeriodSearch()
        {
            Console.Clear();
            Console.WriteLine("               --- Period Search ---");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine(" With this we can search for games over a timespan");
            Console.WriteLine(" Example: 1990 to 2000 will show games released in the 90s");
            Console.WriteLine("---------------------------------------------------------");
            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    string sql = @"
                        select Game.Title, Console.Console, Game.Release
                        from [ArcadeBlockade].[dbo].[Game] join [ArcadeBlockade].[dbo].[Console]
                        on Game.ConID = Console.ConID
                        where Game.Release between @start and @end
                        order by Release asc";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        Console.Write("Enter start year: ");
                        int startYear = int.Parse(Console.ReadLine());
                        Console.Write("Enter end year: ");
                        int endYear = int.Parse(Console.ReadLine());
                        cmd.Parameters.AddWithValue("@start", startYear);
                        cmd.Parameters.AddWithValue("@end", endYear);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"{reader["Title"].ToString().Trim().PadRight(45)} | {reader["Console"].ToString().Trim().PadRight(15)} | {reader["Release"]}");
                            }
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
            }
            Console.WriteLine(" ");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }

        // creation menu functions
        static void ConsoleIDs()
        {
            Console.Clear();
            Console.WriteLine("               --- Console IDs ---");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine(" ");
            Console.WriteLine("------------------------ ------------------------");
            Console.WriteLine("    --Nintendo 10x--          -- Atari 20x--");
            Console.WriteLine("------------------------ ------------------------");
            Console.WriteLine("--NES               101  --Atari 2600        201");
            Console.WriteLine("--SNES              102  ------------------------");
            Console.WriteLine("--N64               103     -- Commodore 30x--");
            Console.WriteLine("--GameCube          104  ------------------------");
            Console.WriteLine("--Gameboy           105  --Commodore 64      301");
            Console.WriteLine("--Gameboy Colour    106");
            Console.WriteLine("--Gameboy Advance   107");
            Console.WriteLine("--Nintendo DS       108");
            Console.WriteLine("--Nintendo DS Lite  109");
            Console.WriteLine("--Nintendo DS XL    110");
            Console.WriteLine(" ");
            Console.WriteLine("------------------------ ------------------------");
            Console.WriteLine("    -- Sony 40x--           -- Microsoft 50x--");
            Console.WriteLine("------------------------ ------------------------");
            Console.WriteLine("--Playstation 1     401  --Xbox              501");
            Console.WriteLine("--Playstation 2     402  --Xbox 360          502");
            Console.WriteLine("--Playstation 3     403  --Xbox One          503");
            Console.WriteLine(" ");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }
        static void AddNewGame()
        {
            Console.Clear();
            Console.WriteLine("               --- Add New Games ---");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine(" --- Paste SQL-Style Values (Multiple Lines) ---");
            Console.WriteLine(" Format: ('Title', ConID, Release Year),");
            Console.WriteLine(" Add games and press ENTER on a blank line to finish:");
            Console.WriteLine("---------------------------------------------------------");

            StringBuilder sb = new StringBuilder();
            string line;

            // This loop keeps grabbing lines until it hits an empty one
            while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
            {
                sb.AppendLine(line);
            }

            string input = sb.ToString();

            // just in case there are weird characters between the entries.
            string pattern = @"\(\s*'(?<Title>.+?)'\s*,\s*(?<ConID>\d+)\s*,\s*(?<Release>\d+)\s*\)";
            MatchCollection matches = Regex.Matches(input, pattern, RegexOptions.Singleline);

            if (matches.Count == 0)
            {
                Console.WriteLine("No valid entries found. Check your formatting!");
            }
            else
            {
                InsertMatchesToDatabase(matches);
            }

            Console.WriteLine(" ");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }
        // helper method to insert the matched entries into the database
        static void InsertMatchesToDatabase(MatchCollection matches)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    int successCount = 0;

                    foreach (Match match in matches)
                    {
                        string sql = "insert into Game (Title, ConID, Release) values (@Title, @ConID, @Release)";
                        using (SqlCommand cmd = new SqlCommand(sql, connection))
                        {
                            cmd.Parameters.AddWithValue("@Title", match.Groups["Title"].Value.Trim());
                            cmd.Parameters.AddWithValue("@ConID", int.Parse(match.Groups["ConID"].Value));
                            cmd.Parameters.AddWithValue("@Release", int.Parse(match.Groups["Release"].Value));
                            cmd.ExecuteNonQuery();
                            successCount++;
                        }
                    }
                    Console.WriteLine($"Success! Added {successCount} games.");
                }
                catch (Exception ex) { Console.WriteLine("\nDatabase Error: " + ex.Message); }
            }
        }
        static void AddNewConsole()
        {
            Console.Clear();
            Console.WriteLine("             --- Add New Consoles ---");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine(" --- Paste Console SQL-Style Values ---");
            Console.WriteLine(" Format: (ConID, 'ConsoleName', 'Company'),");
            Console.WriteLine(" Paste your block and press ENTER on a blank line:");
            Console.WriteLine("---------------------------------------------------------");

            StringBuilder sb = new StringBuilder();
            string line;
            while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
            {
                sb.AppendLine(line);
            }

            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();
                    int successCount = 0;
                    var entries = sb.ToString().Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var entry in entries)
                    {
                        string clean = entry.Trim().TrimEnd(',').Replace("(", "").Replace(")", "").Replace("'", "");
                        string[] parts = clean.Split(',');

                        if (parts.Length == 3)
                        {
                            string sql = "insert into Console (ConID, Console, Company) values (@ConID, @Console, @Company)";

                            using (SqlCommand cmd = new SqlCommand(sql, connection))
                            {
                                cmd.Parameters.AddWithValue("@ConID", int.Parse(parts[0].Trim()));
                                cmd.Parameters.AddWithValue("@Console", parts[1].Trim());
                                cmd.Parameters.AddWithValue("@Company", parts[2].Trim());
                                cmd.ExecuteNonQuery();
                                successCount++;
                            }
                        }
                    }
                    Console.WriteLine($"Successfully added {successCount} consoles!");
                }
                catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
            }
            Console.WriteLine(" ");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }
        static void DeleteGames()
        {
            Console.Clear();
            Console.WriteLine("           --- Delete Games by Title ---");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine(" Enter titles separated by commas or new lines.");
            Console.WriteLine(" Example: 'Pac-Man', 'Digimon World'");
            Console.WriteLine(" Press ENTER on a blank line to finish:");
            Console.WriteLine("---------------------------------------------------------");

            StringBuilder sb = new StringBuilder();
            string line;
            while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
            {
                sb.AppendLine(line);
            }

            // Clean up the input into a list of titles
            var titlesToDelete = sb.ToString()
                .Split(new[] { ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim().Trim('\'')) // Removes spaces and wrapping quotes
                .ToList();

            if (titlesToDelete.Count == 0) return;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();

                    // --- STEP 1: THE SAFEGUARD (PREVIEW) ---
                    Console.WriteLine("\nReviewing database for matches...");
                    List<string> foundGames = new List<string>();

                    foreach (var title in titlesToDelete)
                    {
                        string checkSql = "SELECT Title, Release FROM Game WHERE RTRIM(Title) = @T";
                        using (SqlCommand checkCmd = new SqlCommand(checkSql, connection))
                        {
                            checkCmd.Parameters.AddWithValue("@T", title);
                            using (SqlDataReader reader = checkCmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    foundGames.Add($"{reader["Title"].ToString().Trim()} ({reader["Release"]})");
                                }
                            }
                        }
                    }

                    if (foundGames.Count == 0)
                    {
                        Console.WriteLine("No matching games found in the database.");
                        return;
                    }

                    Console.WriteLine("\nTHE FOLLOWING ENTRIES WILL BE DELETED:");
                    foreach (var game in foundGames) Console.WriteLine($"- {game}");

                    // --- STEP 2: THE CONFIRMATION ---
                    Console.Write("\nAre you absolutely sure? (Type 'YES' to confirm): ");
                    string confirm = Console.ReadLine().ToUpper();

                    if (confirm == "YES")
                    {
                        int totalDeleted = 0;
                        foreach (var title in titlesToDelete)
                        {
                            string deleteSql = "delete from Game where rtrim(Title) = @T";
                            using (SqlCommand delCmd = new SqlCommand(deleteSql, connection))
                            {
                                delCmd.Parameters.AddWithValue("@T", title);
                                totalDeleted += delCmd.ExecuteNonQuery();
                            }
                        }
                        Console.WriteLine($"\nSuccess! {totalDeleted} games removed.");
                    }
                    else
                    {
                        Console.WriteLine("\nOperation cancelled. No data was harmed.");
                    }
                }
                catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
            }
            Console.WriteLine(" ");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }
        static void DeleteConsoles()
        {
            Console.Clear();
            Console.WriteLine("          --- Delete Consoles by Name ---");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine(" Enter names separated by commas or new lines.");
            Console.WriteLine(" Example: 'Playstation 2', 'Xbox One'");
            Console.WriteLine(" Press ENTER on a blank line to finish:");
            Console.WriteLine("---------------------------------------------------------");

            StringBuilder sb = new StringBuilder();
            string line;
            while (!string.IsNullOrWhiteSpace(line = Console.ReadLine()))
            {
                sb.AppendLine(line);
            }

            // Clean up the input into a list of names
            var namesToDelete = sb.ToString()
                .Split(new[] { ',', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(n => n.Trim().Trim('\''))
                .ToList();

            if (namesToDelete.Count == 0) return;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                try
                {
                    connection.Open();

                    // --- STEP 1: THE PREVIEW ---
                    Console.WriteLine("\nSearching for matching consoles...");
                    List<string> foundConsoles = new List<string>();

                    foreach (var name in namesToDelete)
                    {
                        // We use RTRIM because of the nchar(100) padding
                        string checkSql = "SELECT ConID, Console FROM Console WHERE RTRIM(Console) = @Name";
                        using (SqlCommand checkCmd = new SqlCommand(checkSql, connection))
                        {
                            checkCmd.Parameters.AddWithValue("@Name", name);
                            using (SqlDataReader reader = checkCmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    foundConsoles.Add($"ID: {reader["ConID"]} - {reader["Console"].ToString().Trim()}");
                                }
                            }
                        }
                    }

                    if (foundConsoles.Count == 0)
                    {
                        Console.WriteLine("No matching consoles found.");
                        return;
                    }

                    Console.WriteLine("\nTHE FOLLOWING CONSOLES WILL BE DELETED:");
                    foreach (var c in foundConsoles) Console.WriteLine($"- {c}");

                    // --- STEP 2: THE CONFIRMATION ---
                    Console.WriteLine("\nWARNING: Deleting a console will fail if games are still linked to it.");
                    Console.Write("Type 'YES' to confirm deletion: ");
                    string confirm = Console.ReadLine().ToUpper();

                    if (confirm == "YES")
                    {
                        int totalDeleted = 0;
                        int failedCount = 0;

                        foreach (var name in namesToDelete)
                        {
                            try
                            {
                                string deleteSql = "DELETE FROM Console WHERE RTRIM(Console) = @Name";
                                using (SqlCommand delCmd = new SqlCommand(deleteSql, connection))
                                {
                                    delCmd.Parameters.AddWithValue("@Name", name);
                                    int rows = delCmd.ExecuteNonQuery();
                                    totalDeleted += rows;
                                }
                            }
                            catch (SqlException ex) when (ex.Number == 547)
                            {
                                Console.WriteLine($"[Error] Could not delete '{name}': Games are still linked to it.");
                                failedCount++;
                            }
                        }
                        Console.WriteLine($"\nProcess finished. Deleted: {totalDeleted} | Blocked: {failedCount}");
                    }
                    else
                    {
                        Console.WriteLine("\nOperation cancelled.");
                    }
                }
                catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }
            }
            Console.WriteLine(" ");
            Console.WriteLine("============ ============ ============ ============");
            Console.WriteLine("Press any key to go back...");
            Console.ReadKey();
        }
    }
}