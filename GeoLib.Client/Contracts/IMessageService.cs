using System.ServiceModel;

namespace GeoLib.Client.Contracts
{
    [ServiceContract(Namespace = "http://www.pluralsight.com/MiguelCastro/WCF-End-To-End")]
    public interface IMessageService
    {
        [OperationContract]
        void ShowMessage(string message);
    }
}
