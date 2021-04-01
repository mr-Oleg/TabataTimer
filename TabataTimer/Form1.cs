using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TabataTimer
{
    public partial class Form1 : Form
    {
        private Train cur;
        private int round = 0;
        private static bool pause = false;
        private static bool repeatRound = false;
        private static bool again = false;
        private SoundPlayer sp;



        public Form1()
        {
            InitializeComponent();
            String fullAppName = Application.ExecutablePath;
            String fullAppPath = Path.GetDirectoryName(fullAppName);
            String fullFileName = Path.Combine(fullAppPath, "1.wav");
            sp = new SoundPlayer(fullFileName);
            pictureBox1.Load("1.png");
            
            LocalUtils.setRoundedShape(panel2, 150);
        }

        private async void button3_ClickAsync(object sender, EventArgs e) // choose train
        {
            if (!button3.Text.Equals("Начать")) {
                new Initializer().initialize();
                Form8 selector = new Form8();
                selector.ShowDialog(this);
                cur = LocalUtils.selectedTrain;
                button3.Text = "Начать";
                listView1.Visible = true;               
                
                label1.Text = cur.Exercises[0].TimeBetweenRounds + "";
                round++;
                label4.Text = round + "";
                listView1.Items.Add("Подготовка");
                foreach (Exercise iter in cur.Exercises) {
                    listView1.Items.Add(iter.Title);
                    listView1.Items.Add("Отдых");
                }
                pictureBox1.Image = cur.Exercises[0].EncryptedImage;
                listView1.Items[0].Selected = true;
                
            }
            else
            {
                Form9 additional = new Form9();
                additional.ShowDialog(this);
                ServiceDTO serviceInfo = ServiceDTO.getInstance();
                int cycles = serviceInfo.getCycles();
                int rest = serviceInfo.getRest();
                int preparing = serviceInfo.getPrepare();
                //bool restTime = false;
                bool prepareTime = true;
                bool restTime = false;
                label3.Text = cycles + "";
                button3.Visible = false;
                listView1.MultiSelect = false;
                int counter = 0;
                for (int y = 0; y < cycles; y++)
                {
                    for (int a = 0; a < (cur.Exercises.Count * 2 + 1); a++)
                    {
                        if (!prepareTime)
                        {
                            if (!restTime)
                            {
                                if (!pause)
                                {
                                    if (!again)
                                    {
                                        if (a < cur.Exercises.Count)
                                        {
                                            pictureBox1.Image = cur.Exercises[a].EncryptedImage;
                                            pictureBox1.Invalidate();
                                            this.Invalidate();
                                            listView1.Items[counter].Selected = true;

                                            for (int i = 0; i < cur.Exercises[a].TimeBetweenRounds; i++)
                                            {
                                                if (!pause)
                                                {
                                                    if (!repeatRound)
                                                    {
                                                        if (!again)
                                                        {
                                                            if ((cur.Exercises[a].TimeBetweenRounds - i) < 4)
                                                            {
                                                                sp.PlaySync();
                                                            }
                                                            label1.Text = (cur.Exercises[a].TimeBetweenRounds - i) + "";
                                                            await Task.Delay(1000);
                                                        }
                                                        else
                                                        {
                                                            round = 1;
                                                            a = 0;
                                                            break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        repeatRound = false;
                                                        i = 0;
                                                    }
                                                }
                                                else
                                                {
                                                    while (pause)
                                                    {
                                                        await Task.Delay(100);
                                                    }
                                                }
                                            }
                                            restTime = true;
                                            label1.Text = cur.Exercises[0].TimeBetweenRounds + "";
                                            label4.Text = round + "";
                                            counter++;
                                            round++;
                                        }
                                        else
                                        {
                                           // MessageBox.Show("Тренировка закончена");

                                        }
                                    }
                                    else
                                    {
                                        again = false;
                                        counter = 0;

                                        a = 0;
                                        a--;
                                    }
                                }
                            }
                            else
                            {
                                restTime = false;
                                label1.Text = restTime + "";
                                listView1.Items[counter].Selected = true;
                                for (int q = 0; q < rest; q++)
                                {
                                    label1.Text = (rest - q) + "";
                                    if ((rest - q) < 4)
                                    {
                                        sp.PlaySync();
                                    }
                                    await Task.Delay(1000);
                                }
                                
                                prepareTime = false;
                                counter++;
                                a--;
                            }
                        }
                        else
                        {
                            label1.Text = preparing + "";
                            for (int q = 0; q < preparing; q++)
                            {
                                label1.Text = (preparing - q) + "";
                                if ((preparing - q) < 4)
                                {
                                    sp.PlaySync();
                                }
                                await Task.Delay(1000);
                            }
                            listView1.Items[0].Selected = true;
                            prepareTime = false;
                            counter++;
                            a = 0;
                            a--;
                        }
                    }
                    counter = 0;
                    label3.Text = (cycles - y) + "";
                }
                MessageBox.Show("Тренировка завершена!");
                button3.Text = "Выбрать тренировку";
                button3.Visible = true;
                label1.Text = "0";
                label4.Text = "0";
                label3.Text = "0";
                listView1.Clear();
                pictureBox1.Load("1.png");
                listView1.Visible = false;
                cur = null;
            }
        }

        static void showTime(Object obj)
        {
            Console.WriteLine("Текущие дата и время: {0}", DateTime.Now.ToString());
        }

        private void добавитьТренировкуToolStripMenuItem_Click(object sender, EventArgs e) // Add train
        {
            Form2 trainAddingWindow = new Form2();
            trainAddingWindow.Show();
        }

        private void добавитьУпражнениеToolStripMenuItem_Click(object sender, EventArgs e) // Add exercise
        {
            Form3 exerciseAddingWindow = new Form3();
            exerciseAddingWindow.Show();
        }

        private void запланироватьТренировкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Initializer().initialize();
            Form5 scheduler = new Form5();
            scheduler.Show();
        }

        private void button1_Click(object sender, EventArgs e) // stop
        {
            if (!pause)
            {
                button1.Text = "Продолжить";
                pause = true;
            }
            else
            {
                pause = false;
                button1.Text = "Стоп";
            }
        }

        private void button2_Click(object sender, EventArgs e) //again
        {
            again = true;
        }

        private void button4_Click(object sender, EventArgs e) //repeat current round
        {
            repeatRound = true;
        }
    }
}
