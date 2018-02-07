using System.ServiceModel;

namespace EvoCraft2.Common
{
    [ServiceContract]
    public interface IAdminService
    {
        [OperationContract]
        void SetServerDetails(GameDescription description);
    }
}
