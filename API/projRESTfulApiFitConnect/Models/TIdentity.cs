using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TIdentity
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? EMail { get; set; }

    public string Password { get; set; } = null!;

    public string? Photo { get; set; }

    public DateOnly Birthday { get; set; }

    public string? Address { get; set; }

    public int GenderId { get; set; }

    public bool Activated { get; set; }

    public decimal? Payment { get; set; }

    public virtual TgenderTable Gender { get; set; } = null!;

    public virtual TidentityRoleDetail Role { get; set; } = null!;

    public virtual ICollection<TclassReserve> TclassReserves { get; set; } = new List<TclassReserve>();

    public virtual ICollection<TclassSchedule> TclassSchedules { get; set; } = new List<TclassSchedule>();

    public virtual ICollection<TcoachExpert> TcoachExperts { get; set; } = new List<TcoachExpert>();

    public virtual ICollection<TcoachInfoId> TcoachInfoIds { get; set; } = new List<TcoachInfoId>();

    public virtual ICollection<TcoachPhoto> TcoachPhotos { get; set; } = new List<TcoachPhoto>();

    public virtual ICollection<TfieldReserve> TfieldReserves { get; set; } = new List<TfieldReserve>();

    public virtual ICollection<TmemberFollow> TmemberFollowCoaches { get; set; } = new List<TmemberFollow>();

    public virtual ICollection<TmemberFollow> TmemberFollowMembers { get; set; } = new List<TmemberFollow>();

    public virtual ICollection<TmemberRateClass> TmemberRateClasses { get; set; } = new List<TmemberRateClass>();

    public virtual ICollection<TnewsComment> TnewsComments { get; set; } = new List<TnewsComment>();

    public virtual ICollection<Torder> Torders { get; set; } = new List<Torder>();

    public virtual ICollection<TproductShoppingcart> TproductShoppingcarts { get; set; } = new List<TproductShoppingcart>();

    public virtual ICollection<TproductTrack> TproductTracks { get; set; } = new List<TproductTrack>();
}
