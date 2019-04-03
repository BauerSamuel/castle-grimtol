using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    public Room CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }

    public void GetUserInput()
    {
      Console.ReadLine();
      return;
    }

    public void Go(string direction)
    {
      throw new System.NotImplementedException();
    }

    public void Help()
    {
      throw new System.NotImplementedException();
    }

    public void Inventory()
    {
      throw new System.NotImplementedException();
    }

    public void Look()
    {
      throw new System.NotImplementedException();
    }

    public void Quit()
    {
      throw new System.NotImplementedException();
    }

    public void Reset()
    {
      throw new System.NotImplementedException();
    }

    public void Setup()
    {
      //build everything here.
      Room room1 = new Room("Room 1", "It's dark. You set your feet on the ground and stand, somehow exactly as some white-LED lights turn on so now you can see. If you look around, you notice an industrial looking room. Lots of wires hanging from the high ceiling, large and small pipes running through the ceiling, a smell of metal and production, yet also ancient smells, like earth and damp leaves. You now notice that many wires hanging from the ceiling are actually vines--the room was certainly abandoned until now. You notice various rubble scattered about the room, this facility seems so old that it's falling apart. The northern wall has a bench against it, and the wall itself has markings that suggest some artwork was hanging on the wall before. Shattered glass on the floor below next to a small wooden picture-frame proves that. The eastern wall has a closed door in the middle (locked until challenge is solved). The western wall has a long table against it. The table has various rubble scattered on it: old papers, crumbled Styrofoam, dirt, dead leaves, and what appears to be an empty vase. The south the wall is well-lit and mostly empty. Something on the wall looks familiar to you… is that a…. Whiteboard? Yes, definitely a whiteboard. With writing all over it. Just as you notice this, a crunchy, high-pitched sound tears through the silence. A robot voice. \"Hello.Welcome to E - Corp.You have been selected to interview with us.Please solve the whiteboard challenge on the south wall to continue!\"");
      Room room2 = new Room("Room 2", "Musty and dim except for another well-lit whiteboard on the south wall. Various rubble scattered around the room, but nothing really noteworthy. Robot voice continues. \"Well done! You've found that when you solve problems, you advance. We are already impressed by you and want you to know you are one step closer to getting the job.\"");
      Room room3 = new Room("Room 3", "Vines and cobwebs cover the walls. Yet another well-lit whiteboard is on the south wall, slightly crooked, as it's probably been there for a long time. Patches of rugged looking plants spew from the ground in several places, but there's nothing else interesting in here. The robot pips in. \"You know there was a better solution to the last problem right? You're just displaying your skill in diverse solutions I suppose. No matter. We know you'll work harder and get the perfect solution this round.\"");
      Room room4 = new Room("Room 4", "This room is slightly different than the rest. The white-board has now moved over to the east wall, and the door is on the south wall. You do notice something out of the corner of your eye, at the north wall. The ceiling has a small crack, through it you see a skeleton with eyebrows. Creepy. Clutched in its hand, hanging against the north wall, is a wooden cane. The locked door is on the south wall. Robot seems a little annoyed by you now. \"So... we're talking back here and we can't believe you're authentic. Keep going of course, these solutions mean everything to us. But we're starting to think you are taking our innovative white-board questions to sell on the internet. We would like to remind you that our facility is 100% farrady cage, and wireless internet just doesn't work or exist here. Be weary, interviewee.\"");
      Room room5 = new Room("Room 5", "In this room, the west wall is plain, with even more rubble and debris than the last though. East wall has another whiteboard. Robot is silent so far.");
      Room room6 = new Room("Room 6", "Now this room is interesting, it has a collapsed roof on the west wall, so lots of debris there. Against that wall, there's an upside down table with only two legs. East wall has whiteboard and more debris, and a pipe laying across the ground. South wall has locked door.");
      Room room7 = new Room("Room 7", "");
      Room room8 = new Room("Room 8", "");
      Room room9 = new Room("Room 9", "");
      Room room10 = new Room("Room 10", "Robot insults you. Whiteboard is on south wall. Strange podium is against the north wall. No door in this room.");
      Room parkingLot = new Room("Parking Lot", "You jump down from the roof on to the mushy ground. It's now night. The smell of death immidiately swells in your nostrils. A large decrepid field, maybe it was once a parking lot in a different time, is almost completely enclosed by the walls of the facility you've just escaped from. To your horror, robot peices and bones protrude from the ground everywhere you look. Possibly the remnants of an ancient battle. The half-moon provides you some light. You notice an opening in the west side of the parking lot.");
    }

    public void StartGame()
    {
      throw new System.NotImplementedException();
    }

    public void TakeItem(string itemName)
    {
      throw new System.NotImplementedException();
    }

    public void UseItem(string itemName)
    {
      throw new System.NotImplementedException();
    }
  }
}