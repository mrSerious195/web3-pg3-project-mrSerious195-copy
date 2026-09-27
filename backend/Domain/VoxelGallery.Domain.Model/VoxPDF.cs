using System;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using VoxelGallery.Domain.Model;

namespace VoxelGallery.Domain;

public class VoxPDF(SceneModel scene) : IDocument
{
    public void Compose(IDocumentContainer container)
    {  string poster = scene.Poster==null?"Unknown":scene.Poster.Name;
        container.Page(page =>
		{
			page.Size(PageSizes.A4);
			page.Margin(4.0f, Unit.Centimetre);


			page.Header()
				.Text($"snapshot of {scene.Name} (id ={scene.Id})")
				.FontSize(28)
				.Bold()
				.FontColor(Colors.Red.Darken2);
    
            
			page.Content()
				.PaddingVertical(8)
				.Column(column =>
				{
 					column.Item().Text($"time of snapshot: {DateTime.Now}");
                    column.Item().Text($"time of upload: {scene.UploadTime}");
                    column.Item().Text($"posted by: {poster}");
                    column.Item().Text($"Discription:{scene.Description}");
                    column.Item().Text($"Likes:{scene.LikeCount}");
                 
				});
		});
    }
}

