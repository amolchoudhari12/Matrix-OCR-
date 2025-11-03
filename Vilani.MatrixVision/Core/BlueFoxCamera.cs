using mvIMPACT_NET.acquire;
using System;
using System.Collections.Generic;
using System.Text;


namespace Vilani.MatrixVision.Core
{
    public class BlueFoxCamera
    {
        DeviceManager _devMgr;
        public Device GetDeviceFromUserInput(ref DeviceManager devMgr, uint userInputId)
        {
            uint devCnt = devMgr.deviceCount();
            if (devCnt == 0)
            {
                Console.WriteLine("No MATRIX VISION device found!");
                return null;
            }

            // display every device detected
            for (uint i = 0; i < devCnt; i++)
            {
                Device dev = devMgr.getDevice(i);
                if (dev != null)
                {
                    Console.WriteLine("[{0}]: {1}({2}, family: {3})", i, dev.serial.read(), dev.product.read(), dev.family.read());
                }
            }


            uint devNr = userInputId;

            if (devNr >= devCnt)
            {
                Console.WriteLine("Invalid selection!");
                return null;
            }

            Console.WriteLine("Using device number {0}", devNr);
            return devMgr.getDevice(devNr);
        }
    }
}
