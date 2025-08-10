using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;


//-------- Custom Html Helper 8 ------------


namespace Sinema_Dunyasi.Helpers
{
    public static class ContactInfoHelper
    {
        public static IHtmlContent ContactInfo(this IHtmlHelper htmlHelper)
        {
            var builder = new TagBuilder("div");
            builder.AddCssClass("col-md-6");

            var phoneSection = new TagBuilder("div");
            phoneSection.AddCssClass("mb-4");
            var phoneTitle = new TagBuilder("h5");
            phoneTitle.AddCssClass("fw-bold");
            phoneTitle.InnerHtml.Append("TELEFON :");
            var phoneText = new TagBuilder("p");
            phoneText.InnerHtml.Append("1 (234) 567 891,<br /><br />1 (234) 567 891");
            phoneSection.InnerHtml.AppendHtml(phoneTitle);
            phoneSection.InnerHtml.AppendHtml(phoneText);
            builder.InnerHtml.AppendHtml(phoneSection);

            var lawSection = new TagBuilder("div");
            lawSection.AddCssClass("mb-4");
            var lawTitle = new TagBuilder("h5");
            lawTitle.AddCssClass("fw-bold");
            lawTitle.InnerHtml.Append("İLGİLİ YASA :");
            var lawText = new TagBuilder("p");
            lawText.InnerHtml.Append("MADDE 5 (1) Yer sağlayıcı, yer sağladığı içeriği kontrol etmek veya hukuka aykırı bir faaliyetin söz konusu olup olmadığını araştırmakla yükümlü değildir.");
            lawSection.InnerHtml.AppendHtml(lawTitle);
            lawSection.InnerHtml.AppendHtml(lawText);
            builder.InnerHtml.AppendHtml(lawSection);

            var legalSection = new TagBuilder("div");
            legalSection.AddCssClass("mb-4");
            var legalTitle = new TagBuilder("h5");
            legalTitle.AddCssClass("fw-bold");
            legalTitle.InnerHtml.Append("HUKUKSAL :");
            var legalText = new TagBuilder("p");
            legalText.InnerHtml.Append("Yer sağlayıcı, yer sağladığı hukuka aykırı içerikten, ceza sorumluluğu ile ilgili hükümler saklı kalmak kaydıyla, bu Kanunun 8 inci ve 9 uncu maddelerine göre haberdar edilmesi halinde ve teknik olarak imkân bulunduğu ölçüde hukuka aykırı içeriği yayından kaldırmakla yükümlüdür.");
            legalSection.InnerHtml.AppendHtml(legalTitle);
            legalSection.InnerHtml.AppendHtml(legalText);
            builder.InnerHtml.AppendHtml(legalSection);

            builder.InnerHtml.AppendHtml("<br /><div class='hr mb-3'></div>");

            return builder;
        }
    }
}
