using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mario
{
    public partial class prvniUroven : Form
    {
        bool doleva, doprava, skok = false;
        int rychlostSkoku = 10;
        int sila = 10;
        int rychlostHrace = 10;
        int score = 0;
        bool maKlic = false;
        int zivoty = 3;
        bool hit = false;
        bool smrt = false;
        
        public prvniUroven()
        {
            InitializeComponent();
        }

        private void timerEvent(object sender, EventArgs e)
        {
            zivoty_label.Text = "Životy: " + zivoty;
            score_label.Text = "Score: " + score;
            hrac.Top += rychlostSkoku;
            hrac.Refresh();
            
            if(!(hrac.Bounds.IntersectsWith(pozadi.Bounds)))
            {
                //******************************
                hrac.Top = pictureBox1.Top - hrac.Height;
                hrac.Left = 200;
                zivoty--;

            }

            if (score >= 100)
            {
                score = 0;
                zivoty++;
                zivoty_label.Text = "Životy: " + zivoty;
                score_label.Text = "Score: " + score;
            }
            
            
            if (zivoty <= 0 && smrt == false)
            {
                smrt = true; 
                MessageBox.Show("Bohužel umřels");
                this.Close();
                               
            }
           

            if (nepritel.Left >= pictureBox3.Left)
            {
                nepritel.Left += -rychlostHrace;
                
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
            //-------------------------------------
            if(skok == true)
            {
                rychlostSkoku = -12;
                sila -= 1;
            }
            else
            {
                rychlostSkoku = 12;
            }
            if (skok == true && sila <= 0)
            {
                skok = false;
                rychlostSkoku = 0;
            }
            //-------------------------------------
            foreach(Control objekt in this.Controls)
            {
                if(!(objekt is PictureBox)) { continue; }

                if((string)objekt.Tag == "platforma")
                {
                    if(hrac.Bounds.IntersectsWith(objekt.Bounds) && skok == false)
                    {
                        sila = 10;
                        hrac.Top = objekt.Top - hrac.Height;
                        rychlostSkoku = 0;
                    }
                    objekt.BringToFront(); 
                }

                else if ((string)objekt.Tag == "mince")
                {
                    if (hrac.Bounds.IntersectsWith(objekt.Bounds))
                    {
                        this.Controls.Remove(objekt);
                        score+=20;
                        score_label.Text = "Score: " + score;
                    }
                }

                else if ((string)objekt.Tag == "klic")
                {
                    if (hrac.Bounds.IntersectsWith(objekt.Bounds))
                    {
                        this.Controls.Remove(objekt);
                        maKlic = true;
                        pictureBox27.BackColor = Color.Lime;
                    }
                }

                else if ((string)objekt.Tag == "dvere" && maKlic == true)
                {
                    if (hrac.Bounds.IntersectsWith(objekt.Bounds))
                    {
                        this.Controls.Remove(objekt);
                        MessageBox.Show("Dokončil jsi úroveň :)");
                        this.Close();
                    }
                }

                else if (hrac.Bounds.IntersectsWith(objekt.Bounds) && (string)objekt.Tag == "nepritel" && hit == false)
                {
                    zivoty--;
                    zivoty_label.Text = "Životy: " + zivoty;
                    hit = true;
                }

                else if (!hrac.Bounds.IntersectsWith(objekt.Bounds) && (string)objekt.Tag == "nepritel")
                {
                    hit = false;
                }
                
            }


        }

        private void prvniUroven_Load(object sender, EventArgs e)
        {

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
