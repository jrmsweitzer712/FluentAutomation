using FluentFramework.Core;
using FluentFramework.Core.Exceptions;
using FluentFramework.Core.Extensions;
using System;
using System.Text;

namespace FluentFramework.Pages.Shared
{
    public class ErrorPage : BasePage
    {
        public ErrorPage(TestBase test) : base(test)
        {
            OnLoad = () =>
            {
                // Use this to look for 404, 500, etc error pages.
                // throw Exception if one is found

                if (I.SeeText("Server Error in '/' Application"))
                    throw new ErrorPageException(new StringBuilder()
                        .AppendLine(Driver.VisibleText())
                        .ToString());

                if (I.SeeText("This page returned a 404 status code"))
                    throw new ErrorPageException(new StringBuilder()
                        .AppendLine(Driver.VisibleText())
                        .ToString());
            };
        }
    }
}
