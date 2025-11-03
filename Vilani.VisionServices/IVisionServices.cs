using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace Vilani.VisionServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IVisionServices" in both code and config file together.

    
    [ServiceContract]
    public interface IVisionServices
    {
        [OperationContract]
        int DoWork();
    }
}
