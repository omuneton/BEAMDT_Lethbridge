using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xmodem
{
    public enum XModem : byte
    {
        SOH = 0x01,
        STX = 0x02,
        ETX = 0x03,
        EOT = 0x04,
        ACK = 0x06,
        NACK = 0x15,
        CAN = 0x18,

    }
}
