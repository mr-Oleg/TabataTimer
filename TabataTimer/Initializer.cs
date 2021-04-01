using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TabataTimer
{
    class Initializer
    {

        public void initialize()
        {
            DBCache cacheInstance = DBCache.getIntance();
            cacheInstance.addTrainBulk(fillTrains(getAllTrains()));
            cacheInstance.addExerciseBulk(getAllExercises());
            cacheInstance.addSchedulesBulk(getAllSchedules());
        }

        private List<Train> fillTrains(List<Train> trains)
        {
            ExerciseDAO exerciseRepository = ExerciseDAO.getInstance();
            foreach (Train iter in trains)
            {
                iter.Exercises.AddRange(exerciseRepository.getAllExercisesForTrainWithId(iter));
            }
            return trains;
        }

        private List<Train> getAllTrains()
        {
            TrainDAO trainRepository = TrainDAO.getInstance();
            List<Train> fullTrainList = trainRepository.getAllTrains();
            return fullTrainList;
        }

        private List<Exercise> getAllExercises()
        {
            ExerciseDAO exerciseRepository = ExerciseDAO.getInstance();
            List<Exercise> fullExerciseList = exerciseRepository.getAllExercises();
            return fullExerciseList;
        }

        private List<Schedule> getAllSchedules()
        {
            ScheduleDAO scheduleRepository = ScheduleDAO.getInstance();
            List<Schedule> fullScheduleList = scheduleRepository.getAllSchedules();
            return fullScheduleList;
        }
    }
}
