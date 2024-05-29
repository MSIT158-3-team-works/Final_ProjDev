using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TgenderTable
{
    public int GenderId { get; set; }

    public string GenderText { get; set; } = null!;

    public virtual ICollection<TIdentity> TIdentities { get; set; } = new List<TIdentity>();
}
