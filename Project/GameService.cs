using System;
using System.Collections.Generic;
using System.Threading;
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
      string input = Console.ReadLine();
      string[] inputs = input.Split(" ");
      if (inputs.Length < 2)
      {
        switch (input)
        {
          case "help":
            Help();
            GetUserInput();
            break;
          case "inventory":
            Inventory();
            GetUserInput();
            break;
          case "look":
            Console.Clear();
            Look();
            break;
          case "quit":
            Quit();
            break;
          case "reset":
            Reset();
            break;
        }
        GetUserInput();
      }
      else
      {
        string action = inputs[0].ToLower();
        string noun = inputs[1].ToLower();
        switch (action)
        {
          case "go":
            Go(noun);
            GetUserInput();
            break;
          case "take":
            TakeItem(noun);
            GetUserInput();
            break;
          default:
            Console.WriteLine("Command not recognized. Try again.");
            GetUserInput();
            break;
        }
      }
    }

    public void Go(string direction)
    {
      if (CurrentRoom.Exits.ContainsKey(direction))
      {
        if (CurrentRoom.isSolved)
        {
          CurrentRoom = (Room)CurrentRoom.Exits[direction];
          Console.WriteLine(CurrentRoom.Description);
        }
        else
        {
          Console.WriteLine("Door won't budge. Use marker to complete white board challenge and advance to the next room.");
          GetUserInput();
        }
      }
      else
      {
        Console.WriteLine($"Can't move {direction}, choose another direction.");
      }
      GetUserInput();
    }

    public void Help()
    {
      //Display all options for user to choose.
      Console.WriteLine("Shows options for player.");
    }

    public void Inventory()
    {
      Console.WriteLine("Items in inventory:");
      foreach (Item i in CurrentPlayer.Inventory)
      {
        Console.WriteLine(i.Name);
      }
    }

    public void Look()
    {
      Console.WriteLine(CurrentRoom.Description);
      if (CurrentRoom.Items.Count > 0)
      {
        foreach (Item i in CurrentRoom.Items)
        {
          Console.WriteLine(i.Name);
        }
      }

    }

    public void Quit()
    {
      Console.WriteLine("Quits out of game.");
    }

    public void Reset()
    {
      Console.WriteLine("Starts from beggining.");
    }

    public void Setup()
    {
      //build rooms here.
      Room room1 = new Room("Room 1", "It's dark. You set your feet on the ground and stand, somehow exactly as some white-LED lights turn on so now you can see. If you look around, you notice an industrial looking room. Lots of wires hanging from the high ceiling, large and small pipes running through the ceiling, a smell of metal and production, yet also ancient smells, like earth and damp leaves. You now notice that many wires hanging from the ceiling are actually vines--the room was certainly abandoned until now. You notice various rubble scattered about the room, this facility seems so old that it's falling apart. The northern wall has a bench against it, and the wall itself has markings that suggest some artwork was hanging on the wall before. Shattered glass on the floor below next to a small wooden picture-frame proves that. The eastern wall has a closed door in the middle (locked until challenge is solved). The western wall has a long table against it. The table has various rubble scattered on it: old papers, crumbled Styrofoam, dirt, dead leaves, and what appears to be an empty vase with a dry-erase marker in it. The south the wall is well-lit and mostly empty. Something on the wall looks familiar to you… is that a…. Whiteboard? Yes, definitely a whiteboard. With writing all over it. Just as you notice this, a crunchy, high-pitched sound tears through the silence. A strange robot voice, reminds you of both jar-jar binks and 343 guilty-spark from the Halo series. \"Hello. Welcome to E - Corp.You have been selected to interview with us. Please solve the whiteboard challenge on the south wall to continue!\"");
      Room room2 = new Room("Room 2", "Musty and dim except for another well-lit whiteboard on the south wall. Various rubble scattered around the room, but nothing really noteworthy. Robot voice continues. \"Well done! You've found that when you solve problems, you advance. We are already impressed by you and want you to know you are one step closer to getting the job.\"");
      Room room3 = new Room("Room 3", "Vines and cobwebs cover the walls. Yet another well-lit whiteboard is on the south wall, slightly crooked, as it's probably been there for a long time. Patches of rugged looking plants spew from the ground in several places, but there's nothing else interesting in here. The robot pips in. \"You know there was a better solution to the last problem right? You're just displaying your skill in diverse solutions I suppose. No matter. We know you'll work harder and get the perfect solution this round.\"");
      Room room4 = new Room("Room 4", "This room is slightly different than the rest. The white-board has now moved over to the east wall, and the door is on the south wall. You do notice something out of the corner of your eye, at the north wall. The ceiling has a small crack, through it you see a skeleton with eyebrows. Creepy. Clutched in its hand, hanging against the north wall, is a wooden cane. The locked door is on the south wall. Robot seems a little annoyed by you now. \"So... we're talking back here and we can't believe you're authentic. Keep going of course, these solutions mean everything to us. But we're starting to think you are taking our innovative white-board questions to sell on the internet. We would like to remind you that our facility is 100% a farrady cage, and wireless internet just doesn't work or exist here. Be weary, interviewee.\"");
      Room room5 = new Room("Room 5", "In this room, the west wall is plain, with even more rubble and debris than the last though. East wall has a whiteboard. Other walls are blank. Robot is silent so far.");
      Room room6 = new Room("Room 6", "Now this room is interesting, it has a collapsed roof on the west wall, so lots of debris there. Against that wall, there's an upside down table with only two legs. East wall has whiteboard and more debris, and a pipe laying across the ground with some things sticking out of the end. . South wall has locked door.");
      Room room7 = new Room("Room 7", "Whiteboard on east wall. \"You are excellent. We are impressed and offer a surprise for you if you complete the next 4 challenges. Good luck, brave interviewee.\"");
      Room room8 = new Room("Room 8", "\"In case you didn't notice, I am a robot. I have been here for thousands of years, and I've never felt better. I'm so youthful, in fact, that I prepared quite the surprise for you in room 10. I heard you like popcorn so I had our engineers pop 300 kilograms of the stuff in anticipation of you finishing the interview. We decided it was mostly possible that you are NOT trying to 'Slugworth' us. See? I'm hip, I know the pop-culture of the world. I read Charlie and the Chocolate factory.\" This room is looking seriously decrepid. Rubble everywhere. Thousands of years of whiteboard challenges must have predecessed you. You feel a pulse in the air, as if thousands of computer scientists everywhere are chearing for you, hoping and waiting excitedly to finish these lasts whiteboard challenges. The hairs on your arm stand. Your pulse beats in your eardrums, it's deafening. You must carry on.");
      Room room9 = new Room("Room 9", "The rubble and plants swirl around your feet as you walk into room 9. The air is dense, musty, yet full of life. You glance at the door on the west wall. Behind it, the final challenge. The popcorn. You feel the chear of the world's computer scientists swelling, chearing you on in spirit. Like your in the final round of some bizarre game show that no one has ever really won before. The robot is silent.");
      Room room10 = new Room("Room 10", "\"Let me tell you something, no one has ever solved this last challenge. It's the result of thousands of years of trial and error, error and trial. The greatest minds in the world created the greatest robots in the world, which then created a final super computer. That very super computer is me, and I am the result. I am the test. I am the origin of all white board problems. And alone you stand, with marker and brain, to solve my final test. We're actually adversaries, you know? All along we knew about you. Those who fortold of you have been long dead, everyone but me. Now here we are. Your test is on the whiteboard. Write your solution there, if you're worthy.\" As you approach the whiteboard, you hear deep thunder, somewhere in the depths of the massive and ancient facility. Trembling in the earth. The north wall is a strange color, different than other walls you've seen. You notice this white board is blank. The robot said 'Write you solution here?' What could he mean? The solution to what? What is it? You raise your shaking arm. Your shoulder is sore from all the whiteboard challenges you've defeated. The marker feels heavy. You prepare to write your final answer.");
      Room parkingLot = new Room("Parking Lot", "You jump down on to the mushy ground. It's now night. The smell of death immidiately swells in your nostrils. A large decrepid field, maybe it was once a parking lot in a different time, is almost completely enclosed by the walls of the facility you've just escaped from. To your horror, robot peices and bones protrude from the ground everywhere you look. Possibly the remnants of an ancient battle. The half-moon provides you some light. You notice an opening in the west side of the parking lot.");
      Room desert = new Room("Desert", "You walk through the parking lot turnstile and into the open desert of southern Idaho. Sand swirls around you. You breath in air and some grit. You feel free, despite the wastes in front of you. As you march into the desert, you muse over the fact that you're still unemployed.");

      //Add exits to rooms.
      room1.Exits.Add("east", room2);
      room2.Exits.Add("east", room3);
      room3.Exits.Add("east", room4);
      room4.Exits.Add("south", room5);
      room5.Exits.Add("south", room6);
      room6.Exits.Add("south", room7);
      room6.Exits.Add("west", parkingLot);
      room7.Exits.Add("west", room8);
      room8.Exits.Add("west", room9);
      room9.Exits.Add("west", room10);
      room10.Exits.Add("north", parkingLot);
      parkingLot.Exits.Add("west", desert);


      parkingLot.isSolved = true;


      //Create items
      Item marker1 = new Item("marker", "You slide your hand into the vase and grab the marker. It feels cold. It's old, and you have the feeling the marker could hypothetically have enough ink to solve around 6 whiteboard challenges.");
      Item marker2 = new Item("marker", "You also find another old marker. Perfect timing since your marker just ran out of ink. --Marker added to inventory--");
      Item cane1 = new Item("cane 1", "You reach up and tear the cane away from the skeleton's clutch. Phalangies and carpals go flying. --Cane added to inventory--");
      Item cane2 = new Item("cane 2", "You look in the pipe and find a wooden cane, this one looks like it fits somewhere. Maybe it's not actually a cane. --Cane added to inventory--");

      //Add items to rooms
      room1.Items.Add(marker1);
      room4.Items.Add(cane1);
      room6.Items.Add(cane2);
      room6.Items.Add(marker2);


      CurrentRoom = room1;
    }

    public void StartGame()
    {
      Setup();
      Console.WriteLine("Who are you?");
      string pn = Console.ReadLine();
      CurrentPlayer = new Player("pn");
      string intro = "You have just finished a coding boot-camp at Coise BodeWorks. You feel prepared for the world, but your next step is to get a job, and that makes you nervous. You notice an email in your inbox from E-Corp. \"Only the largest tech company this side of the Mississippi!\" you say jokingly in your old instructor's voice. You open the email nervously,despite not wanting to, prepared for something big.  Email says: \"Congrats, you have been selected! We received your resume before and are pleased to invite you to our new DesertSide LAB for evaluation for a position. If you can get to Twin Falls, Idaho before May 15th, you can take the CY-PHI EVOSPEED Train 1.5 hours directly to our facility. Our location is rather secret so we ask you don't inform others of your journey. We hope to see you soon.\"  Even a fool wouldn't pass up this offer, you pack your bags and prepare to leave the following morning. Sleep comes to you, and you're alright with that. You'll leave early in the morning, you decide.\n";
      string intro2 = "You awake early and start driving to Twin Falls, you arrive in a few hours. You find the amazing train station. It's quite ridiculous actually, such an expensive and technologically advanced project to only be used for the employees of E-corp. But it does need to travel through the harsh desert to the famous new DesertSide LAB. You show your ID to the ticket person, they radio something in but immediately stamp your ticket and send you on your way. You zone in and out of sleep, after the initial novelty wears off of traveling at 237mph. You're startled awake by the stewardess, who offers you water. You take it and drink, and your stomach feels as if it’s in free-fall. What's in that water? Immediate black-out………\n";
      string intro3 = "You roll off the side of a short, uncomfortable table. The strange things you were dreaming about swirl out of focus and memory. Something about a potato… you can't remember. Whatever. WHERE ARE YOU?? Why were you sleeping? Why are you in an unfamiliar place?\n";
      Console.Clear();
      for (int i = 0; i < intro.Length; i++)
      {
        Console.Write(intro[i]);
        Thread.Sleep(2);
      }
      Console.Write("press enter to go to sleep.");
      Console.ReadLine();
      string sleep = "SLEEP.......";
      for (int i = 0; i < sleep.Length; i++)
      {
        Console.Write(sleep[i]);
        Thread.Sleep(600);
      }
      Console.Clear();
      for (int i = 0; i < intro2.Length; i++)
      {
        Console.Write(intro2[i]);
        Thread.Sleep(2);
      }
      string potato = "~~POTATO";
      Console.ReadLine();
      for (int i = 0; i < potato.Length; i++)
      {
        Console.Write(potato[i]);
        Thread.Sleep(500);
      }
      Console.Clear();
      for (int i = 0; i < intro3.Length; i++)
      {
        Console.Write(intro3[i]);
        Thread.Sleep(2);
      }
      Console.ReadLine();
      Help();
      Console.ReadLine();
      Console.WriteLine("\n" + CurrentRoom.Description);
      GetUserInput();
    }

    public void TakeItem(string itemName)
    {
      Item take = CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName.ToLower());
      if (take != null)
      {
        CurrentPlayer.Inventory.Add(take);
        CurrentRoom.Items.Remove(take);
        Console.WriteLine(take.Description);
      }
      else
      {
        Console.WriteLine("Can't take that.");
      }
    }

    public void UseItem(string itemName)
    {
      if (itemName == "marker" && !CurrentRoom.isSolved)
      {
        Console.WriteLine("You walk up to the whiteboard with marker in hand. It reads, scrawled in ancient fonts and inks: ");
        CurrentRoom.AttemptChallenge();
      }
    }
  }
}