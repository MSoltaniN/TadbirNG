using System;
using System.Web.Mvc;
using SPPC.Tadbir.Values;
using BabakSoft.Platform.Web.Markup;

namespace SPPC.Tadbir.Web.Extensions
{
    public static class CaptchaExtensions
    {
        public static MvcHtmlString Captcha(this HtmlHelper htmlHelper)
        {
            var captchaEditorMarkup = new HtmlMarkup(
                "input", new
                {
                    type = "text",
                    id = "user-response",
                    name = "user-response",
                    autocomplete = "off",
                    @class = "form-control captcha-entry",
                    required = "required"
                });
            var captchaSpanMarkup = new HtmlMarkup("span", new { @class = "field-validation-valid text-danger" });
            captchaSpanMarkup.Attributes.Add("data-valmsg-for", "user-response");
            captchaSpanMarkup.Attributes.Add("data-valmsg-replace", "true");
            captchaEditorMarkup.Attributes.Add("data-val", "true");
            captchaEditorMarkup.Attributes.Add("data-val-required", Strings.SecurityCodeIsRequired);
            var captchaMarkup = new HtmlMarkup("div", new { id = "captcha" },
                new HtmlMarkup("input", new { type = "hidden", id = "captcha-response", name = "captcha-response", value = String.Empty }),
                new HtmlMarkup("div", new { @class = "form-group" },
                    new HtmlMarkup("label") { Text = FieldNames.SecurityCode },
                    new HtmlMarkup("br"),
                    new HtmlMarkup("img", new { src = String.Empty, id = "captcha-image", alt = FieldNames.SecurityCode, @class = "img-responsive" }),
                    new HtmlMarkup("br"),
                    captchaEditorMarkup,
                    captchaSpanMarkup),
                new HtmlMarkup("div", new { },
                    new HtmlMarkup("a", new { href = "#", id = "new-captcha", visibility = "visible" }) { Text = Strings.NewSecurityCode }));

            return MvcHtmlString.Create(captchaMarkup.ToHtml());
        }
    }
}
