using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabataTimer
{
    class Schedule
    {
        public int ID { get; private set; }
        public int trainID { get; private set; }
        public long time { get; private set; }

        public Schedule(int ID, int trainID, long time)
        {
            this.ID = ID;
            this.trainID = trainID;
            this.time = time;
        }
    }
}
