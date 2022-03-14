using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Capa_Presentacion.Models;

namespace Capa_Presentacion.Controllers
{
    public class UsuariosController : ApiController
    {

        [HttpGet]
        public List<ListarUsuario_Result> listarUsuarios() {
            using (PuestoTrabajoEntities db = new PuestoTrabajoEntities()) {
                return db.ListarUsuario().ToList();
            }
        }

        [HttpGet]
        public List<ListarID_Usuario_Result> listarID_Usuario(int id) {
            using (PuestoTrabajoEntities db = new PuestoTrabajoEntities()) {
                return db.ListarID_Usuario(id).ToList();
            }
        }

        [HttpPost]
        public HttpResponseMessage registrarUsuario(Usuarios usuarios) {

            try
            {
                using (PuestoTrabajoEntities db = new PuestoTrabajoEntities())
                {
                    db.AgregarUsuario(usuarios.email, usuarios.password, usuarios.tipo);
                    db.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, usuarios);
                    message.Headers.Location = new Uri(Request.RequestUri.ToString());
                    return message;

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
            

        [HttpDelete]
        public HttpResponseMessage eliminarUsuario(int id) {

            try {

                using (PuestoTrabajoEntities db = new PuestoTrabajoEntities()) {
                    var user = db.ListarID_Usuario(id).ToList();

                    if (user == null) {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "El usuario con ID " + id.ToString() + " no existe");
                    }
                    else {
                        db.EliminarUsuario(id);
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        [HttpPut]
        public HttpResponseMessage actualizarUsuario([FromBody] Usuarios usuarios) {

            try {
                
                using (PuestoTrabajoEntities db = new PuestoTrabajoEntities()) {
                    var user = db.ListarID_Usuario(usuarios.idUsuario).ToList();

                    if (user == null) {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "El usuario con ID " + usuarios.idUsuario.ToString() + " no existe");
                    }
                    else {
                        db.ActualizarUsuario(usuarios.idUsuario, usuarios.email, usuarios.password, usuarios.tipo);
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, user);
                    }
                }
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        public List<SP_Validar_Usuario_Result> Loguearse(Usuarios usuarios)
        {
            using (var db = new PuestoTrabajoEntities())
            {
                return db.SP_Validar_Usuario(usuarios.email, usuarios.password).ToList();
            }
        }

        public List<string> ValidarTipoUsuario(Usuarios usuarios)
        {
            using (var db = new PuestoTrabajoEntities())
            {
                return db.SP_ValidarTipoUsuario(usuarios.email).ToList();
            }
        }

        public int ObtenerID_Usuario(Usuarios usuarios)
        {
            using (var db = new PuestoTrabajoEntities())
            {
                return int.Parse(db.listarUsuario_email(usuarios.email).FirstOrDefault().ToString());
            }
        }
    }
}
