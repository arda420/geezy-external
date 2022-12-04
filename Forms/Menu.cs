using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using elmencho.Cheats;
using elmencho.Classes;
using elmencho.Utilities;

namespace elmencho
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            if (Main.RunStartup())
            {
                OffsetUpdater.UpdateOffsets();
                #region Start Threads
                // found the process and everything, lets start our cheats in a new thread
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    CheckMenu();
                }).Start();

                Tools.InitializeGlobals();

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Bunnyhop.Run();
                }).Start();

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Visuals v = new Visuals();
                    v.Run();
                }).Start();
                #endregion
            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            string rand = null;

            Random random = new Random();
            for (int i = 0; i < 16; i++)
            {
                rand += Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))).ToString();
            }

            this.Text = rand;

            enableESP.Checked = Properties.Settings.Default.esp;
            enableBHOP.Checked = Properties.Settings.Default.bhop;
            snapLines.Checked = Properties.Settings.Default.snapline;
            snapLinesColor.FillColor = Properties.Settings.Default.snaplineColor;
            healthBar.Checked = Properties.Settings.Default.healthbar;
            outlineBox.Checked = Properties.Settings.Default.outline;
            outlineColor.FillColor = Properties.Settings.Default.outlineColor;
            enemyColor.FillColor = Properties.Settings.Default.enemy;
            teamColor.FillColor = Properties.Settings.Default.team;
            getBoxType.SelectedItem = Properties.Settings.Default.boxtype;
            roundedRadius.Value = Properties.Settings.Default.radius;
            label14.Text = Properties.Settings.Default.radius.ToString();
            ftt.Value = Properties.Settings.Default.ftt;
            ft.Text = Properties.Settings.Default.ftt.ToString();
            Watermark.Checked = Properties.Settings.Default.watermark;
            wtColor.FillColor = Properties.Settings.Default.watermarkColor;
            wtColor2.FillColor = Properties.Settings.Default.watermarkColor2;
            bold.Checked = Properties.Settings.Default.bold;
            italic.Checked = Properties.Settings.Default.italic;
            customtext.Text = Properties.Settings.Default.text;
            TopMost = true; // make this hover over the game, can remove if you want
        }

        public void CheckMenu()
        {
            // Here we make the main variables equal to what our menu checkboxes say
            while (true)
            {
                Main.S.BunnyhopEnabled = enableBHOP.Checked;
                Main.S.ESP = enableESP.Checked;
                if ((Memory.GetAsyncKeyState(Keys.VK_INSERT) & 1) > 0)
                    Visible = !Visible;

                Thread.Sleep(50); // Greatly reduces cpu usage
            }
        }

        //////////////////////////////////////////////////////

        private void snapLines_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.snapline = snapLines.Checked;
            Properties.Settings.Default.Save();
        }

        private void snapLinesColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            var result = cd.ShowDialog();
            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.snaplineColor = cd.Color;
                snapLines.BackColor = cd.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void healthBar_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.healthbar = healthBar.Checked;
            Properties.Settings.Default.Save();
        }

        private void outlineBox_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.outline = outlineBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void outlineColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            var result = cd.ShowDialog();
            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.outlineColor = cd.Color;
                outlineColor.BackColor = cd.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void enemyColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            var result = cd.ShowDialog();
            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.enemy = cd.Color;
                enemyColor.BackColor = cd.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void teamColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            var result = cd.ShowDialog();
            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.team = cd.Color;
                teamColor.BackColor = cd.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void getBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.boxtype = getBoxType.Text;

            if (getBoxType.Text == "Rounded")
            {
                label13.Visible = true;
                label14.Visible = true;
                roundedRadius.Visible = true;
            }
            else
            {
                label13.Visible = false;
                label14.Visible = false;
                roundedRadius.Visible = false;
            }
            Properties.Settings.Default.Save();
        }

        private void roundedRadius_Scroll(object sender, ScrollEventArgs e)
        {
            label14.Text = roundedRadius.Value.ToString();
            Properties.Settings.Default.radius = roundedRadius.Value;
            Properties.Settings.Default.Save();
        }

        private void Watermark_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.watermark = Watermark.Checked;
            Properties.Settings.Default.Save();
        }

        private void wtColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            var result = cd.ShowDialog();
            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.watermarkColor = cd.Color;
                wtColor.BackColor = cd.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void wtColor2_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            var result = cd.ShowDialog();
            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.watermarkColor2 = cd.Color;
                wtColor2.BackColor = cd.Color;
                Properties.Settings.Default.Save();
            }
        }

        private void enableBHOP_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.bhop = enableBHOP.Checked;
            Properties.Settings.Default.Save();
        }

        private void enableESP_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.esp = enableESP.Checked;
            Properties.Settings.Default.Save();
        }

        private void bold_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.bold = bold.Checked;
            Properties.Settings.Default.Save();
        }

        private void italic_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.italic = italic.Checked;
            Properties.Settings.Default.Save();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.text = customtext.Text;
            Properties.Settings.Default.Save();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Menu_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/discordurl");
        }

        private void ftt_Scroll(object sender, ScrollEventArgs e)
        {
            ft.Text = ftt.Value.ToString();
            Properties.Settings.Default.ftt = ftt.Value;
            Properties.Settings.Default.Save();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
    }
}
