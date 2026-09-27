using System;
using System.Collections.Generic;

namespace VoxelGallery.Persistence.DataModel.Entities;

public partial class OldComment
{
    public long Id { get; set; }

    public long SceneId { get; set; }

    public long UserId { get; set; }

    public virtual Scene Scene { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
