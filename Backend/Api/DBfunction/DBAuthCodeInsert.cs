using System;
using System.Text.Json.Serialization;
using System.Xml;
using A2_project_work.ApplicationCore.Interfaces.Repositories;
using A2_project_work.Domain.Entities;
using A2_project_work.Infrastructure.Repositories;
using Dapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DBfunction
{
    //public enum Incomingmessage
    //{
    //    authcode,
    //    pic_id
    //}

    public class DBAuthCodeInsert
    {
        private readonly ILogger _logger;
        private readonly ILogRepository _logrepo;
        private readonly IConfiguration _conf;
        private readonly IPicRepository _picrepo;
        public DBAuthCodeInsert(ILoggerFactory loggerFactory, IConfiguration conf, ILogRepository rp, IPicRepository picrepo)
        {
            _logger = loggerFactory.CreateLogger<DBAuthCodeInsert>();
            _conf = conf;
            _logrepo = rp;
            _picrepo = picrepo;
        }


        [Function("DBAuthCodeInsert")]
        public async Task Run([ServiceBusTrigger("from-rasp-to-db", Connection = "servicebus")] Incomingmessage myQueueItem)
        {
            var jsonString = JsonConvert.SerializeObject(myQueueItem, new Newtonsoft.Json.JsonConverter[] { new StringEnumConverter() });
            _logger.LogInformation($"C# ServiceBus queue trigger function processed message: {jsonString}");
            try
            {
                Guid picid = await _picrepo.GetPicIdGivenPortnumberAndRaspberryId(myQueueItem.PortNumber, myQueueItem.BuildingNumber);
                try
                {
                    if (myQueueItem.info != null)
                    {
                        var recordid = await _logrepo.GetLastPicLogId(picid);

                        if (myQueueItem.info == "opened") { await _logrepo.UpdatePortState(recordid, true); }
                    }
                    if (myQueueItem.authcode != null && myQueueItem.unlockcode != null)
                    {
                        await _logrepo.InsertAuthcodeUnlockcodeAndPicGuid(myQueueItem.authcode, myQueueItem.unlockcode, picid);
                    }
                    if (myQueueItem.status != null)
                    {
                        if (myQueueItem.status == "alive")
                        {
                            await _picrepo.UpdatePicStatus(true, picid);
                        }
                        else
                        {
                            await _picrepo.UpdatePicStatus(false, picid);
                        }
                    }
                    else
                    {
                        _logger.LogError("no codes");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Can't insert code");
                    _logger.LogError(ex.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Can't get portnumber or building number");
                _logger.LogError(ex.Message);
            }



        }
    }
}
