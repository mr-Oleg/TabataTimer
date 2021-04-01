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
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e) // choose exercises
        {
            int amountCachedExercises = DBCache.getIntance().getAllExercises().Count;
            if (amountCachedExercises == 0)
            {
                new Initializer().initialize();
                amountCachedExercises = DBCache.getIntance().getAllExercises().Count;
            }
            if (amountCachedExercises == 0)
            {
                MessageBox.Show("Нужно добавить хотя бы 1 упражнение в базу!");
            }
            else
            {
                DataTransferObject.getInstance().clear();
                Form4 exerciseSelector = new Form4();
                exerciseSelector.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e) // add
        {
            if (textBox1.Text != null && !textBox1.Text.Equals(""))
            {
                DataTransferObject dtoExercise = DataTransferObject.getInstance();
                List<Exercise> localExercises = dtoExercise.getExercises();
                dtoExercise.clear();
                if (localExercises.Count == 0)
                {
                    MessageBox.Show("Нужно добавить хотя бы 1 упражнение!");
                }
                else
                {
                    TrainDAO trainDataProvider = TrainDAO.getInstance();
                    Console.WriteLine(DataTransferObject.getInstance().getExercises().Count);
                    trainDataProvider.insertTrainFull(textBox1.Text,Convert.ToInt32(numericUpDown1.Value), localExercises);
                    MessageBox.Show("Тренировка успешно добавлена!");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Нужно добавить название к");
            }
        }
    }
}
