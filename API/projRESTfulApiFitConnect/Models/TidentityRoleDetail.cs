using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TidentityRoleDetail
{
    public int RoleId { get; set; }

    public string RoleDescribe { get; set; } = null!;

    public virtual ICollection<TIdentity> TIdentities { get; set; } = new List<TIdentity>();
}
