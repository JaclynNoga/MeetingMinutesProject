using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MeetingMinutesProject
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> typesOfTeams = new List<string>() { "Marketing Team", "All Team", "Administrative Team", "Education Team" };
            Dictionary<string, int> teamMembers = new Dictionary<string, int>();
            teamMembers.Add("Mooney Man",0);
            teamMembers.Add("Tyra Banks",0);
            teamMembers.Add("Kevin Spacey",0);
            teamMembers.Add("Jackie Noga",1);
            teamMembers.Add("Noob Noga",1);
            teamMembers.Add("Senor Senior",1);
            teamMembers.Add("Garret George",2);
            teamMembers.Add("Jordan Sparks",2);
            teamMembers.Add("Mary J. Blige", 2);
            teamMembers.Add("Albert Zooney",3);
            teamMembers.Add("Erica Hernandez",3);
            teamMembers.Add("Georgina London",3);

            Console.WriteLine("Meeting Minutes Management Software\n***************\n");

            do  //loops until user chooses to exit in main menu
            {
                Refresh();
                Console.WriteLine("MAIN MENU\n1. Create Meeting\n2. View Team\n3. Exit");
                int answer = 0; //need to initialize answer in case user enter's a number other than 1,2, or 3
                int.TryParse(Console.ReadLine(), out answer);   //tries to parse user input
                switch (answer)
                {
                    case 1:
                        CreateMeeting(typesOfTeams);    //creates the meeting minutes
                        break;
                    case 2:
                        ViewTeam(typesOfTeams, teamMembers);        //shows the teams
                        break;
                    case 3:
                        Exit();     //exits
                        break;
                    default:
                        Refresh();
                        Console.WriteLine("Try again.");
                        Console.ReadKey();
                        break;
                }
            }
            while (true);
        }
        static void Refresh()   //clears console and rewrites the header
        {
            Console.Clear();   
            Console.WriteLine("Meeting Minutes Management Software\n***************\n");
        }
        static void CreateMeeting(List<string> typesOfTeams)    //writes and reads the meeting minutes
        {
            Refresh();

            StringBuilder sb = new StringBuilder(); //use this stringbuilder object to save all of the minutes text until the end when we write it
            sb.Append("We Can Code IT\r\n50 Public Square\r\nMeeting Minutes\r\n-------------\r\n\r\n");

            Console.WriteLine("Note Taker: ");
            sb.Append("Note Taker: ");
            sb.Append(Console.ReadLine());

            Console.WriteLine("Meeting Leader: ");
            sb.Append("\r\nLeader: ");
            sb.Append(Console.ReadLine());

            Console.WriteLine("Date of Meeting (MMDDYY): ");
            string date = Console.ReadLine();
            sb.AppendLine();
            sb.AppendLine(date);
            string fileName = "Minutes" + date + ".txt";

            Refresh();

            Console.WriteLine("Type of Meeting: ");
            sb.AppendLine(typesOfTeams[SelectTeamType(typesOfTeams)]);      //calls method that allows the user to select a team, then appends it to the stringbuilder

            string anotherTopicAns = "yes";
            while (anotherTopicAns == "yes" || anotherTopicAns == "y")  //asks user for more topics until they enter something other than yes
            {
                Refresh();
                sb.AppendLine();
                Console.WriteLine("Topic: ");
                sb.AppendLine(Console.ReadLine());

                Console.WriteLine("Meeting Notes: ");
                sb.AppendLine(Console.ReadLine());

                Console.WriteLine("Would you like to enter notes for another topic? ");
                anotherTopicAns = Console.ReadLine().ToLower();
            }

            StreamWriter writer = new StreamWriter(fileName);
            writer.Write(sb);   //writes to the file everything just written in sb
            writer.Close();

            Refresh();
            StreamReader reader = new StreamReader(fileName);
            using (reader)
            {
                string line;
                while ((line = reader.ReadLine()) != null)  //while there is text in the file
                {
                    Console.WriteLine(line);    //writes what's in the file
                }
            }
            Console.ReadKey();
        }
        static int SelectTeamType(List<string> typesofTeams)    //displays the types of teams and asks user to select which one
        {
            int tempTeamAns = 0;
            while (tempTeamAns == 0 || tempTeamAns > typesofTeams.Count)    //loops until the user enters one of the number options
            {
                Refresh();
                for (int numb = 1; numb <= (typesofTeams.Count); numb++)
                {
                    Console.WriteLine("{0}. {1}", numb, typesofTeams[numb - 1]);
                }
                int.TryParse(Console.ReadLine(), out tempTeamAns);  //tries to parse the user's input
            }
            return tempTeamAns-1;   //need to subtract 1 because the list's index starts at 0 and i asked user for a list starting at 1
        }
        static void ViewTeam(List<string> typesOfTeams, Dictionary<string, int> teamMembers)    //shows either all of the team members or one specific team
        {
            Refresh();

            Console.WriteLine("1. View a Team\n2. View Company");
            int ans = int.Parse(Console.ReadLine());
            Refresh();
            if (ans == 1)   //to view one team
            {
                int teamToPrintAns = SelectTeamType(typesOfTeams);  //runs method to select a team
                Refresh();
                foreach (KeyValuePair<string, int> temp in teamMembers) //for each pair in the dictionary containing team members
                {
                    if (temp.Value == teamToPrintAns)   //if the index is equal to the index of the selected team
                    {
                        Console.WriteLine(temp.Key);    //prints the name
                    }
                }
            }
            else if (ans == 2)  //to view whole company
            {
                foreach (KeyValuePair<string,int> temp in teamMembers)  //runs through every name in the dictionary containing team members
                {
                    Console.WriteLine("{0} ({1})",temp.Key,typesOfTeams[ temp.Value]);  //prints name and associated team
                }
            }
            else
            {
                Console.WriteLine("Error with input");
            }
            Console.ReadKey();
        }
        static void Exit()
        {
            Refresh();
            Console.WriteLine("Goodbye.");
            Environment.Exit(0);
        }
    }
}
