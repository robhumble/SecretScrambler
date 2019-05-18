using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pinNote.Models
{
    public class SecurityModel
    {
        public bool SelectedAES { get; set; }
        public bool SelectedTripleDES { get; set; }
        public bool SelectedHumbleCrypt { get; set; }
        public string Password { get; set; }
    }
}
