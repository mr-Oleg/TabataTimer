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
    public partial class Form9 : Form
    {
        private int preparing = 0;
        private int rest = 0;
        private int cycles = 0;
        public Form9()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServiceDTO transferObj = ServiceDTO.getInstance();
            preparing = Convert.ToInt32(numericUpDown1.Value);
            rest = Convert.ToInt32(numericUpDown2.Value);
            cycles = Convert.ToInt32(numericUpDown3.Value);
            ServiceDTO dtoServiceInfo = ServiceDTO.getInstance();
            dtoServiceInfo.fill(preparing, rest, cycles);
            this.Close();
        }
    }
}
