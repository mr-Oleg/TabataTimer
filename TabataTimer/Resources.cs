using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabataTimer
{
    class Resources
    {
        public static readonly string databaseTitle = "TabataDataSource.db";
        public static readonly string connectionString = "Data Source = " + databaseTitle + "; Version = 3;";
        public static readonly string getAllTrains = "SELECT * FROM TRAIN";
        public static readonly string getAllExercises = "SELECT * FROM EXERCISE";
        public static readonly string getAllExercisesForTrainWithId = "SELECT * FROM EXERCISE WHERE ID IN (SELECT EXERCISEID FROM TRAINEXERCISE WHERE TRAINID = {0})";
        public static readonly string getSelectedExercise = "SELECT * FROM EXERCISE WHERE ID = {0}";
        public static readonly string getSelectedTrain = "SELECT * FROM TRAIN WHERE ID = {0}";
        public static readonly string getAllExercisesForSelectedTrain = "SELECT * FROM TRAINEXERCISE WHERE TRAINID = {0}";
        public static readonly string updateExerciseWithId = "UPDATE EXERCISE SET TITLE = {0}, TIMEBETWEENROUNDS = {1}, ENCRYPTEDIMAGE = {2} WHERE ID = {3}";
        public static readonly string updateTrainWithId = "UPDATE TRAIN SET TITLE = {0}, TIMEBETWEENEXERCISES = {1} WHERE ID = {2}";
        public static readonly string removeTrainWithId = "DELETE TRAIN WHERE ID = {0}";
        public static readonly string removeExerciseWithId = "DELETE EXERCISE WHERE ID = {0}";
        public static readonly string insertExercise = "INSERT INTO EXERCISE (TITLE, TIMEBETWEENROUNDS, ENCRYPTEDIMAGE) VALUES ('{0}', {1}, '{2}')";
        public static readonly string insertTrain = "INSERT INTO TRAIN (TITLE,TIMEBETWEENEXERCISES) VALUES ('{0}', {1})";
        public static readonly string insertLinkerToExercise = "INSERT INTO TRAINEXERCISE (TRAINID,EXERCISEID,DESCRIPTION) VALUES ({0}, {1}, '{2}');";
        public static readonly string getInsertedTrainID = "SELECT MAX(ID) FROM TRAIN;";
        public static readonly string getAllSchedules = "SELECT * FROM SCHEDULE;";
        public static readonly string removeAllSchedules = "DELETE SCHEDULE;";
        public static readonly string insertSchedule = "INSERT INTO SCHEDULE (TRAINID, TIME) VALUES ({0}, {1})";
        public static readonly string getScheduleWithId = "SELECT * FROM SCHEDULE WHERE ID = {0};";
        public static readonly string removeScheduleWithId = "DELETE SCHEDULE WHERE ID = {0};";
        public static readonly string filterForImage = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
        
    }
}
