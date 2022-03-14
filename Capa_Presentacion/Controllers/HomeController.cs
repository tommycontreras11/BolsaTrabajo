using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Capa_Presentacion.Models;
using Capa_Presentacion.Extensions;

namespace Capa_Presentacion.Controllers
{
    public class HomeController : Controller
    {

        UsuariosController controller = new UsuariosController();
        public ActionResult Index()
        {
            if (Session["Tipo_usuario"] != null && Session["Tipo_usuario"].Equals("Admin"))
            {
                //this.AddNotification("El tipo de usuario es: " + Session["Tipo_usuario"], NotificationType.SUCCESS);
            }
            else if (Session["Tipo_usuario"] != null && Session["Tipo_usuario"].Equals("Usuario") && Session["ID_Usuario"] != null)
            {
                //this.AddNotification($"El tipo de usuario es: {Session["Tipo_usuario"]} y el id es {Session["ID_cliente"]}", NotificationType.SUCCESS);
            }
            else
            {
                return RedirectToAction("/Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Usuarios usuarios)
        {

            try
            {
                var login = controller.Loguearse(usuarios).FirstOrDefault();

                if (login != null)
                {

                    var tipo = controller.ValidarTipoUsuario(usuarios).FirstOrDefault();
                    if (tipo.Equals("Admin"))
                    {
                        Session["Tipo_usuario"] = tipo;
                        return RedirectToAction("/Index");

                    }
                    else if (tipo.Equals("Usuario"))
                    {
                        Session["Tipo_usuario"] = tipo;
                        Session["ID_Usuario"] = controller.ObtenerID_Usuario(usuarios);
                        return RedirectToAction("/Index");
                    }
                }
                else
                {
                    this.AddNotification("Usuario y/o contraseña incorrectos", NotificationType.ERROR);
                }
            }
            catch (Exception e) {
                this.AddNotification("Ha ocurrido un error: " + e.Message, NotificationType.ERROR);

            }

            return View();
        }

        public ActionResult CerrarSesion()
        {
            if (Session["Tipo_usuario"] != null && Session["Tipo_usuario"].Equals("Admin"))
            {
                Session["Tipo_usuario"] = null;
                return RedirectToAction("/Index");
            }
            else if (Session["Tipo_usuario"] != null && Session["Tipo_usuario"].Equals("Usuario") && Session["ID_Usuario"] != null)
            {
                Session["Tipo_usuario"] = null;
                Session["ID_Usuario"] = null;

                return RedirectToAction("/Index");
            }
            return View();
        }

        public ActionResult listarUsuarios()
        {
            if (Session["Tipo_usuario"] != null && Session["Tipo_usuario"].Equals("Admin"))
            {
                //this.AddNotification("El tipo de usuario es: " + Session["Tipo_usuario"], NotificationType.SUCCESS);
        
            } else {
                return RedirectToAction("/Login");
            }

            var lista = controller.listarUsuarios();
            return View(lista);
        }

        public ActionResult informacionUsuario()
        {
            
            if (Session["Tipo_usuario"] != null && Session["Tipo_usuario"].Equals("Usuario") && Session["ID_Usuario"] != null)
            {
                ViewBag.ID_Usuario = Session["ID_Usuario"];

            }
            else
            {
                return RedirectToAction("Index");
            }

            var lista = controller.listarID_Usuario(ViewBag.ID_Usuario);
            return View(lista);
        }


        public ActionResult registrarUsuario()
        {
            if (Session["Tipo_usuario"] != null && Session["Tipo_usuario"].Equals("Admin"))
            {
                //this.AddNotification("El tipo de usuario es: " + Session["Tipo_usuario"], NotificationType.SUCCESS);
            }
            else
            {
                return RedirectToAction("/Index");
            }

            
            return View();
        }

        [HttpPost]
        public ActionResult registrarUsuario(Usuarios usuarios)
        {
            if (!String.IsNullOrEmpty(usuarios.email) && !String.IsNullOrEmpty(usuarios.password) && !String.IsNullOrEmpty(usuarios.tipo)) {
                controller.registrarUsuario(usuarios);
                this.AddNotification("Se ha creado exitosamente el usuario", NotificationType.SUCCESS);
            }
            else {
                this.AddNotification("Debes de llenar los campos", NotificationType.ERROR);
            }
            return View();
        }

        public ActionResult actualizarUsuario(int id)
        {
            if (Session["Tipo_usuario"] != null && Session["Tipo_usuario"].Equals("Admin"))
            {
                //this.AddNotification("El tipo de usuario es: " + Session["Tipo_usuario"], NotificationType.SUCCESS);
            }
            else
            {
                return RedirectToAction("/Index");
            }

            ViewBag.Title = "Home Page";
            var idUsuario = controller.listarID_Usuario(id).FirstOrDefault();
            return View(idUsuario);
        }

        [HttpPost]
        public ActionResult actualizarUsuario(Usuarios usuarios)
        {
            if (!String.IsNullOrEmpty(usuarios.email) && !String.IsNullOrEmpty(usuarios.password) && !String.IsNullOrEmpty(usuarios.tipo))
            {
                controller.actualizarUsuario(usuarios);
                this.AddNotification("Se ha actualizado exitosamente el usuario", NotificationType.SUCCESS);
            }
            else
            {
                this.AddNotification("Debes de llenar los campos", NotificationType.ERROR);
            }
            return View();
        }


        public ActionResult eliminarUsuario(int id)
        {
            if (Session["Tipo_usuario"] != null && Session["Tipo_usuario"].Equals("Admin"))
            {
                //this.AddNotification("El tipo de usuario es: " + Session["Tipo_usuario"], NotificationType.SUCCESS);
            }
            else
            {
                return RedirectToAction("/Login");
            }

            controller.eliminarUsuario(id);
            this.AddNotification("Se ha eliminado exitosamente el usuario", NotificationType.SUCCESS);
            
            return View();
        }

    }
}
