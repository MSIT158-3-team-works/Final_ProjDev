using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class Torder
{
    public int OrderId { get; set; }

    public int MemberId { get; set; }

    public string OrderedTime { get; set; } = null!;

    public string ShippingAddress { get; set; } = null!;

    public string Consignee { get; set; } = null!;

    public string ConsigneePhone { get; set; } = null!;

    public string? Note { get; set; }

    public int ShippingMethod { get; set; }

    public int PaymentMethod { get; set; }

    public int LogisticsStatus { get; set; }

    public int OrderStatus { get; set; }

    public virtual TlogisticsStatus LogisticsStatusNavigation { get; set; } = null!;

    public virtual TIdentity Member { get; set; } = null!;

    public virtual TorderStatus OrderStatusNavigation { get; set; } = null!;

    public virtual TpaymentMethod PaymentMethodNavigation { get; set; } = null!;

    public virtual TshippingMethod ShippingMethodNavigation { get; set; } = null!;

    public virtual ICollection<TorderDetail> TorderDetails { get; set; } = new List<TorderDetail>();
}
