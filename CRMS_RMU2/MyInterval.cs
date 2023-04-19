using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS_RMU2
{
    class MyInterval
    {
        private int invokeCount;
        private int maxCount;

        public MyInterval(int count)
        {
            invokeCount = 0;
            maxCount = count;
        }

        public void Tick(Object stateInfo)
        {

        }

    }
}
