using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TabataTimer
{
    class DBCache
    {
        private static DBCache Instance;
        private List<Train> Trains;
        private List<Exercise> Exercises;
        private List<Schedule> Schedules;

        private DBCache() {}

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static DBCache getIntance()
        {
            if(Instance == null) {
                Instance = new DBCache();
            }
            return Instance;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addTrain(Train train)
        {
            if(Trains == null)
            {
                Trains = new List<Train>();
            }
            Trains.Add(train);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addTrainBulk(List<Train> trains)
        {
            if (Trains == null)
            {
                Trains = new List<Train>();
            }
            Trains.AddRange(trains);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addExerciseBulk(List<Exercise> exercises)
        {
            if (Exercises == null)
            {
                Exercises = new List<Exercise>();
            }
            Exercises.AddRange(exercises);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void addSchedulesBulk(List<Schedule> scheduleList)
        {
            if (Schedules == null)
            {
                Schedules = new List<Schedule>();
            }
            Schedules.AddRange(scheduleList);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void resetCache()
        {
            Trains = null;
            Exercises = null;
            Schedules = null;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public List<Train> getAllTrains()
        {
            if(Trains == null)
            {
                Trains = new List<Train>();
            }
            return Trains;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public List<Exercise> getAllExercises()
        {
            if (Exercises == null)
            {
                Exercises = new List<Exercise>();
            }
            return Exercises;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public List<Schedule> getAllSchedules()
        {
            if (Schedules == null)
            {
                Schedules = new List<Schedule>();
            }
            return Schedules;
        }
    }
}
