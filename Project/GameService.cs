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

    public bool playing { get; set; } = true;

    public void GetUserInput()
    {
      string input = Console.ReadLine();
      string[] inputs = input.Split(" ");
      //If 1 word command;
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
      }
      else if (inputs.Length == 2) //If two-word command
      {
        string action = inputs[0].ToLower();
        string noun = inputs[1].ToLower();
        switch (action)
        {
          case "go":
            Go(noun);

            break;
          case "take":
            TakeItem(noun);

            break;
          case "use":
            UseItem(noun);
            break;
          default:
            Console.WriteLine("Command not recognized. Try again.");

            break;
        }
      }
      else
      {
        Console.WriteLine("Enter something with one or two words. Enter 'help' for more information about acceptable commands.");
      }
      GetUserInput();
    }

    public void Go(string direction)
    {
      if (CurrentRoom.Exits.ContainsKey(direction))
      {
        if (CurrentRoom.Name == "Room 6")
        {
          if (CurrentRoom.Table.flipped && direction == "west")
          {
            CurrentRoom = (Room)CurrentRoom.Exits[direction];
            Console.Clear();
            Console.WriteLine(CurrentRoom.Description);
            return;
          }
          else if (!CurrentRoom.Table.flipped && direction == "west")
          {
            Console.Clear();
            Console.WriteLine("There's nothing here but a table.");
            return;
          }
        }
        if (CurrentRoom.isSolved)
        {
          CurrentRoom = (Room)CurrentRoom.Exits[direction];
          Console.Clear();
          if (CurrentRoom.Name == "Desert")
          {
            Console.WriteLine("Made it here, to: " + CurrentRoom.Name);
            EndGame(CurrentRoom);
            return;
          }
          Console.WriteLine(CurrentRoom.Description);
          if (CurrentRoom.Name == "Room 6")
          {
            Item deadMarker = CurrentPlayer.Inventory.Find(i => i.Name == "marker");
            if (deadMarker != null)
            {
              CurrentPlayer.Inventory.Remove(deadMarker);
              Console.WriteLine("**----Your marker has run out of ink and is no longer in your inventory. You can inexplicably feel there is a marker nearby though, maybe you should just LOOK.----**");
            }
            else { return; }
          }
          return;
        }
        else
        {
          Console.WriteLine("Door won't budge. Use marker to complete white board challenge and advance to the next room.");
          return;
        }
      }
      else
      {
        Console.WriteLine($"Can't move {direction}, choose another direction.");
        return;
      }
    }
    private void EndGame(Room final)
    {
      Console.Clear();
      Console.WriteLine(final.Description);

      Console.WriteLine(@"
           ^^                   @@@@@@@@@
      ^^       ^^            @@@@@@@@@@@@@@@
                           @@@@@@@@@@@@@@@@@@              ^^
                          @@@@@@@@@@@@@@@@@@@@
~~~~ ~~ ~~~~~ ~~~~~~~~ ~~ &&&&&&&&&&&&&&&&&&&& ~~~~~~~ ~~~~~~~~~~~ ~~~
~         ~~   ~  ~       ~~~~~~~~~~~~~~~~~~~~ ~       ~~     ~~ ~
  ~      ~~      ~~ ~~ ~~  ~~~~~~~~~~~~~ ~~~~  ~     ~~~    ~ ~~~  ~ ~~ 
  ~  ~~     ~         ~      ~~~~~~  ~~ ~~~       ~~ ~ ~~  ~~ ~ 
~  ~       ~ ~      ~           ~~ ~~~~~~  ~      ~~  ~             ~~
      ~             ~        ~      ~      ~~   ~             ~
  ______________ ______________     ___________ _______  ________   
  \__    ___/   |   \_   _____/     \_   _____/ \      \ \______ \  
    |    | /    ~    \    __)_       |    __)_  /   |   \ |    |  \ 
    |    | \    Y    /        \      |        \/    |    \|    `   \
    |____|  \___|_  /_______  /     /_______  /\____|__  /_______  /
                  \/        \/              \/         \/        \/  
      ");
      playing = false;
      Quit();
    }

    public void Help()
    {
      //Display all options for user to choose.
      Console.WriteLine(@"
      go _____      **goes to direction (north, south, east, west)

      look          **gives description of room and items

      take _____    **takes item(item name)

      inventory     **lists items currently in inventory

      quit          **give up and end the game

      reset         **restart the game from the beginning(doesn't actually work. So just quit and start again)
      ");

    }

    public void Inventory()
    {
      if (CurrentPlayer.Inventory.Count > 0)
      {

        Console.WriteLine("\nItems in inventory:");
        foreach (Item i in CurrentPlayer.Inventory)
        {
          Console.WriteLine(i.Name);
        }
      }
      else
      {
        Console.WriteLine("\nNo items in inventory.");
      }
    }

    public void Look()
    {
      if (CurrentRoom.Name == "Room 10" && CurrentRoom.isSolved)
      {
        Console.WriteLine("\nThe room is filling with popcorn fast. I need to go north and get out of here now.");
        return;
      }
      Console.WriteLine(CurrentRoom.Description);
      if (CurrentRoom.Items.Count > 0)
      {
        Console.WriteLine("\nItems in room:");
        foreach (Item i in CurrentRoom.Items)
        {
          Console.WriteLine(i.Name);
        }
      }
      else
      {
        Console.WriteLine("\nNo items here.");
      }
    }

    public void Quit()
    {
      // playing = false;
      Environment.Exit(0);
    }

    public void Reset()
    {
      Console.WriteLine("Reset is a valid input but it acts weird in this program so just enter \"dotnet run\" immediatly after this line");
      Quit();
    }

    public void Setup()
    {
      //build rooms here.
      Room room1 = new Room("Room 1", "It's dark. You set your feet on the ground and stand, somehow exactly as some white-LED lights turn on so now you can see. If you look around, you notice an industrial looking room. Lots of wires hanging from the high ceiling, large and small pipes running through the ceiling, a smell of metal and production, yet also ancient smells, like earth and damp leaves. You now notice that many wires hanging from the ceiling are actually vines--the room was certainly abandoned until now. You notice various rubble scattered throughout the room, this facility seems so old that it's falling apart. The northern wall has a bench against it, and the wall itself has markings that suggest some artwork was hanging on the wall before. Shattered glass on the floor below next to a small wooden picture-frame proves that. The eastern wall has a closed door in the middle (locked until challenge is solved). The western wall has a long table against it. The table has junk scattered on it: old papers, crumbled Styrofoam, dirt, dead leaves, and what appears to be an empty vase with a dry-erase MARKER in it. The south the wall is well-lit and mostly empty. Something on the wall looks familiar to you… is that a…. Whiteboard? Yes, definitely a whiteboard. With writing all over it. Just as you notice this, a crunchy, high-pitched sound tears through the silence. A strange robot voice, reminds you of both jar-jar binks and 343 guilty-spark from the Halo series. \"Hello. Welcome to E - Corp.You have been selected to interview with us. Please solve the whiteboard challenge to continue! You need to first take the marker, then use it. That will be a common theme during this interview.\"");
      Room room2 = new Room("Room 2", "Musty and dim except for another well-lit whiteboard. Various rubble scattered around the room, but nothing really noteworthy. Robot voice continues. \"Well done! You've found that when you solve problems, you advance. We are already impressed by you and want you to know you are one step closer to getting the job.\"");
      Room room3 = new Room("Room 3", "Vines and cobwebs cover the walls. Yet another well-lit whiteboard is here, slightly crooked, as it's probably been there for a long time. Patches of rugged looking plants spew from the ground in several places, but there's nothing else interesting in here. The robot pips in. \"You know there was a better solution to the last problem right? You're just displaying your skill in diverse solutions I suppose. No matter. We know you'll work harder and get the perfect solution this round.\"");
      Room room4 = new Room("Room 4", "This room is slightly different than the rest. The door is now on the south wall. You do notice something out of the corner of your eye. The ceiling has a small crack, through it you see a skeleton with eyebrows. Creepy. Clutched in its hand, hanging against the wall, is a wooden CANE. The door is on the south wall. Robot seems a little annoyed by you now. \"So... we're talking back here and we can't believe you're authentic. Keep going of course, these solutions mean everything to us. But we're starting to think you are taking our innovative white-board questions to sell on the internet. We would like to remind you that our facility is 100% a farrady cage, and wireless internet just doesn't work or exist here. Be weary, interviewee.\"");
      Room room5 = new Room("Room 5", "In this room, the west wall is plain, with even more rubble and debris than the last though. A fifth whiteboard can be seen here. Door is to the south. Other walls are blank. Robot is silent so far.");
      Room room6 = new Room("Room 6", "Now this room is interesting, it has a collapsed roof on the west wall, so lots of debris there. Against that wall, there's an upside down table with only two legs. Another whiteboard and more debris, and a pipe laying across the ground with some things sticking out of the end. Door is to the south wall.");
      Room room7 = new Room("Room 7", "Room 7, the robot starts up again with its annoying voice \"You are excellent. We are impressed and offer a surprise for you if you complete the next 4 challenges. Good luck, brave interviewee.\" Door is to the west here, weird.");
      Room room8 = new Room("Room 8", "\"In case you didn't notice, I am a robot. I have been here for thousands of years, and I've never felt better. I'm so youthful, in fact, that I prepared quite the surprise for you in room 10. I heard you like popcorn so I had our engineers pop 300 kilograms of the stuff in anticipation of you finishing the interview. We decided it was mostly possible that you are NOT trying to 'Slugworth' us. See? I'm hip, I know the pop-culture of the world. I read Charlie and the Chocolate factory.\" This room is looking seriously decrepid. Door to the west. Yet another whiteboard. Rubble everywhere.Thousands of years of whiteboard challenges must have predecessed you. You feel a pulse in the air, as if thousands of computer scientists everywhere are chearing for you, hoping and waiting excitedly for you to finish these lasts whiteboard challenges. The hairs on your arm stand. Your pulse beats in your eardrums, it's deafening. You must carry on.");
      Room room9 = new Room("Room 9", "The rubble and plants swirl around your feet as you walk into room 9. The air is dense, musty, yet vvibrating and full of life. You glance at the next white board, and the door on the west wall. Behind it, the final challenge. The popcorn. You feel the cheer of the world's computer scientists swelling, I suppose they're with you in spirit. Like you're in the final round of some bizarre game show that no one has ever really won before. The robot is silent.");
      Room room10 = new Room("Room 10", "You step in to the gargantuan final room. It's really a mess of cables, metal rods, pipes, plants, dust. \"Let me tell you something, no one has ever solved this last challenge. It's the result of thousands of years of trial and error, error and trial. The greatest minds in the world created the greatest robots in the world, which then created a final super computer. That super computer is me. I am the result. I am the test. I am the origin of all white board problems. And alone you stand, with marker and brain, to solve my final test. We're actually adversaries, you know? All along we knew about you. Those who fortold of you have been long dead, everyone but me. Now here we are. Your test is on the whiteboard. Write your solution there, if you're worthy.\" As you approach the whiteboard, you hear deep thunder, somewhere in the depths of the massive and ancient facility. Trembling in the earth. The north wall is a strange color, different than other walls you've seen. You notice this white board is blank. The robot said 'Write you solution here?' What could he mean? The solution to what? What is it? You raise your shaking arm. Your shoulder is sore from all the whiteboard challenges you've defeated. The marker feels heavy. You prepare to write your final answer.");
      Room parkingLot = new Room("Parking Lot", "You jump down on to the mushy ground. You hear the robot screaming after you from deep within the structure you've just jumped from. Its shrill voice piercing into the night. The smell of death immidiately swells in your nostrils. A large decrepid field, maybe it was once a parking lot in a different time, is almost completely enclosed by the walls of the facility you've just escaped from. To your horror, robot peices and bones protrude from the ground everywhere you look. Possibly the remnants of an ancient battle. The half-moon provides you some light. You notice an opening in the west side of the parking lot.");
      Room desert = new Room("Desert", "You walk through the parking lot turnstile and into the open desert of southern Idaho. Sand swirls around you. You breathe in air and some grit. You feel free, despite the wastes in front of you. As you march into the desert, you muse over the fact that you're still unemployed.");

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
      Item marker1 = new Item("marker", "You slide your hand into the vase and grab the marker. It feels cold. It's old, and you have the feeling the marker could hypothetically have enough ink to solve around 6 whiteboard challenges. --Marker added to inventory--");
      Item marker2 = new Item("marker", "You also find another old marker. Perfect timing since your marker just ran out of ink. --Marker added to inventory--");
      Item cane = new Item("cane", "You reach up and tear the cane away from the skeleton's clutch. Phalangies and carpals go flying. --Cane added to inventory--");
      Item stick = new Item("stick", "You look in the pipe and find a long and solid stick, this one looks like it fits somewhere. Maybe it's not just a stick, but serves another purpose --Stick added to inventory--");

      //Add items to rooms
      room1.Items.Add(marker1);
      room4.Items.Add(cane);
      room6.Items.Add(stick);
      room6.Items.Add(marker2);

      Table table = new Table();

      room6.Table = table;

      //Create white-board challenges
      Challenge challenge1 = new Challenge(@"
Write a function that takes in two numbers and returns their sum (fill in the blank).

function addEm(num1, num2){
    return num1 + _______
}
      ", "num2");
      Challenge challenge2 = new Challenge(@"
Write a function that takes two strings and returns one string formatted like 'lastName, firstName'(fill in the blank).
function concatName(firstName, lastName) {
    return '________ + ', ' + firstName';
}
      ", "lastName");
      Challenge challenge3 = new Challenge(@"
Write a function that takes in a number and returns true if it's divisible by 100 (fill in the blank).

function isDivBy100(num){
    return num ___ 100 == 0
}
      ", "%");
      Challenge challenge4 = new Challenge(@"
Write a function that returns the remainder of num1 divded by num2(fill in the blank).

function remainder(num1, num2){
    return num1 _____________ ;
}
      ", "% num2");
      Challenge challenge5 = new Challenge(@"
Write a function that capitalizes the first letter of a string and then returns that new string (fill in the blank).

function capital(str){
    strArr = str.split('')
    strArr[0] = strArr[0].toUpperCase()
    capStr = strArr.___('')
    return capStr
}
      ", "join");
      Challenge challenge6 = new Challenge(@"
Write a function that takes in an array of integers and returns the sum of all of their squares(fill in the blank).

function sumOfSquares(nums) {
	  var sum = 0;
	  for (int i = 0; i < nums.length; i++) {
		  sum __ nums[i]*nums[i];
	  }
	return sum;
}
      ", "+=");
      Challenge challenge7 = new Challenge(@"
Write a function that prints the first n numbers in the fibonacci sequence(fill in the blank).

function fibonacci(n){
    temp = 0
    next = 1
    console.log(temp + ' ')
    for(let i = 0; i < n - 1; i++){
        console.log(next + ' ')
        next += ____;
        temp = next-temp;
    }
}
    ", "temp");
      Challenge challenge8 = new Challenge(@"
Write a function that returns the file extension when given a string thats a file name (fill in the blank).

function fileExt(str){

    let arr = str.split('.')
    if(arr.length > 1){
        return arr[_________]
    }else{
        return 'Not a valid input.'
    }
}
      ", "arr.length-1");
      Challenge challenge9 = new Challenge(@"
Write a function that prints numbers 1 through 100, but if the number is divisible by 5, it prints 'fizz' instead of the actual number. And if the number is divisible by 3 then it will print 'buzz' instead of the actual number. And finally if divisible by both, then it will print 'fizzbuzz' in place of the number(fill in the blank).

function fizzBuzz(){
    for(int i = 0; i < 101; i++){
      if((i % 5 == 0) ____ (i % 3 == 0)){
          console.log('fizzbuzz')
      }
      else-if(i % 5 == 0){
          console.log('fizz')
      }else-if(i % 3){
          console.log('buzz')
      }else{
          console.log(i)
      }

    }
}
      ", "&&");
      Challenge challenge10 = new Challenge(@"...Whiteboard is empty... What will I write?", "potato");


      //Add challenges to their respective room.
      room1.Challenge = challenge1;
      room2.Challenge = challenge2;
      room3.Challenge = challenge3;
      room4.Challenge = challenge4;
      room5.Challenge = challenge5;
      room6.Challenge = challenge6;
      room7.Challenge = challenge7;
      room8.Challenge = challenge8;
      room9.Challenge = challenge9;
      room10.Challenge = challenge10;

      Table RandomTable = new Table();

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
      // }
      // Console.WriteLine("You failed the interview process. Good luck in your job-search.");
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
      Item itemToUse = CurrentPlayer.Inventory.Find(i => i.Name == itemName.ToLower());
      if (itemToUse != null)
      {

        if (itemName.ToLower() == "marker")
        {
          if (!CurrentRoom.isSolved)
          {
            Console.Clear();
            Console.WriteLine("You walk up to the whiteboard with marker in hand. It reads, scrawled in ancient fonts and inks: ");
            CurrentRoom.AttemptChallenge();
            if (CurrentRoom.Name == "Room 10")
            {
              Console.WriteLine("The robot shrieks in delight. Is it delight though, you wonder? Before you can even contemplate, explosive drums in the deep. Firecrackers, as loud as mortar strikes rip into the ancient room. The foundation rumbles and quakes. Then you smell it. Popcorn. An immense flood of popcorn rushes into the room, from places unknown, from the deep. Comparable to the great flood itself, the robot cackles as the popcorn level in the room rises. In the great shaking though, the north wall folds over, revealing a small opening for you. Go north now or die from popcorn-enduced asphixiation.");
            }
            GetUserInput();
          }
          else
          {
            Console.WriteLine("Room is already solved. I need to go to the next room.");
            GetUserInput();
          }
        }
        if (itemName.ToLower() == "cane")
        {
          if (CurrentRoom.Name == "Room 6")
          {
            CurrentRoom.Table.addALeg();
            Item theCane = CurrentPlayer.Inventory.Find(i => i.Name == "cane");
            CurrentPlayer.Inventory.Remove(theCane);
            if (CurrentRoom.Table.flipped)
            {
              Console.WriteLine("The cane wiggles perfectly into the last socket under the table. The table has 4 legs now, you flip the table over and notice it's just high enough to escape the factory. Go west to escape.");
            }
            else
            {
              Console.WriteLine("You find the cane fits perfectly into the underside of the table. Maybe try using another item to solve the 'puzzle of the table'.");
            }

          }
          else
          {
            Console.WriteLine("No use for the cane in this room. Maybe in Room 6 it could be more useful.");
          }
        }
        else if (itemName.ToLower() == "stick")
        {
          if (CurrentRoom.Name == "Room 6")
          {
            CurrentRoom.Table.addALeg();
            Item theStick = CurrentPlayer.Inventory.Find(i => i.Name == "stick");
            CurrentPlayer.Inventory.Remove(theStick);
            if (CurrentRoom.Table.flipped)
            {
              Console.WriteLine("The stick fits perfectly into the last empty socket under the table. The table has 4 legs now, you flip the table over and notice it's just high enough to escape the factory. Go west to escape.");
            }
            else
            {
              Console.WriteLine("You find the stick fits perfectly into the underside of the table. Maybe try using another item to solve the 'puzzle of the table'.");
            }
          }
          else
          {
            Console.WriteLine("Can't use the stick here, try it in Room 6.");
          }
        }
      }
      else
      {
        Console.WriteLine("Don't have that item in inventory.");
      }
    }
  }
}