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

    public Room(string name, string description)
    {
      Name = name;
      Description = description;
      isSolved = false;
    }
  }
}