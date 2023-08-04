using System;
using System.Collections.Generic;

namespace FrontendWebApi.Models;

public partial class CartDetail
{
    public string CdId { get; set; } = null!;

    public string? CId { get; set; }

    public string? PId { get; set; }

    public int? GroupId { get; set; }

    public virtual Cart? CIdNavigation { get; set; }

    public virtual ComboGroup? Group { get; set; }

    public virtual Product? PIdNavigation { get; set; }
}
