using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
class Program
{
    public static List<Task> tasks = new List<Task>();
    static void Main()
    {
        Random random = new Random();
        List<Group> groups = new List<Group>();
        Console.Write("Number of teams = ");
        int number = int.Parse(Console.ReadLine());
        Console.WriteLine(new string('_', 20));
        for (int i = 0; i < number; i++)
        {
            groups.Add(new Group(i + 1, random.Next(2, 10), random.Next(10, 100)));
        }
        Game g = new Game(groups);
        g.start_game();
        g.bastau();
        for (int i = 0; i < groups.Count; i++)
        {
            Console.WriteLine(groups[i]);
        }
        Console.ResetColor();
        Console.ReadKey();
    }
}
class Group
{
    public int group_id;
    public int players;
    public int points;
    public int win = 0;
    public bool alive = true;

    public Group(int group_id, int players, int points)
    {
        this.group_id = group_id;
        this.players = players;
        this.points = points;
        Console.WriteLine(ToString());
        Console.ResetColor();
    }
    public override string ToString()
    {
        if (alive == true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            return $"Team - {group_id}\nPlayers - {players}\nPoints - {points}\nWin - {win} times";
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            return $"Team - {group_id}  lost";
        }

    }
}

class Game
{
    public List<Group> groups;

    public Game(List<Group> groups)
    {
        this.groups = groups;
    }
    public void bastau()
    {
        foreach (var task in Program.tasks)
        {
            task.Start();
            //task.RunSynchronously();
            //task.Wait();
        }
        Task.WaitAll(Program.tasks.ToArray());
    }
    public void start_game()
    {
        for (int i = 0; i < groups.Count; i++)
        {
            for (int j = i + 1; j < groups.Count; j++)
            {
                new MyTask(i, j, groups);
            }
        }
        Thread.Sleep(2000);
    }
}


class MyTask
{
    public int i, j;
    public List<Group> groups;
    public Random random = new Random();
    public Task task;
    public MyTask(int i, int j, List<Group> groups)
    {
        this.i = i;
        this.j = j;
        this.groups = groups;
        task = new Task(play);
        Program.tasks.Add(task);
    }
    public void play()
    {
        Console.WriteLine($"Team {groups[i].group_id} vs Team {groups[j].group_id}");
        if (groups[i].points > groups[j].points)
        {
            result(groups[i], groups[j]);
        }
        else
        {
            if (groups[i].points == groups[j].points)
            {
                result(groups[i], groups[j]);
            }
            else
            {
                result(groups[j], groups[i]);
            }
        }
    }
    public void result(Group win, Group lose)
    {
        if (win.points == lose.points)
        {
            Console.WriteLine("Draw");
            return;
        }
        Console.WriteLine($"Team {win.group_id} win!");
        int num = random.Next(3, 5);
        win.players += num;
        lose.players -= num;
        win.win += 1;
        win.points = random.Next(10, 100);
        Console.WriteLine($"Team {win.group_id} gets +{num} players");
        Console.WriteLine($"Team {lose.group_id} loses -{num} players");
    }
    public void check()
    {
        for (int i = 0; i < groups.Count; i++)
        {
            if (groups[i].players <= 0)
            {
                groups[i].alive = false;
            }
        }
    }

}




