using A2_project_work.Domain.Entities;


namespace A2_project_work.ApplicationCore.Interfaces.Repositories
{
    public interface ILogRepository : IRepository< Log,long>
    {
        Task InsertAuthcodeUnlockcodeAndPicGuid(string authcode,string unlockcode, Guid pic_id);

        Task UpdatePortState(long logid, bool state);

        Task<long> GetLastPicLogId(Guid pic_id);

        Task<string> GetUnlockCode(string auhtcode);

        Task UpdateUser(UsernameUserId us);

        Task<IEnumerable<GetLog>> PrettyGet();
    }
}
