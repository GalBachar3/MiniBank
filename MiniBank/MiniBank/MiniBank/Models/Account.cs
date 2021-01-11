using System.Collections.Generic;

namespace MiniBank.Models
{
    public abstract class Account
    {
        public virtual int Id { get; set; }
        public virtual double Balance { get; set; }
        public virtual IList<User> Users { get; set; }

        protected Account()
        {
            Users = new List<User>();
        }
    }
}
