namespace mcc_web_client.Models
{
    public class MCCD
    {
        public int Id { get; set;  }
        public double tmp { get; set; }
        public double vbr { get; set; }
        public double rpm { get; set; }
        public DateTime recordingTime { get; set; }
    }
}
