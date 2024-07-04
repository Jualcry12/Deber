using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MaestroDetalleMVC.Models;
using MaestroDetalleMVC.Models.ViewModels;

namespace MaestroDetalleMVC.Controllers
{
    public class MaestroDetalleController : Controller
    {
        // GET: MaestroDetalle
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(VentaViewModel model)
        {
            try
            {
                using (var db = new MastroDetalleMVCEntities1())
                {
                    var oVenta = new venta
                    {
                        fecha = DateTime.Now,
                        cliente = model.Cliente
                    };
                    db.venta.Add(oVenta);
                    db.SaveChanges();

                    if (model.conceptos != null)
                    {
                        foreach (var oC in model.conceptos)
                        {
                            var oConcepto = new concepto
                            {
                                cantidad = oC.Cantidad,
                                nombre = oC.Nombre,
                                precioUnitario = oC.PrecioUnitario,
                                total = oC.Cantidad * oC.PrecioUnitario,
                                idVenta = oVenta.id
                            };
                            db.concepto.Add(oConcepto);
                        }

                        db.SaveChanges();
                    }
                }

                ViewBag.Message = "Registro Exitoso";
                return View();
            }
            catch (Exception ex)
            {
                // Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Error al guardar los datos: " + ex.Message);
                return View(model);
            }
        }
    }
}
