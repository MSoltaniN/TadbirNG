using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SPPC.Tadbir.Values;
using SwForAll.Platform.Web.Security;

namespace SPPC.Tadbir.Web.Extensions
{
    public static class ControllerExtensions
    {
        public static bool ValidateCaptcha(this Controller controller)
        {
            // Validation handles several scenarios :
            // 1. Captcha challenge is blank.
            // 2. Captcha challenge is invalid.
            // 3. Captcha challenge is valid.
            // 4. Session is invalid (i.e. anti-forgery token is absent or invalid).

            // To validate captcha, two predefined fields must be present :
            // 1. input[id="user-response"] => Plain-text captcha response from user (Text box).
            // 2. input[id="captcha-response"] => Hash of actual captcha response (Hidden field).
            var isValid = true;
            if (controller == null)
            {
                return isValid;
            }

            var formDictionary = controller.Request.Form;
            if (formDictionary != null
                && formDictionary.AllKeys.Contains("user-response")
                && formDictionary.AllKeys.Contains("captcha-response"))
            {
                if (!formDictionary.AllKeys.Contains("__RequestVerificationToken"))
                {
                    controller.ModelState.AddModelError(
                        String.Empty, Strings.InvalidBrowserRequest);
                    isValid = false;
                }
                else
                {
                    var userResponse = formDictionary["user-response"];
                    var responseHash = formDictionary["captcha-response"];
                    if (String.IsNullOrWhiteSpace(userResponse))
                    {
                        controller.ModelState.AddModelError(String.Empty, Strings.SecurityCodeIsRequired);
                        isValid = false;
                    }
                    else
                    {
                        var captchaServer = new CaptchaServer();
                        isValid = captchaServer.Validate(userResponse, responseHash);
                        if (!isValid)
                        {
                            controller.ModelState.AddModelError(String.Empty, Strings.InvalidSecurityCode);
                        }
                    }
                }
            }

            return isValid;
        }
    }
}
