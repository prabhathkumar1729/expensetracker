using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWalletTrackerBL.Models
{
    public class UserBL
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string SecurityQuestion { get; set; } = null!;

        public string SecurityAnswer { get; set; } = null!;
    }
}
