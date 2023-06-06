﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SugarProductionManagement.Models;

namespace SugarProductionManagement.Filter {
    public class PagUserAutenticado : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            string sectionUser = filterContext.HttpContext.Session.GetString("sectionUserAutenticado");

            if (string.IsNullOrEmpty(sectionUser)) {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Logar" }, { "action", "Index" } });
            }
            else {
                Funcionario usuario = JsonConvert.DeserializeObject<Funcionario>(sectionUser);
                if (usuario == null) {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Logar" }, { "action", "Index" } });
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
