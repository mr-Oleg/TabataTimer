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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //confirm
        {
            if (radioButton1.Checked) // free choice
            {
                LocalUtils.freeModeEnabled = true;
                Form7 selectionTrainWindow = new Form7();
                selectionTrainWindow.ShowDialog(this);
                this.Close();
            }
            else if (radioButton2.Checked) // according to schedule
            {
                LocalUtils.freeModeEnabled = false;
                Form7 selectionTrainWindow = new Form7();
                selectionTrainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Выберите хотя бы один из вариантов!");
            }
        }
    }
}
