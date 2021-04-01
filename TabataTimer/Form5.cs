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
    public partial class Form5 : Form
    {
        private List<Train> localTrainListForMainForm = new List<Train>();
        private List<Train> localTrainList = new List<Train>();
        private List<Schedule> localScheduleList = new List<Schedule>();

        public Form5()
        {
            InitializeComponent();
            listView1.Clear();
            localScheduleList = new List<Schedule>();
            localScheduleList = DBCache.getIntance().getAllSchedules();
            localTrainListForMainForm = DBCache.getIntance().getAllTrains();
            if (localScheduleList == null || localScheduleList.Count == 0)
            {
                listView1.Visible = false;
            }
            else
            {
                listView1.Visible = true;
                foreach (Schedule iter in localScheduleList) {
                    listView1.Items.Add(new DateTime(iter.time).ToShortDateString() + " " + getTrainNameById(localTrainListForMainForm, iter.trainID));
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // create schedule
        {
            DTOTrain trainDTO = DTOTrain.getInstance();
            localTrainList.Clear();
            localTrainList.AddRange(trainDTO.getTrains());
            trainDTO.clear();
            if (dateTimePicker1.Text == null || dateTimePicker1.Text.Equals(""))
            {
                MessageBox.Show("Вы должны добавить время");
            }
            else
            {
                if (localTrainList == null || localTrainList.Count == 0)
                {
                    MessageBox.Show("Вы должны добавить тренировку");
                }
                else
                {
                    ScheduleDAO scheduledataProvider = ScheduleDAO.getInstance();
                    foreach (Train iter in localTrainList)
                    {
                        scheduledataProvider.insertSchedule(iter.ID, Convert.ToInt64(dateTimePicker1.Value.Ticks));
                        DBCache.getIntance().resetCache();
                        this.Close();
                        MessageBox.Show("Тренировка запланирована!");
                    }
                }
            }
        }

        private string getTrainNameById(List<Train> list, int id)
        {
            foreach (Train iter in list)
            {
                if(iter.ID == id)
                {
                    return iter.Title;
                }
            }
            return "default Train";
        }

        private void button1_Click(object sender, EventArgs e) // choose train
        {
            DTOTrain trainDTO = DTOTrain.getInstance();
            trainDTO.clear();
            Form6 trainSelector = new Form6();
            trainSelector.Show();
        }
    }
}
