using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery
{
    class User
    {
        private string id { get; set; }
        private int name { get; set; }
        private int identity { get; set; }
        private int numberOfUsualSpeech { get; set; }
        private int numberOfLotterySpeech { get; set; }
        private int activity { get; set; }
        public List<Dictionary<string, int>> RecentSpeech { get; set; }
    }
}
