using System.Collections.Generic;

namespace MiniBank.Models
{
    public class User : BasicEntity
    {
        public virtual string Name { get; set; }
        public virtual IList<Account> Accounts { get; set; }

        public User()
        {
            Accounts = new List<Account>();
        }
    }
}
