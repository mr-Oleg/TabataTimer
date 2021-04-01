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
    class TrainDAO
    {
        private string dbFileName;
        private SQLiteConnection dbConn;

        public static TrainDAO getInstance()
        {
            return new TrainDAO();
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

        public List<Train> getAllTrains()
        {
            List<Train> trainList = new List<Train>();
            try
            {
                prepareData();
                dbConn.Open();
                SQLiteCommand sqlCmd = new SQLiteCommand(Resources.getAllTrains);
                sqlCmd.Connection = dbConn;
                SQLiteDataReader reader = sqlCmd.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    trainList.Add(new Train(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                    count++;
                }
            }
            catch (SQLiteException ex)
            {
                throw new IOException("Problem with query text for getting all trains from DB.", ex);
            }
            finally
            {
                dbConn.Close();
            }
            return trainList;
        }

        public Train getSelectedTrain(int id) 
        {
            Train selectedObj = null;
            try
            {
                prepareData();
                dbConn.Open();
                SQLiteCommand sqlCmd = new SQLiteCommand(String.Format(Resources.getSelectedTrain, id));
                sqlCmd.Connection = dbConn;
                SQLiteDataReader reader = sqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    selectedObj = new Train(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                }
            }
            catch (SQLiteException ex)
            {
                throw new IOException(String.Format("Problem with query text for getting train with id = {0} from DB.", id), ex);
            }
            finally
            {
                dbConn.Close();
            }
            return selectedObj; //Optional? No, sorry
        }

        public void updateSelectedTrain(Train updatedObj) // possible, we can return Enum with query status or something like that
        {
            try
            {
                prepareData();
                dbConn.Open();
                SQLiteCommand sqlCmd = new SQLiteCommand(String.Format(Resources.updateTrainWithId, updatedObj.Title, updatedObj.TimeBetweenExercises, updatedObj.ID));
                sqlCmd.Connection = dbConn;
                sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                throw new IOException(String.Format("Problem with query text for updating train with id = {0} from DB.", updatedObj.ID), ex);
            }
            finally
            {
                dbConn.Close();
            }
        }

        public void removeSelectedTrain(Train toRemoveObj) // possible, we can return Enum with query status or something like that
        {
            try
            {
                prepareData();
                dbConn.Open();
                SQLiteCommand sqlCmd = new SQLiteCommand(String.Format(Resources.removeTrainWithId, toRemoveObj.ID));
                sqlCmd.Connection = dbConn;
                sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                throw new IOException(String.Format("Problem with query text for removing train with id = {0} from DB.", toRemoveObj.ID), ex);
            }
            finally
            {
                dbConn.Close();
            }
        }

        public void insertTrain(string title, int timeBetweenExercises) // possible, we can return Enum with query status or something like that
        {
            try
            {
                prepareData();
                dbConn.Open();
                SQLiteCommand sqlCmd = new SQLiteCommand(String.Format(Resources.insertTrain, title, timeBetweenExercises));
                sqlCmd.Connection = dbConn;
                sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                throw new IOException("Problem with query text for inserting train to DB.", ex);
            }
            finally
            {
                dbConn.Close();
            }
        }

        public void insertTrainFull(string title, int timeBetweenExercises, List<Exercise> local)
        {
            int id = 1;
            try
            {


                using (SQLiteConnection c = new SQLiteConnection(Resources.connectionString))
                {
                    
                    using (SQLiteCommand sqlCmd = new SQLiteCommand(String.Format(Resources.insertTrain, title, timeBetweenExercises)))
                    {
                        c.Open();
                        sqlCmd.Connection = c;
                        sqlCmd.ExecuteNonQuery();
                        sqlCmd.CommandText = Resources.getInsertedTrainID;
                        SQLiteDataReader reader = sqlCmd.ExecuteReader();
                        reader.Read();
                        id = (int)reader.GetInt32(0);
                        c.Close();
                    }
                    
                }
                insertTrainWithLinker(new Train(id, title, timeBetweenExercises, local));
            }
            catch (SQLiteException ex)
            {
                throw new IOException("Problem with query text for inserting train to DB.", ex);
            }
            finally
            {
                
            }
        }

        private void insertTrainWithLinker(Train toInsert)
        {
            try
            {
                using (SQLiteConnection c = new SQLiteConnection(Resources.connectionString))
                {
                    
                    List<Exercise> local = toInsert.Exercises;
                    foreach (Exercise iter in local)
                    {

                        using (SQLiteCommand cmd = new SQLiteCommand(String.Format(Resources.insertLinkerToExercise, toInsert.ID, iter.ID, "")))
                        {
                            c.Open();
                            cmd.Connection = c;
                            //cmd.CommandTimeout = 1000;
                            cmd.ExecuteNonQuery();
                            c.Close();
                        }

                    }
                }
            }
            catch (SQLiteException ex)
            {
                throw new IOException("Problem with query text for inserting train to DB.", ex);
            }
            finally
            {
                
            }
        }
    }
}
