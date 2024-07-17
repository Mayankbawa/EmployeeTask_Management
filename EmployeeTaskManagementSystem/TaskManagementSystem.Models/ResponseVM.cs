using Newtonsoft.Json;

namespace TaskManagementSystem.Models
{
    public class ResponseVM
    {
        public int status { get; set; }
        public string message { get; set; }
        public dynamic data { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
