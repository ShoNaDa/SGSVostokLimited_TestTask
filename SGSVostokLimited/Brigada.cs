using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSVostokLimited
{
    public class Brigade
    {
        private byte brigadeNumber;

        public byte BrigadeNumber { get => brigadeNumber; private set => brigadeNumber = value; }

        public Brigade(byte _brigadeNumber)
        {
            BrigadeNumber = _brigadeNumber;
        }
    }
}
