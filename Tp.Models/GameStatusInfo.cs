using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp.Models
{
    public class TryWordRequest
    {
        public string Word { get; set; }
    }

    public class TryLetterRequest
    {
        public char Letter { get; set; }
    }

    public class GameStatusInfoResponse
    {
        public bool IsMatch { get; set; }
        public string Result { get; set; }
        public int AttemptsLeft { get; set; }
        public List<char> IncorrectChars { get; set; }
        public bool GameOver { get; set; }
        public bool Win { get; set; }
    }
}
