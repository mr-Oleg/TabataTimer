using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabataTimer
{
    class ExerciseDAO
    {
        private string dbFileName;
        private SQLiteConnection dbConn;

        public static ExerciseDAO getInstance()
        {
            return new ExerciseDAO();
        }

        private void prepareData() // possible, we can incapsulate it to avoid same code from another DAO class
        {
            try
            {
                dbFileName = Resources.databaseTitle;
                if (!File.Exists(dbFileName))
                {
                    throw new FileNotFoundException("DataBase file " + dbFileName + " not found! Ask admin for solving this problem!");
                }
                dbConn = new SQLiteConnection(Resources.connectionString);
            }
            catch (SQLiteException ex)
            {
                throw new IOException("Problem with DB connection", ex);
            }
        }

        public List<Exercise> getAllExercises()
        {
            List<Exercise> exerciseList = new List<Exercise>();
            try
            {
                prepareData();
                dbConn.Open();
                SQLiteCommand sqlCmd = new SQLiteCommand(Resources.getAllExercises);
                sqlCmd.Connection = dbConn;
                SQLiteDataReader reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    exerciseList.Add(new Exercise(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), LocalUtils.base64ToImage(reader.GetString(3))));
                }
            }
            catch (SQLiteException ex)
            {
                throw new IOException("Problem with query text for getting all exercises from DB.", ex);
            }
            finally
            {
                dbConn.Close();
            }
            return exerciseList;
        }

        public List<Exercise> getAllExercisesForTrainWithId(Train obj)
        {
            List<Exercise> exerciseList = new List<Exercise>();
            try
            {
                prepareData();
                dbConn.Open();
                SQLiteCommand sqlCmd = new SQLiteCommand(String.Format(Resources.getAllExercisesForTrainWithId, obj.ID));
                sqlCmd.Connection = dbConn;
                SQLiteDataReader reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    exerciseList.Add(new Exercise(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), LocalUtils.base64ToImage(reader.GetString(3))));
                }
            }
            catch (SQLiteException ex)
            {
                throw new IOException(String.Format("Problem with query text for getting all exercises for train with id = {0} from DB.", obj.ID), ex);
            }
            finally
            {
                dbConn.Close();
            }
            return exerciseList;
        }

        public Exercise getSelectedExcercise(int id)
        {
            Exercise selectedObj = null;
            try
            {
                prepareData();
                dbConn.Open();
                SQLiteCommand sqlCmd = new SQLiteCommand(String.Format(Resources.getSelectedExercise, id));
                sqlCmd.Connection = dbConn;
                SQLiteDataReader reader = sqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    selectedObj = new Exercise(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), LocalUtils.base64ToImage(reader.GetString(3)));
                }
            }
            catch (SQLiteException ex)
            {
                throw new IOException(String.Format("Problem with query text for getting exercise with id = {0} from DB.",id), ex);
            }
            finally
            {
                dbConn.Close();
            }
            return selectedObj; //Optional? No, sorry
        }

        public void updateSelectedExcercise(Exercise updatedObj) // possible, we can return Enum with query status or something like that
        {
            try
            {
                prepareData();
                dbConn.Open();
                SQLiteCommand sqlCmd = new SQLiteCommand(String.Format(Resources.updateExerciseWithId, updatedObj.Title, updatedObj.TimeBetweenRounds, updatedObj.EncryptedImage, updatedObj.ID));
                sqlCmd.Connection = dbConn;
                sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                throw new IOException(String.Format("Problem with query text for updating exercise with id = {0} from DB.", updatedObj.ID), ex);
            }
            finally
            {
                dbConn.Close();
            }
        }

        public void removeSelectedExercise(Exercise toRemoveObj) // possible, we can return Enum with query status or something like that
        {
            try
            {
                prepareData();
                dbConn.Open();
                SQLiteCommand sqlCmd = new SQLiteCommand(String.Format(Resources.removeExerciseWithId, toRemoveObj.ID));
                sqlCmd.Connection = dbConn;
                sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                throw new IOException(String.Format("Problem with query text for removing exercise with id = {0} from DB.", toRemoveObj.ID), ex);
            }
            finally
            {
                dbConn.Close();
            }
        }

        public void insertExercise(string title, int timeBetweenRounds, Image picture) // possible, we can return Enum with query status or something like that
        {
            try
            {
                prepareData();
                dbConn.Open();
                Console.WriteLine(String.Format(Resources.insertExercise, title, timeBetweenRounds, LocalUtils.imageToBase64(picture)));
                SQLiteCommand sqlCmd = new SQLiteCommand(String.Format(Resources.insertExercise, title, timeBetweenRounds, LocalUtils.imageToBase64(picture)));
                sqlCmd.Connection = dbConn;
                sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                throw new IOException("Problem with query text for inserting exercise to DB.", ex);
            }
            finally
            {
                dbConn.Close();
            }
        }
    }
}
