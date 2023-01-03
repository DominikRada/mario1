using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace mario
{
    public partial class prvniUroven : Form
    {
        bool doleva, doprava, skok = false;
        int rychlostSkoku = 10;
        int sila = 8;
        int rychlostHrace = 10;
        int score = 0;
        bool maKlic = false;
        int zivoty = 1;
        bool hit = false;

        public prvniUroven()
        {
            InitializeComponent();
        }

        private void timerEvent(object sender, EventArgs e)
        {

            hrac.Top += rychlostSkoku;
            hrac.Refresh();


            if (score >= 100)
            {
                score = 0;
                zivoty++;
                zivoty_label.Text = "Životy: " + zivoty;
                score_label.Text = "Score: " + score;
            }

            /*if (hrac.Top < this.Bottom)
            {
                zivoty--;
                
                if (zivoty <= 0)
                {
                    MessageBox.Show("Umřels :(");
                    Close();
                }
            }*/

            if (nepritel.Left >= pictureBox3.Left)
            {
                nepritel.Left += -rychlostHrace;
                nepritel.BringToFront();
            }
            else
            {
                nepritel.Left = pictureBox3.Right;
            }

            if (doleva == true && hrac.Left > 10)
            {
                hrac.Left -= rychlostHrace;
            }
            if (doprava == true && hrac.Left + (hrac.Width + 10) < this.ClientSize.Width)
            {
                hrac.Left += rychlostHrace;
            }

            if(skok == true)
            {
                rychlostSkoku = -12;
                sila -= 1;
            }
            else
            {
                rychlostSkoku = 12;
            }
            if (skok == true && sila < 0)
            {
                skok = false;
                rychlostSkoku = 0;
            }

            foreach(Control objekt in this.Controls)
            {
                if(objekt is PictureBox && (string)objekt.Tag == "platforma")
                {
                    if(hrac.Bounds.IntersectsWith(objekt.Bounds) && skok == false)
                    {
                        sila = 8;
                        hrac.Top = objekt.Top - hrac.Height;
                        rychlostSkoku = 0;
                    }
                    objekt.BringToFront(); 
                }

                if (objekt is PictureBox && (string)objekt.Tag == "mince")
                {
                    if (hrac.Bounds.IntersectsWith(objekt.Bounds))
                    {
                        this.Controls.Remove(objekt);
                        score+=20;
                        score_label.Text = "Score: " + score;
                    }
                }

                if (objekt is PictureBox && (string)objekt.Tag == "klic")
                {
                    if (hrac.Bounds.IntersectsWith(objekt.Bounds))
                    {
                        this.Controls.Remove(objekt);
                        maKlic = true;
                        pictureBox27.BackColor = Color.Lime;
                    }
                }

                if (objekt is PictureBox && (string)objekt.Tag == "dvere" && maKlic == true)
                {
                    if (hrac.Bounds.IntersectsWith(objekt.Bounds))
                    {
                        this.Controls.Remove(objekt);
                        MessageBox.Show("Dokončil jsi úroveň :)");
                        this.Visible = false;
                        this.Close();
                        Score s = new Score(score);
                        s.ShowDialog();
                        
                    }
                }

                if (objekt is PictureBox && (string)objekt.Tag == "nepritel" && hit == false)
                {
                    if (hrac.Bounds.IntersectsWith(objekt.Bounds))
                    {
                        hit = true;
                        zivoty--;
                        zivoty_label.Text = "Životy: " + zivoty;
                    }
                    
                }
                hit = false;
            }


        }

        private void KlavesaStisknuta(object sender, KeyEventArgs e)
        {
           switch(e.KeyCode)
            {
                case Keys.Left: doleva = true; break;
                case Keys.Right: doprava = true; break;
                case Keys.Space:
                    if (skok == false)
                    {
                        skok = true;
                        //MessageBox.Show("skok");
                        break;
                    }
                    break;
            }
        }

        private void KlavesaNahore(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left: doleva = false; break;
                case Keys.Right: doprava = false; break;
            }
            if (skok == true)
            {
                skok = false;             
            }

        }

        
    }
}
