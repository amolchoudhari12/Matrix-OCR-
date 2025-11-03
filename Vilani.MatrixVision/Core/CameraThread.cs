using mvIMPACT_NET;
using mvIMPACT_NET.acquire;
using mvIMPACT_NET.match;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;

using System.Text;


namespace Vilani.MatrixVision.Core
{
    public class CameraThread : IDisposable
    {




        public CameraThread()
        {
            _devMgr = new DeviceManager();
            _blueFoxCamera = new Core.BlueFoxCamera();
            _pDev = _blueFoxCamera.GetDeviceFromUserInput(ref _devMgr, 0);

        }

        private bool boTerminated = false;
        private Device _pDev;
        DeviceManager _devMgr;
        BlueFoxCamera _blueFoxCamera;

        public delegate void UpdateLiveImageHandler(System.Drawing.Image img);
        public event CameraThread.UpdateLiveImageHandler UpdateLiveImage;

        public CameraThread(Device dev)
        {
            this._pDev = dev;
        }

        public void terminate()
        {
            boTerminated = true;
        }

        public void StartCapturing()
        {
            Console.WriteLine("Initializing the device. This might take some time...");
            try
            {
                _pDev.open();
            }
            catch (ImpactAcquireException e)
            {
                // this e.g. might happen if the same device is already opened in another process...
                Console.WriteLine("An error occurred while opening the device " + _pDev.serial.read() +
                     "(error code: " + e.Message + "). Press any key to end the application...");
                Console.ReadLine();
                Environment.Exit(0);
            }



            System.Console.WriteLine("Press space in the live window to end the program");

            // if color images shall be captured, setting the destination pixel format to a planar mode would improve
            // the performance as then the IMPACT representation of the image can be constructed more efficiently.
            //Settings settings = new Settings(ref pDev);
            //settings.imageDestination.pixelFormat.write( TImageDestinationPixelFormat.idpfRGBx888Planar );

            // establish access to the statistic properties
            Statistics statistics = new Statistics(ref _pDev);
            // create an interface to the device found
            FunctionInterface fi = new FunctionInterface(ref _pDev);

            // prefill the capture queue. There can be more then 1 queue, but for this sample
            // we will work with the default capture queue
            SystemSettings ss = new SystemSettings(ref _pDev);
            int REQUEST_COUNT = ss.requestCount.read();
            for (int i = 0; i < REQUEST_COUNT; i++)
            {
                fi.imageRequestSingle();
            }

            // run thread loop
            Request pRequest;
            const int timeout_ms = 5000;
            int requestNr = -1;
            int lastRequestNr = -1;
            int cnt = 0;
            while (!boTerminated)
            {
                // wait for results from the default capture queue
                requestNr = fi.imageRequestWaitFor(timeout_ms);
                if (fi.isRequestNrValid(requestNr))
                {
                    pRequest = fi.getRequest(requestNr);
                    if (fi.isRequestOK(ref pRequest))
                    {
                        ++cnt;
                        // here we can display some statistical information every 100th image
                        if (cnt % 100 == 0)
                        {
                            Console.WriteLine("Info from " + _pDev.serial.read() +
                                 " fps: " + statistics.framesPerSecond.readS() + ", error count: " +
                                 statistics.errorCount.readS() + ", capture time: " + statistics.captureTime_s.readS());
                        }
                        // This call is fast, as it uses the request memory to create the IMPACT image. However
                        // the IMPACT image will become invalid as soon as the request buffer is unlocked via
                        // fi.imageRequestUnlock( requestNr );
                        Image image = pRequest.getIMPACTImage(TImpactBufferFlag.ibfUseRequestMemory);
                        // This call would be slower as it copies the image data. This image remains valid even when the
                        // request has been unlocked again...
                        // Image image = pRequest.getIMPACTImage();
                        UpdateLiveImage(image.convertToBitmap());
                        //                        image.Dispose();
                    }
                    else
                    {
                        Console.WriteLine("Error: " + pRequest.requestResult.readS());
                    }
                    if (fi.isRequestNrValid(lastRequestNr))
                    {
                        // this image has been displayed thus the buffer is no longer needed...
                        fi.imageRequestUnlock(lastRequestNr);
                    }
                    lastRequestNr = requestNr;
                    // send a new image request into the capture queue
                    fi.imageRequestSingle();
                }
                else
                {
                    // this should not happen in this sample, but may happen if you wait for a request without
                    // sending one to the driver before or if a trigger is missing while the device has been 
                    // set up for triggerd acquisition. Please refer to the documentation for reasons for
                    // possible errors if you ever reach this code
                    Console.WriteLine("Acquisition error: {0}.", requestNr);
                }
            }



            // free the last potential locked request
            if (fi.isRequestNrValid(requestNr))
            {
                fi.imageRequestUnlock(requestNr);
            }
            // clear the request queue
            fi.imageRequestReset(0, 0);
            // extract and unlock all requests that are now returned as 'aborted'
            while ((requestNr = fi.imageRequestWaitFor(0)) >= 0)
            {
                fi.imageRequestUnlock(requestNr);
            }
            _pDev.close();
        }

        public void Dispose()
        {
            if (this != null)
                this.Dispose();
        }
    }

}
