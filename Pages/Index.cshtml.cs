using mcc_web_client.Data;
using mcc_web_client.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace mcc_web_client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AppDbContext _dbContext;


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public IndexModel(ILogger<IndexModel> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public List<MCCD> MccData;
        public string IpValue = "localhost";
        public ushort PortValue = 29500;

        private void SendMccCommand(string cmd)
        {
            try
            {
                // Function is deprecated (level: warning),
                // yet works better than new analog
#pragma warning disable SYSLIB0014 // Type or member is obsolete
                _ = new WebClient().DownloadString(
                    $"http://{IpValue}:{PortValue}/{cmd}");
#pragma warning restore SYSLIB0014 // Type or member is obsolete
            }
            catch (Exception)
            {
                // Unable to connect with MCC server
            }
        }

        public void OnGet()
        {
            // Retrieve all table data
            // limit: last added 50 rows
            MccData = _dbContext.GetAllData();
        }

        public void OnPostSave()
        {
            // Set new ip, port values
            var addrValue = Request.Form["mccIp"];
            if (!String.IsNullOrWhiteSpace(addrValue))
                IpValue = addrValue;
            _ = ushort.TryParse(Request.Form["mccPort"], out PortValue);

            // Resets to NULL if not handled
            // -- slow down page load speed
            MccData = _dbContext.GetAllData();
        }

        public void OnPostReqEnable()
        {
            SendMccCommand("cmd_ee");
            // Resets to NULL if not handled here
            // -- slow down page load speed
            MccData = _dbContext.GetAllData();
        }

        public void OnPostReqDisable()
        {
            SendMccCommand("cmd_de");
            // Resets to NULL if not handled here
            // -- slow down page load speed
            MccData = _dbContext.GetAllData();
        }
    }
}