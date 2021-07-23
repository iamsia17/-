using System;
using System.Threading;
using System.Windows.Forms;

namespace Между_жизнью_и_смертью
{
    public partial class game : Form
    {
        Thread th;
        bool up, down, right, left, game_over;
        int score, student_speed, teacher1_speedx, teacher2_speedx, teacher3_speedx, count;

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
                                Gameover();
                            }
                        }
                    }

                    if ((string)x.Tag == "doorA" && x.Visible == true)
                    {
                        if (student.Bounds.IntersectsWith(x.Bounds))
                        {
                            x.Visible = false;
                            teacher2.Visible = true;
                        }
                    }

                    if ((string)x.Tag == "doorB" && x.Visible == true)
                    {
                        if (student.Bounds.IntersectsWith(x.Bounds))
                        {
                            x.Visible = false;
                            teacher3.Visible = true;
                        }
                    }

                    if ((string)x.Tag == "doorC" && x.Visible == true)
                    {
                        if (student.Bounds.IntersectsWith(x.Bounds))
                        {
                            x.Visible = false;
                            teacher1.Visible = true;
                        }
                    }
                }
            }
            if (score==61)
            {
                DontMove();
                DialogResult message1 = MessageBox.Show("Поздравляем! Вы получили ТРОЙКУ автоматом. \nЕсли продолжите - получите +5 баллов! \nХотите продолжить?)", "Внимание!", MessageBoxButtons.YesNo);
                if (message1 == DialogResult.No)                
                    Gameover();
                if (message1 == DialogResult.Yes)
                {
                    score += 5;
                    started.Start();
                }
                   
            }
            if (score == 76)
            {
                DontMove();
                DialogResult message2 = MessageBox.Show("Поздравляем! Вы получили ЧЕТВЕРКУ автоматом. \nЕсли продолжите - получите +5 баллов! \nХотите продолжить?)", "Внимание!", MessageBoxButtons.YesNo);
                if (message2 == DialogResult.No)
                    Gameover();
                if (message2 == DialogResult.Yes)
                {
                    score += 5;
                    started.Start();
                }
            }
            if (score == 91)
            {
                DontMove();
                MessageBox.Show("Поздравляем! Вы получили ПЯТЕРКУ автоматом! До новых встреч!)", "Конец игры");              
                Gameover();
            }     

            teacher2.Left += teacher2_speedx;
            if (teacher2.Bounds.IntersectsWith(walls1.Bounds) || teacher2.Bounds.IntersectsWith(walls2.Bounds))
                teacher2_speedx = -teacher2_speedx;
            teacher3.Left -= teacher3_speedx;
            if (teacher3.Bounds.IntersectsWith(walls3.Bounds) || teacher3.Bounds.IntersectsWith(walls4.Bounds))
                teacher3_speedx = -teacher3_speedx;
            teacher1.Left += teacher1_speedx;
            if (teacher1.Bounds.IntersectsWith(walls7.Bounds) || teacher1.Bounds.IntersectsWith(walls11.Bounds))
                teacher1_speedx = -teacher1_speedx;
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
            Resetgame();
        }

        private void Resetgame()
        {
            count = 0;

            heart1.Visible = true;
            heart2.Visible = true;
            heart3.Visible = true;
            heart4.Visible = false;
            heart5.Visible = false;
            heart6.Visible = false;

            teacher1.Visible = false;
            teacher2.Visible = false;
            teacher3.Visible = false;

            Score1.Text = "Score: 0";
            score = 0;

            teacher1_speedx = 5;
            teacher2_speedx = 5;
            teacher3_speedx = 5;
            student_speed = 6;

            game_over = false;

            student.Left = 246;
            student.Top = 360;
            teacher1.Left = 1000;
            teacher1.Top = 285;
            teacher2.Left = 302;
            teacher2.Top = 580;
            teacher3.Left = 600;
            teacher3.Top = 195;

            started.Start();
        }
        private void Gameover()
        {
            game_over = true;
            started.Stop();
            Close(); 
            th = new Thread(Open);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        public void Open(object obj)
        {
            Application.Run(new menu());
        }
    }
}
