using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shared.Equipment;

namespace devops_23_24_net_a02.Shared.Emails;

public class EmailResult
{
  public class Index {
    public IEnumerable<EmailDto.Index>? EmailAddresses { get; set; }

    public int TotalAmount { get; set; }
  }
}

