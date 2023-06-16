using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SugarProductionManagement.Models.Enums;
using SugarProductionManagement.Models;

namespace SugarProductionManagement.Filter {
    public class PagRfInventario : ActionFilterAttribute {
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
                if (usuario!.Departamento != Departamento.Administracao && usuario.Departamento != Departamento.Estoque) {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Home" }, { "action", "Index" } });
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
