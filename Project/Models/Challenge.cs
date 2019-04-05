namespace CastleGrimtol.Project.Models
{
  public class Challenge
  {
    public string Problem { get; }
    public string Solution { get; }

    public Challenge(string prob, string sol)
    {
      Problem = prob;
      Solution = sol;
    }
  }
}