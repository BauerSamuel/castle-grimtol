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
    public Challenge challenge { get; set; }
    public bool isSolved { get; set; } = false;



    public void AttemptChallenge()
    {
      //Logic to attempt challenge. print challenge.Problem. If user inputs challenge.answer, then room.isSolved becomes true. Else, recurse to AttemptChallenge.
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