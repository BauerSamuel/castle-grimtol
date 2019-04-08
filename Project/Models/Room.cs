using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }
    public Challenge Challenge { get; set; }
    public Table Table { get; set; } = null;
    public bool isSolved { get; set; } = false;



    public void AttemptChallenge()
    {
      //Logic to attempt challenge. print challenge.Problem. If user inputs challenge.answer, then room.isSolved becomes true. Else, recurse to AttemptChallenge.
      Console.WriteLine(Challenge.Problem);
      Console.WriteLine("Fill in the blank to solve: ");
      string sol = Console.ReadLine();
      if (sol.ToLower() == Challenge.Solution.ToLower())
      {
        Random rnd = new Random();
        int rando = rnd.Next(1, 5);
        if (rando == 1)
        {
          Console.WriteLine("Robot speaks.\"Well done!\" --Door to next room is now unlocked--");
        }
        else if (rando == 2)
        {
          Console.WriteLine("Robot speaks. \"These are so practical, amazing work solving it.\"--Door to next room is now unlocked--");
        }
        else if (rando == 3)
        {
          Console.WriteLine("Robot speaks. \"You wouldn't believe how important it is to us that you solved that for us.\"--Door to next room is now unlocked--");
        }
        else if (rando == 4)
        {
          Console.WriteLine("Robot speaks. \"You're a wizard, Harry. Good job.\"--Door to next room is now unlocked--");
        }
        else
        {
          Console.WriteLine("Robot speaks. \"Incredible. That was fast. Good job. --Door to next room is now unlocked--\"");
        }

        isSolved = true;
      }
      else
      {
        Random rnd = new Random();
        int rando = rnd.Next(1, 5);
        if (rando == 1)
        {
          Console.WriteLine("Robot speaks. \"FAIL!\"");
        }
        else if (rando == 2)
        {
          Console.WriteLine("Robot speaks. \"Try again.\"");
        }
        else if (rando == 3)
        {
          Console.WriteLine("Robot speaks. \"You really didn't get it? I could solve this problem with one stick of RAM.\"");
        }
        else if (rando == 4)
        {
          Console.WriteLine("Robot speaks. \"Interesting... you're just messing with us aren't you?\"");
        }
        else
        {
          Console.WriteLine("Robot speaks. \"Well I guess the world needs bad coders so good coders have something to fix... try again.\"");
        }
        AttemptChallenge();
      }
    }
    public Room(string name, string description)
    {
      Name = name;
      Description = description;
      isSolved = false;
      Exits = new Dictionary<string, IRoom>();
      Items = new List<Item>();
    }
  }
}