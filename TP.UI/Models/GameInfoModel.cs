using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP.UI.Models
{
    public class GameInfoModel
    {
        public string Result { get; set; }
        public List<char> IncorrectChars { get; set; }
        public int AttemptsLeft { get; set; }
        public string Difficulty { get; set; }
        public string Name { get; set; }
    }
}
