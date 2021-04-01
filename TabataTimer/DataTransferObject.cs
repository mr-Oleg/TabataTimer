using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TabataTimer
{
    // intended for transfering data between forms. It contains only exercises instances
    class DataTransferObject
    {
        private static DataTransferObject instance = new DataTransferObject();
        private bool isFilledVar = false;
        private List<Exercise> localData = new List<Exercise>();

        private DataTransferObject() { }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static DataTransferObject getInstance()
        {
            return instance;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool isFilled()
        {
            return isFilledVar;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool fill(List<Exercise> data)
        {
            if (data != null && data.Count > 0)
            {
                localData.AddRange(data);
                isFilledVar = true;
                return isFilledVar;
            }
            else
            {
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public List<Exercise> getExercises()
        {
            return new List<Exercise>(localData);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void clear()
        {
            localData = new List<Exercise>();
            isFilledVar = false;
        }
    }
}
