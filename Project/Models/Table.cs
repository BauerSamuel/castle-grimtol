using System;


namespace CastleGrimtol.Project.Models
{
  public class Table
  {
    private bool hasOneStick { get; set; } = false;
    private bool hasTwoSticks { get; set; } = false;
    public bool flipped { get; set; } = false;

    public void addALeg()
    {
      if (!hasOneStick)
      {
        hasOneStick = true;
        return;
      }
      else if (!hasTwoSticks)
      {
        hasTwoSticks = true;
        flipped = true;
        return;
      }
      return;
    }
  }
}