using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class EndRoom : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }

    public EndRoom(string name, string description)
    {
      Name = name;
      Description = description;
    }
  }
}