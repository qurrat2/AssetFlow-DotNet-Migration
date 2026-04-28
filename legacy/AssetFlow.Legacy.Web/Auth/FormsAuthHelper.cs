using System;
using System.Web;
using System.Web.Security;
using AssetFlow.Core.Entities;

namespace AssetFlow.Legacy.Web.Auth;

public static class FormsAuthHelper
{
    public static void SignIn(HttpResponse response, User user)
    {
        var ticket = new FormsAuthenticationTicket(
            version: 1,
            name: user.Username,
            issueDate: DateTime.UtcNow,
            expiration: DateTime.UtcNow.AddMinutes(60),
            isPersistent: false,
            userData: $"{user.Id}|{user.Role}|{user.DepartmentId}");
        var encrypted = FormsAuthentication.Encrypt(ticket);
        response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encrypted)
        {
            HttpOnly = true,
            Secure = HttpContext.Current.Request.IsSecureConnection
        });
    }

    public static void SignOut(HttpResponse response)
    {
        FormsAuthentication.SignOut();
    }

    public static (int UserId, string Role, int DepartmentId)? Current()
    {
        var ctx = HttpContext.Current;
        if (ctx?.User?.Identity?.IsAuthenticated != true) return null;

        var cookie = ctx.Request.Cookies[FormsAuthentication.FormsCookieName];
        if (cookie == null) return null;
        var ticket = FormsAuthentication.Decrypt(cookie.Value);
        if (ticket == null) return null;
        var parts = ticket.UserData.Split('|');
        return (int.Parse(parts[0]), parts[1], int.Parse(parts[2]));
    }
}