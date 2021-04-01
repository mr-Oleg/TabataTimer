using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TabataTimer
{
    class ServiceDTO
    {
        private static ServiceDTO instance = new ServiceDTO();
        private  static bool isFilledVar = false;

        private int preparing = 0;
        private int rest = 0;
        private int cycles = 0;

        private ServiceDTO() { }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static ServiceDTO getInstance()
        {
            return instance;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool isFilled()
        {
            return isFilledVar;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool fill(int preparingArg, int restArg, int cyclesArg)
        {
            preparing = preparingArg;
            rest = restArg;
            cycles = cyclesArg;
            isFilledVar = true;
            return isFilledVar;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int getRest()
        {
            return rest;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int getCycles()
        {
            return cycles;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int getPrepare()
        {
            return preparing;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public void clear()
        {
            preparing = 0;
            rest = 0;
            cycles = 0;
            isFilledVar = false;
        }
    }
}

