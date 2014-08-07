using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsInput;
using System.Threading;


namespace nek_spammer
{
    public partial class MainForm : Form
    {
        private bool bc = false;
        Random rn = new Random(DateTime.Now.Millisecond);
        string besedilo;
        string izpisb;
        int steviloPonovitev;
        int stevec;
        int tralala;//stevc za presledke
        int trala; //omejitev hitrosti
        private bool dv= false;
        private bool cd = false;

        private bool isSpamming = false;
        private bool isClosing = false;
        
        public MainForm()


        {
            InitializeComponent();
   

        }

        public  void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            besedilo = textBox1.Text;
            if (bc)
            {
                besedilo = besedilo +"  "+ rn.Next(100, 999)+ rn.Next(100,999);
                
            }
           
            //label1.Text = besedilo;
             
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
            
        }

        public void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            if (checkBox1.Checked)
            {
                
                bc = true;
            }
            else
            {
                bc = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //to lohk zbrism ^^
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            
            steviloPonovitev = (int)numericUpDown1.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            trala = (int)numericUpDown2.Value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (DwmApi.DwmIsCompositionEnabled())
            {
                DwmApi.DwmExtendFrameIntoClientArea(Handle, new DwmApi.MARGINS(0, 60, 0, 0));
            }

            Keys hotKey = Keys.S | Keys.Control | Keys.Shift;
            WindowsShell.RegisterHotKey(this, hotKey);
            Thread.Sleep(1000);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isClosing)
            {
                WindowsShell.UnregisterHotKey(this);
            }

            else
            {
                Hide();
                e.Cancel = true;
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WindowsShell.WM_HOTKEY)
            {
                if (isSpamming)
                {
                    isSpamming = false;
                }

                else
                {
                    isSpamming = true;
                    asyncSpam.RunWorkerAsync();
                }
            }
        }

        private void BeginSpam()
        {
            
        }

        private void asyncSpam_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] sporocila = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            // rnd.Next(sporocila.Length);
            
            for (stevec = 0; stevec < steviloPonovitev; stevec++)
            {
                if (!isSpamming)
                    break;

                if (bc)
                {
                    izpisb = sporocila[rn.Next(sporocila.Length)];
                }

                else
                {
                    izpisb = sporocila[0];
                }

                if (cd)
                {
                    izpisb = izpisb + "   " + rn.Next(100, 999) + rn.Next(100, 999);
                }


                if (dv)
                {
                    InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
                }

                InputSimulator.SimulateTextEntry(izpisb);
                Thread.Sleep(50);
                InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);

                Thread.Sleep(trala * 1000);
                    
            }

            isSpamming = false;
        }

        private void asyncSpam_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void izhodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isClosing = true;
            Close();
        }

        private void nastavitveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                dv = true;
            }
            else
            {
                dv = false;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            isClosing = true;
            Close();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                cd = true;
            }
            else
            {
                cd = false;
            }
        }
    }
}
