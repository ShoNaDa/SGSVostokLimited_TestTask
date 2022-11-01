using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSVostokLimited
{
    public class Shift
    {
        private ushort shiftNumber;

        public ushort ShiftNumber { get => shiftNumber; private set => shiftNumber = value; }

        public Shift(ushort _shiftNumber)
        {
            ShiftNumber = _shiftNumber;
        }
    }
}
