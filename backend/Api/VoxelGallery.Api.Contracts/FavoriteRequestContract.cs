namespace VoxelGallery.Api.Contracts;

public class FavoriteRequestContract
{
    public required long FavoriteId{get;set;}

    public required bool IsFavorite{get;set;}
}
