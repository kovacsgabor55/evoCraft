using System.Collections.Generic;
using System.ServiceModel;

namespace EvoCraft2.Common
{
    [ServiceContract(CallbackContract = typeof(IServiceCallback))]
    public interface IClientService
    {
        [OperationContract]
        //[FaultContract(typeof())]
        string JoinGame(string person);

        [OperationContract]
        //[FaultContract(typeof())]
        void SendMessage(string message);

        [OperationContract]
        GameDescription GetServerDetails();

        [OperationContract]
        void GetMap(List<Unit> map);

        [OperationContract]
        void AddCommand(Command command);
    }

    [ServiceContract]
    public interface IServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void Join(string message);

        [OperationContract(IsOneWay = true)]
        void StartGame();

        [OperationContract(IsOneWay = true)]
        void SendMap(List<Unit> map);
    }
}
