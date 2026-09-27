using System;
using System.Collections.Generic;

namespace VoxelGallery.Persistence.DataModel.Entities;

public partial class OldScene
{
    public long Id { get; set; }

    public long? UploaderId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime UploadTime { get; set; }

    public int CameraPosX { get; set; }

    public int CameraPosY { get; set; }

    public int CameraPosZ { get; set; }

    public int CameraLookatX { get; set; }

    public int CameraLookatY { get; set; }

    public int CameraLookatZ { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User? Uploader { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<User> UsersNavigation { get; set; } = new List<User>();
}
