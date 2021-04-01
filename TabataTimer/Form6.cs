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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
            List<Train> items = DBCache.getIntance().getAllTrains();
            foreach (Train iter in items)
            {
                checkedListBox1.Items.Add(iter.Title);
            }
        }

        private void button1_Click(object sender, EventArgs e) // choose
        {
            List<Train> localList = new List<Train>();
            DTOTrain trains = DTOTrain.getInstance();
            trains.clear();
            Console.WriteLine(checkedListBox1.SelectedItems[0]);
            List<Train> globalList = DBCache.getIntance().getAllTrains();
            foreach (string strIter in checkedListBox1.SelectedItems)
            {
                foreach (Train iter in globalList)
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
                trains.fill(localList);
                Console.WriteLine("exercises.getTrains().Count " + trains.getTrains().Count);
                this.Close();
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < checkedListBox1.Items.Count; ++ix)
                if (ix != e.Index) checkedListBox1.SetItemChecked(ix, false);
        }
    }
}
