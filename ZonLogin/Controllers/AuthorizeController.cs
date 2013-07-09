using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using ZonLogin.Filters;
using ZonLogin.Models;

namespace ZonLogin.Controllers
{
    public enum ManageMessageId
    {
        ChangePasswordSuccess,
        SetPasswordSuccess,
        RemoveLoginSuccess,
        RemoveAppSuccess,
    }

    [InitializeSimpleMembership]
    public class AuthorizeController : Controller
    {
        //
        // GET: /Authorize/
        private string _client_id;
        private string _redirect_uri;
        private string _scope;
        private string _state;
        private string _response_type;

        public ActionResult Index()
        {
            string h = "";
            _client_id = Request.QueryString["client_id"];
            _redirect_uri = Request.QueryString["redirect_uri"];
            _state = Request.QueryString["state"];
            _scope = Request.QueryString["scope"];
            _response_type = Request.QueryString["response_type"];

            ZonLoginEntities dbclient = new ZonLoginEntities();
            string appNameDb = (from g in dbclient.Clients
                                where g.clientId == _client_id
                                select g.appName).FirstOrDefault();
            if (appNameDb != null)
            {
                if (string.IsNullOrEmpty(_client_id))
                    ViewBag.client_id = "none";
                else
                    ViewBag.client_id = _client_id;

                if (string.IsNullOrEmpty(_redirect_uri))
                    ViewBag.lblApplicationHostname = "Unknown";
                else
                    ViewBag.lblApplicationHostname = new Uri(_redirect_uri).Host;

                if (string.IsNullOrEmpty(_scope))
                    ViewBag.lblAuthorizationScope = "None";
                else
                    ViewBag.lblAuthorizationScope = _scope;

                ViewBag.redirec_uri = _redirect_uri;
                ViewBag.state = _state;

                ViewBag.lblApplicationHostname = appNameDb;
                var x = from z in dbclient.Webpages_clients
                        where (z.userId == WebSecurity.CurrentUserId && z.clientId == _client_id)
                        select z;
                int i = x.Count();
                if (i == 0)
                {
                    return View();
                }
                else
                {
                    if (_response_type.Equals("code"))
                    // Web Server Flow
                    {
                        h = string.Format(ConfigurationManager.AppSettings["AuthorizeRedirect"] + "authorizationcode?response_type={5}&client_id={0}&redirect_uri={1}&scope={2}&state={3}&user_id={4}", _client_id, _redirect_uri, _scope, _state, User.Identity.Name, _response_type);
                    }
                    else if (_response_type.Equals("token"))
                    {
                        h = string.Format(ConfigurationManager.AppSettings["AuthorizeRedirect"] + "token?response_type={5}&client_id={0}&redirect_uri={1}&scope={2}&state={3}&user_id={4}", _client_id, _redirect_uri, _scope, _state, User.Identity.Name, _response_type);
                    }
                    return Redirect(h);
                }
            }
            else
            {
                return View("InvalidApp");
            }
        }

        //
        // GET: /Authorize/Details/5

        public ActionResult InvalidApp()
        {
            return View();
        }

        public ActionResult Allow(string client_id, string Scope, string RedirectUri, string state, string response_type)
        {
            string h = "";
            _client_id = client_id;
            _redirect_uri = RedirectUri;
            _state = state;
            _scope = Scope;
            _response_type = response_type;

            ZonLoginEntities dbclient = new ZonLoginEntities();
            dbclient.Webpages_clients.Add(new Webpages_clients
            {
                userId = WebSecurity.CurrentUserId,
                clientId = _client_id,
                authDate = DateTime.Now
            });
            dbclient.SaveChanges();

            if (response_type.Equals("code"))
            // Web Server Flow
            {
                h = string.Format(ConfigurationManager.AppSettings["AuthorizeRedirect"] + "authorizationcode?&response_type={5}&client_id={0}&redirect_uri={1}&scope={2}&state={3}&user_id={4}", _client_id, _redirect_uri, _scope, _state, User.Identity.Name, _response_type);
            }
            else if (response_type.Equals("token"))
            {
                h = string.Format(ConfigurationManager.AppSettings["AuthorizeRedirect"] + "token?&response_type={5}&client_id={0}&redirect_uri={1}&scope={2}&state={3}&user_id={4}", _client_id, _redirect_uri, _scope, _state, User.Identity.Name, _response_type);
            }
           
            //WebRequest.DefaultWebProxy = new WebProxy("http://10.137.57.1:8080", true);
            return Redirect(h);
        }

        //
        // GET: /Authorize/Create

        public ActionResult Deny(string id)
        {
            return View();
        }

        //
        // POST: /Authorize/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
           
            // Use a transaction to prevent the user from deleting their last login credential
               
            ZonLoginEntities clientDb = new ZonLoginEntities();
            Webpages_clients t = clientDb.Webpages_clients.Where(s => s.clientId == provider && s.userId == WebSecurity.CurrentUserId).FirstOrDefault();
            if (t != null)
                clientDb.Webpages_clients.Remove(t);
            clientDb.SaveChanges();

            message = ManageMessageId.RemoveAppSuccess;
                   
            return RedirectToAction("Manage", "Account", new { Message = message });
        }

        //
        // GET: /Authorize/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Authorize/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Authorize/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Authorize/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}