using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TabataTimer
{
    class Train
    {

        public int ID { get; private set; }

        public string Title { get; private set; }

        public int TimeBetweenExercises { get; private set; }

        public List<Exercise> Exercises { get; private set; }

        public Train(int id, string title, int timeBetweenExercises) : this(id, title, timeBetweenExercises, new List<Exercise>()) {}

        public Train(int id, string title, int timeBetweenExercises, List<Exercise> exercises)
        {
            ID = id;
            Title = title;
            TimeBetweenExercises = timeBetweenExercises;
            Exercises = exercises;
        }

        public Exercise searchExercisebyTitle(string title)
        {
            foreach (Exercise iter in Exercises)
            {
                if (iter.Title.Equals(title))
                {
                    return iter;
                }
            }
            return null; // Optional? No, sorry
        }

    }
}
