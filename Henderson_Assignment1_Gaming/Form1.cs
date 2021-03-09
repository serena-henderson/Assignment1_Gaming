using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Henderson_Assignment1_Gaming
{

    public partial class Form1 : Form
    {
        //3-8-21 SH NEW 6L: Create class for the gamer
        class Gamer
        {
            public int GamerID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        //3-8-21 SH NEW 7L: Create class info for the console and hours
        class GameInfo
        {
            public int InfoID { get; set; }
            public string Console { get; set; }
            public string Hours { get; set; }
            public int GamerID { get; set; }
        }

        //3-8-21 SH NEW : create 2 lists
        private List<Gamer> gamers = new List<Gamer>();
        private List<GameInfo> info = new List<GameInfo>();

        private int gamerID, infoID;

        public Form1()
        {
            InitializeComponent();
        }

        private void gamerSub_Click(object sender, EventArgs e)
        {
            //3-8-21 SH NEW 5L: Error Handling for tag
            if (string.IsNullOrEmpty(gamerTag.Text.Trim()))
            {
                output.Text = "Please enter the gamer tag.";
                return;
            }

            //3-8-21 SH NEW 2L: Separates between first and last names
            gamerTag.Text.Trim();
            string[] tags = gamerTag.Text.Split(' ');

            Gamer gamer = new Gamer();

            //3-8-21 SH NEW 1L: Each tag gets a different ID
            gamer.GamerID = gamerID++;

            //3-8-21 SH NEW 1L: first name is inserted
            gamer.FirstName = tags[0];

            //3-8-21 SH NEW 8L: tracks the Last name
            if (tags.Count() == 1)
            {
                gamer.LastName = string.Empty;
            }
            else
            {
                gamer.LastName = tags[tags.Count() - 1];
            }

            //3-8-21 SH NEW 3L: output
            gamers.Add(gamer);

            output.Text = "The saved tag is " + gamerTag.Text + "...";

        }

        private void conHourSub_Click(object sender, EventArgs e)
        {
            //3-8-21 SH NEW 5L: Error Handling for tag
            if (gamers.Count == 0 || string.IsNullOrEmpty(gamers[gamers.Count - 1].FirstName))
            {
                output.Text = "Please enter the gamer tag!";
                console.Clear();
                hoursPlayed.Clear();
                return;
            }

            //3-8-21 SH NEW 5L: Error Handling for console
            if (string.IsNullOrEmpty(console.Text.Trim()))
            {
                output.Text = "Please enter the console name.";
                return;
            }

            //3-8-21 SH NEW 5L: Error Handling for hours
            if (string.IsNullOrEmpty(hoursPlayed.Text.Trim()))
            {
                output.Text = "Please enter the hours played.";
                return;
            }

            //3-8-21 SH NEW 5L: Error Handling for hours
            decimal spent = 0;
            if (decimal.TryParse(hoursPlayed.Text.Trim(), out spent) == false)
            {
                output.Text = "Please enter a number.";
                hoursPlayed.Clear();
                return;
            }

            GameInfo gameInfo = new GameInfo();

            //3-8-21 SH NEW 5L: assigning ID
            gameInfo.InfoID = infoID++;
            gameInfo.Console = console.Text;
            gameInfo.Hours = hoursPlayed.Text;
            gameInfo.GamerID = gamers.Last().GamerID;

            //3-8-21 SH NEW 5L: output
            info.Add(gameInfo);

            output.Text = "The console is " + console.Text + " and the hours played is " + hoursPlayed.Text + "...";

            //3-8-21 SH NEW 5L: clear all fields
            gamerTag.Clear();
            console.Clear();
            hoursPlayed.Clear();

        }

        private void searchSub_Click(object sender, EventArgs e)
        {
            string searchFull = search.Text.Trim();

            //3-8-21 SH NEW 14L: Combined the lists to search everything
            var query = info.Join(
                gamers,
                info => info.InfoID,
                gamer => gamer.GamerID,
                (info, gamer) => new
                {
                    FirstName = gamer.FirstName,
                    LastName = gamer.LastName,
                    Console = info.Console,
                    Hours = info.Hours,
                }).Where(s => s.FirstName == searchFull ||
                              s.LastName == searchFull ||
                              s.Console == searchFull ||
                              s.Hours == searchFull);

            output.Clear();

            //3-8-21 SH NEW 6L: output
            foreach (var result in query)
            {
                output.AppendText("Name: " + result.FirstName +
                    result.LastName + "  -  Console: " + result.Console +
                    "  -  Hours: " + int.Parse(result.Hours) + Environment.NewLine);
            }

            search.Clear();
        }

        private void startsSub_Click(object sender, EventArgs e)
        {
            string searchStarts = startsWith.Text.Trim();

            //3-8-21 SH NEW 14L: Combined the lists to search everything
            var query = info.Join(
                gamers,
                info => info.InfoID,
                gamer => gamer.GamerID,
                (info, gamer) => new
                {
                    FirstName = gamer.FirstName,
                    LastName = gamer.LastName,
                    Console = info.Console,
                    Hours = info.Hours,
                }).Where(s => s.FirstName.StartsWith(searchStarts) == true ||
                              s.LastName.StartsWith(searchStarts) == true ||
                              s.Console.StartsWith(searchStarts) == true ||
                              s.Hours.StartsWith(searchStarts) == true);

            output.Clear();

            //3-8-21 SH NEW 6L: output
            foreach (var result in query)
            {
                output.AppendText("Name: " + result.FirstName +
                    result.LastName + "  -  Console: " + result.Console +
                    "  -  Hours: " + int.Parse(result.Hours) + Environment.NewLine);
            }

            startsWith.Clear();
        }

        private void endsSub_Click(object sender, EventArgs e)
        {
            string searchEnds = endsWith.Text.Trim();

            //3-8-21 SH NEW 14L: Combined the lists to search everything
            var query = info.Join(
                gamers,
                info => info.InfoID,
                gamer => gamer.GamerID,
                (info, gamer) => new
                {
                    FirstName = gamer.FirstName,
                    LastName = gamer.LastName,
                    Console = info.Console,
                    Hours = info.Hours,
                }).Where(s => s.FirstName.EndsWith(searchEnds) == true ||
                              s.LastName.EndsWith(searchEnds) == true ||
                              s.Console.EndsWith(searchEnds) == true ||
                              s.Hours.EndsWith(searchEnds) == true);

            output.Clear();

            //3-8-21 SH NEW 6L: output
            foreach (var result in query)
            {
                output.AppendText("Name: " + result.FirstName +
                    result.LastName + "  -  Console: " + result.Console +
                    "  -  Hours: " + int.Parse(result.Hours) + Environment.NewLine);
            }

            endsWith.Clear();
        }
    }
}
