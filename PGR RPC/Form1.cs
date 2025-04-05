using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using DiscordRPC.Logging;
using DiscordRPC;
using Button = DiscordRPC.Button;

namespace PGR_RPC
{
    public partial class Form1 : Form
    {
        public DiscordRpcClient client;

        public Form1()
        {
            InitializeComponent();

            // Start the timer.
            timer1.Start();

            // Start PGR Launcher if exists.
            if (System.IO.File.Exists("../launcher.exe")) { Process.Start("cmd.exe", "/c cd .. && start launcher.exe && exit"); }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName("PGR").Any()) // Check if the game is running.
            {
                // Do this window invisble.
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                this.Opacity = 0;

                // Start Discord RPC.
                client = new DiscordRpcClient("1357643690557968504");
                client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };
                client.Initialize();
                client.SetPresence(new RichPresence()
                {
                    Assets = new Assets()
                    {
                        LargeImageKey = "pgr",
                        LargeImageText = "Punishing: Gray Raven",
                        SmallImageKey = "pgr2",
                        SmallImageText = "Kuro Games"
                    },
                    Buttons = new Button[]
                    {
                    new Button() { Label = "PGR Web", Url = "https://pgr.kurogame.net" },
                    new Button() { Label = "Gray Ravens Wiki", Url = "https://grayravens.com/wiki/GRAY_RAVENS" }
                    }
                });

                timer2.Start(); // Start the secondary timer (for check if the game is open).
                timer1.Stop(); // Stop this timer.
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!Process.GetProcessesByName("PGR").Any()) { Application.Exit(); } // If the game is not open, close the application.
        }
    }
}