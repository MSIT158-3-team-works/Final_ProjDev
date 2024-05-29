using System;
using System.Collections.Generic;

namespace projRESTfulApiFitConnect.Models;

public partial class TfieldPhoto
{
    public int FieldPhotoId { get; set; }

    public int FieldId { get; set; }

    public string FieldPhoto { get; set; } = null!;

    public virtual Tfield Field { get; set; } = null!;
}
