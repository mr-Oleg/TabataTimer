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
    public partial class Form7 : Form
    {
        private List<Train> items;
        public Form7()
        {
            InitializeComponent();
            items = DBCache.getIntance().getAllTrains();
            foreach (Train iter in items)
            {
                checkedListBox1.Items.Add(iter.Title);
            }
            if (!LocalUtils.freeModeEnabled)
            {
                MessageBox.Show("На сегодня нет запланированнных тренировок. Выберите из того, что есть!");
            }
        }

        private void button1_Click(object sender, EventArgs e) // choose
        {
            foreach (Train iter in items)
            {
                if (checkedListBox1.SelectedItem.Equals(iter.Title))
                {
                    LocalUtils.selectedTrain = iter;
                }
            }
            this.Close();
        }      

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < checkedListBox1.Items.Count; ++ix)
                if (ix != e.Index) checkedListBox1.SetItemChecked(ix, false);
        }
    }
}
