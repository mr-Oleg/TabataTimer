using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabataTimer
{
    class ScheduleDAO
    {
        private string dbFileName;
        private SQLiteConnection dbConn;

        public static ScheduleDAO getInstance()
        {
            return new ScheduleDAO();
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

        public List<Schedule> getAllSchedules()
        {
            List<Schedule> scheduleList = new List<Schedule>();
            try
            {
                prepareData();
                dbConn.Open();
                SQLiteCommand sqlCmd = new SQLiteCommand(Resources.getAllSchedules);
                sqlCmd.Connection = dbConn;
                SQLiteDataReader reader = sqlCmd.ExecuteReader();
                while (reader.Read())
                {
                    scheduleList.Add(new Schedule(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt64(2)));
                }
            }
            catch (SQLiteException ex)
            {
                throw new IOException("Problem with query text for getting all schedules from DB.", ex);
            }
            finally
            {
                dbConn.Close();
            }
            return scheduleList;
        }

        public Schedule getSelectedExcercise(int id)
        {
            Schedule selectedObj = null;
            try
            {
                prepareData();
                dbConn.Open();
                SQLiteCommand sqlCmd = new SQLiteCommand(String.Format(Resources.getScheduleWithId, id));
                sqlCmd.Connection = dbConn;
                SQLiteDataReader reader = sqlCmd.ExecuteReader();
                if (reader.HasRows)
                {
                    selectedObj = new Schedule(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt64(2));
                }
            }
            catch (SQLiteException ex)
            {
                throw new IOException(String.Format("Problem with query text for getting schedule with id = {0} from DB.", id), ex);
            }
            finally
            {
                dbConn.Close();
            }
            return selectedObj; //Optional? No, sorry
        }

        public void removeSelectedSchedule(Schedule toRemoveObj) // possible, we can return Enum with query status or something like that
        {
            try
            {
                prepareData();
                dbConn.Open();
                SQLiteCommand sqlCmd = new SQLiteCommand(String.Format(Resources.removeScheduleWithId, toRemoveObj.ID));
                sqlCmd.Connection = dbConn;
                sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                throw new IOException(String.Format("Problem with query text for removing schedule with id = {0} from DB.", toRemoveObj.ID), ex);
            }
            finally
            {
                dbConn.Close();
            }
        }

        public void insertSchedule(int trainID, long time) // possible, we can return Enum with query status or something like that
        {
            try
            {
                prepareData();
                dbConn.Open();
                Console.WriteLine(String.Format(Resources.insertSchedule, trainID, time));
                SQLiteCommand sqlCmd = new SQLiteCommand(String.Format(Resources.insertSchedule, trainID, time));
                sqlCmd.Connection = dbConn;
                sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                throw new IOException("Problem with query text for inserting schedule to DB.", ex);
            }
            finally
            {
                dbConn.Close();
            }
        }

        public void removeAllSchedules() // possible, we can return Enum with query status or something like that
        {
            try
            {
                prepareData();
                dbConn.Open();
                Console.WriteLine(Resources.removeAllSchedules);
                SQLiteCommand sqlCmd = new SQLiteCommand(Resources.removeAllSchedules);
                sqlCmd.Connection = dbConn;
                sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                throw new IOException("Problem with query text for removing all schedules from DB.", ex);
            }
            finally
            {
                dbConn.Close();
            }
        }
    }
}
