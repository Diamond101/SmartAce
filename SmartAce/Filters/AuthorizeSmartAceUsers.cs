using SmartAceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartAce.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AuthorizeSmartAceUsers: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["UserSession"] != null)
            {
                UserSession userSession = (UserSession)httpContext.Session["UserSession"];
                return PermittedToAction(userSession.Role, Roles);
            }

            return false;
        }

        private bool PermittedToAction(string userRole, string actionRole)
        {
            if (string.IsNullOrWhiteSpace(actionRole))
            {
                return true;
            }

            List<string> roles = actionRole.Split(',').ToList();
            var ifound = roles.FirstOrDefault(r => r.Equals(userRole));

            if (userRole == ifound)
            {
                return true;
            }            

            HttpContext.Current.Session["PreviousURL"] = HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string _previousUrl = "";
            if (filterContext.HttpContext.Session["PreviousURL"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{
                    {"DestinationPage", filterContext.RouteData.Values["DestinationPage"] },
                    { "controller", "Home" },
                    { "action", "" }
                });
            }
            else
            {
                _previousUrl = (string)filterContext.HttpContext.Session["PreviousURL"];
                GetControllerActionFromURL(_previousUrl);

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{
                    {"DestinationPage", filterContext.RouteData.Values["DestinationPage"] },
                    { "controller", controllerName },
                    { "action", actionName }
                });
            }

        }

        object controllerName = "";
        object actionName = "";
        object areaName = "";
        private void GetControllerActionFromURL(string fullUrl)
        {
            // Split the url to url + query string
            var questionMarkIndex = fullUrl.IndexOf('?');
            string queryString = null;
            string url = fullUrl;
            if (questionMarkIndex != -1) // There is a QueryString
            {
                url = fullUrl.Substring(0, questionMarkIndex);
                queryString = fullUrl.Substring(questionMarkIndex + 1);
            }

            // Arranges
            var request = new HttpRequest(null, url, queryString);
            var response = new HttpResponse(new System.IO.StringWriter());
            var httpContext = new HttpContext(request, response);

            var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));

            // Extract the data    
            var values = routeData.Values;
            controllerName = values["controller"];
            actionName = values["action"];
            areaName = values["area"];
        }
    }

}