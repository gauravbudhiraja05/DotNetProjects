using System.Collections.Generic;
using System.ServiceModel;

namespace WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMyService" in both code and config file together.
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        List<movy> GetAllMovie();

        [OperationContract]
        int AddMovie(string Title, string Genre, string Price);

        [OperationContract]
        movy GetAllMovieById(int id);

        [OperationContract]
        int UpdateMovie(int Id, string Title, string Genre, string Price);

        [OperationContract]
        int DeleteMovieById(int Id);
    }
}
