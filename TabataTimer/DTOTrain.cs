using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TabataTimer
{
    // intended for transfering data between forms. It contains only train instances
    class DTOTrain
    {
        private static DTOTrain instance = new DTOTrain();
        private bool isFilledVar = false;
        private List<Train> localData = new List<Train>();

        private DTOTrain() { }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static DTOTrain getInstance()
        {
            return instance;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool isFilled()
        {
            return isFilledVar;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool fill(List<Train> data)
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
        public List<Train> getTrains()
        {
            return new List<Train>(localData);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void clear()
        {
            localData = new List<Train>();
            isFilledVar = false;
        }
    }
}
