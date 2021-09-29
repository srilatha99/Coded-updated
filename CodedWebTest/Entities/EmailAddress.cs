using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodedWebTest.Entities
{
    public partial class EmailAddress
    {
        public Guid EmailAddressUid { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
