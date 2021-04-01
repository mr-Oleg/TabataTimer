using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TabataTimer
{
    public partial class Form3 : Form
    {
        private Bitmap image;

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // add Image to preview
        {
            openFileDialog1.Filter = Resources.filterForImage;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    image = new Bitmap(openFileDialog1.FileName);
                }
                catch
                {
                    DialogResult result = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // save exercise to database
        {
            string title = textBox1.Text;
            int timeBetweenRounds = Convert.ToInt32(numericUpDown1.Value);
            if (title != null && !title.Equals(""))
            {
                ExerciseDAO exerciseRepository = ExerciseDAO.getInstance();
                exerciseRepository.insertExercise(title, timeBetweenRounds, image);
                DBCache.getIntance().resetCache();
                MessageBox.Show("Упражнение успешно добавлено!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Название не может быть пустым!");
            }
            
        }
    }
}
