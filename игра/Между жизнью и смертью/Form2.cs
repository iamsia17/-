using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Между_жизнью_и_смертью
{
    public partial class game : Form
    {
        Thread th;
        bool up, down, right, left, game_over;
        int score, student_speed, teacher1_speedx, teacher1_speedy, teacher2_speedx, teacher3_speedx, count;

        private void timer1_Tick(object sender, EventArgs e)
        {
            Score1.Text = "Score:" + score;
            if (left == true)
            {
                student.Left = student.Left - student_speed;
                student.Image = Properties.Resources.Student_left;
            }
            if (right == true)
            {
                student.Left = student.Left + student_speed;
                student.Image = Properties.Resources.student_right;
            }
            if (down == true)
            {
                student.Top = student.Top + student_speed;
                student.Image = Properties.Resources.Student_left;
            }
            if (up == true)
            {
                student.Top = student.Top - student_speed;
                student.Image = Properties.Resources.student_right;
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    if ((string)x.Tag == "mark" && x.Visible == true)
                    {
                        if (student.Bounds.IntersectsWith(x.Bounds))
                        {
                            score = score + 1;
                            x.Visible = false;
                        }
                    }
                    if ((string)x.Tag == "wall" || (string)x.Tag == "object")
                    {
                        if (student.Bounds.IntersectsWith(x.Bounds) && left == true)
                            student.Left += 10;
                        if (student.Bounds.IntersectsWith(x.Bounds) && right == true)
                            student.Left -= 10;
                        if (student.Bounds.IntersectsWith(x.Bounds) && up == true)
                            student.Top += 10;
                        if (student.Bounds.IntersectsWith(x.Bounds) && down == true)
                            student.Top -= 10;
                    }
                    if ((string)x.Tag == "teacher")
                    {
                        if (student.Bounds.IntersectsWith(x.Bounds))
                        {
                            count += 1;
                            DontMove();
                            if (count == 1)
                            {
                                heart1.Visible = false;
                                heart4.Visible = true;
                                MessageBox.Show("Упс! Кажется возникли трудности с автоматом;(", "Предупреждение");
                            }
                            if (count == 2)
                            {
                                heart2.Visible = false;
                                heart5.Visible = true;
                                MessageBox.Show("Упс! Кажется возникли трудности с автоматом;(", "Предупреждение");
                            }
                            if (count == 3)
                            {
                                heart3.Visible = false;
                                heart6.Visible = true;
                                MessageBox.Show("ВЫ ОТЧИСЛЕНЫ!", "Конец игры");
                                gameover();
                            }
                        }
                    }
                }
            }
            if (score == 61)
            {
                score += 1;
                DontMove();
                DialogResult res = MessageBox.Show("Поздравляем! Вы получили ТРОЙКУ автоматом! Хотите продолжить?)", "Внимание!", MessageBoxButtons.YesNo);
                if (res == DialogResult.No)
                    gameover();              
            }
            if (score == 76)
            {
                score += 1;
                DontMove();
                DialogResult res = MessageBox.Show("Поздравляем! Вы получили ЧЕТВЕРКУ автоматом! Хотите продолжить?)", "Внимание!", MessageBoxButtons.YesNo);
                if (res == DialogResult.No)
                    gameover();
            }
            if (score == 91)
            {
                score += 1;
                student.Left = 246;
                student.Top = 360;
                MessageBox.Show("Поздравляем! Вы получили ПЯТЕРКУ автоматом! До новых встреч!)", "Конец игры");
                gameover();
            }


            teacher2.Left += teacher2_speedx;
            if (teacher2.Bounds.IntersectsWith(walls1.Bounds) || teacher2.Bounds.IntersectsWith(walls2.Bounds))
                teacher2_speedx = -teacher2_speedx;
            teacher3.Left += teacher3_speedx;
            if (teacher3.Bounds.IntersectsWith(walls3.Bounds) || teacher3.Bounds.IntersectsWith(walls4.Bounds))
                teacher3_speedx = -teacher3_speedx;
        }

        public void DontMove()
        {
            if (left == true)
                left = false;
            if (right == true)
                right = false;
            if (up == true)
                up = false;
            if (down == true)
                down = false;
            student.Left = 246;
            student.Top = 360;
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                left = true;

            if (e.KeyCode == Keys.Right)
                right = true;

            if (e.KeyCode == Keys.Up)
                up = true;

            if (e.KeyCode == Keys.Down)
                down = true;
        }

        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                left = false;

            if (e.KeyCode == Keys.Right)
                right = false;

            if (e.KeyCode == Keys.Up)
                up = false;

            if (e.KeyCode == Keys.Down)
                down = false;
        }

        
        public game()
        {
            InitializeComponent();
            resetgame();
        }

        private void pictureBox38_Click(object sender, EventArgs e)
        {

        }
        private void resetgame()
        {
            count = 0;
            heart1.Visible = true;
            heart2.Visible = true;
            heart3.Visible = true;
            heart4.Visible = false;
            heart5.Visible = false;
            heart6.Visible = false;
            Score1.Text = "Score: 0";
            score = 0;
            teacher1_speedx = 5;
            teacher1_speedy = 5;
            teacher2_speedx = 5;
            teacher3_speedx = 5;
            student_speed = 6;
            game_over = false;

            student.Left = 246;
            student.Top = 360;
            teacher1.Left = 950;
            teacher1.Top = 300;
            teacher2.Left = 302;
            teacher2.Top = 580;
            teacher3.Left = 600;
            teacher3.Top = 195;

            timer1.Start();
        }
        private void gameover()
        {
            game_over = true;
            timer1.Stop();
            this.Close(); 
            th = new Thread(open);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        public void open(object obj)
        {
            Application.Run(new menu());
        }
    }
}
