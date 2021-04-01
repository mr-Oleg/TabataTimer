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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            List<Exercise> items = DBCache.getIntance().getAllExercises();
            foreach (Exercise iter in items)
            {
                checkedListBox1.Items.Add(iter.Title);
            }
        }

        private void button1_Click(object sender, EventArgs e) // Select
        {
            List<Exercise> localList = new List<Exercise>();
            DataTransferObject exercises = DataTransferObject.getInstance();
            exercises.clear();
            Console.WriteLine(checkedListBox1.SelectedItems[0]);
            List<Exercise> globalList = DBCache.getIntance().getAllExercises();
            foreach (string strIter in checkedListBox1.SelectedItems) {
                foreach (Exercise iter in globalList)
                {
                    if (iter.Title.Equals(strIter))
                    {
                        localList.Add(iter);
                    }
                }
            }
            if (localList.Count == 0)
            {
                MessageBox.Show("Вы ничего не выбрали!");
            }
            else
            {
                exercises.fill(localList);
                Console.WriteLine("exercises.getExercises().Count " + exercises.getExercises().Count);
                this.Close();
            }
        }
    }
}
