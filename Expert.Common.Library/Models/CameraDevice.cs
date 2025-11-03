using mvIMPACT_NET;
using mvIMPACT_NET.acquire;
using System;
using System.Collections.Generic;
using System.Text;

namespace Expert.Common.Library
{
    public class CameraDevice
    {

        public CameraDevice(Device device, Window window)
        {
            this.DevicePointer = device;
            this.WindowPointer = window;

        }
        public Device DevicePointer { get; set; }

        public Window WindowPointer { get; set; }
    }
}
