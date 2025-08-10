using Sinema_Dunyasi.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;



// -------- Custom Tag Helper 10 -----------
namespace Sinema_Dunyasi.MyTagHelpers
{
    public class MovieDetailTagHelper : TagHelper
    {
        public Movie Movie { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            TagBuilder divContainer = new TagBuilder("div");
            divContainer.AddCssClass("movie-detail w-80 m-auto");

            TagBuilder imgTag = new TagBuilder("img");
            imgTag.Attributes.Add("alt", "image");
            imgTag.Attributes.Add("src", $"/Uploads/{Movie.MovieImage}");

            TagBuilder imgContainer = new TagBuilder("div");
            imgContainer.AddCssClass("img-container");
            imgContainer.InnerHtml.AppendHtml(imgTag);

            TagBuilder detailContainer = new TagBuilder("div");
            detailContainer.AddCssClass("detail-container");

            detailContainer.InnerHtml.AppendHtml($"<h3 class='divh4'><span>Başlık : </span> {Movie.Title}</h3>");
            detailContainer.InnerHtml.AppendHtml($"<h4 class='divh1'><span>Yayın Yılı : </span> {Movie.ReleaseYear}</h4>");
            detailContainer.InnerHtml.AppendHtml($"<h4 class='divh1'><span>Kadro : </span> {Movie.Cast}</h4>");
            detailContainer.InnerHtml.AppendHtml($"<h4 class='divh1'><span>Yönetmen : </span> {Movie.Director}</h4>");
            detailContainer.InnerHtml.AppendHtml($"<h4 class='divh1'><span>Fiyat : </span> {Movie.Price}</h4>");

            divContainer.InnerHtml.AppendHtml(imgContainer);
            divContainer.InnerHtml.AppendHtml(detailContainer);

            output.Content.SetHtmlContent(divContainer);
        }
    }
}
