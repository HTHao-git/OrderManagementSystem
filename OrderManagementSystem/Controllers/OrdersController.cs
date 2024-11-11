using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OrderManagementSystem.Controllers;
using OrderManagementSystem.DAL;
using OrderManagementSystem.Models;
using OrderManagementSystem.ViewModels;

public class OrdersController : BaseController
{
    private OrderManagementContext db = new OrderManagementContext();

    // GET: Orders
    public ActionResult Index()
    {
        var orders = db.Orders
            .Include(o => o.Agent)
            .Include(o => o.OrderDetails)
            .OrderByDescending(o => o.OrderDate)
            .ToList();
        return View(orders);
    }

    // GET: Orders/Create
    public ActionResult Create()
    {
        ViewBag.AgentID = new SelectList(db.Agents, "AgentID", "AgentName");
        ViewBag.Items = new SelectList(db.Items, "ItemID", "ItemName");
        return View(new OrderViewModel());
    }

    // POST: Orders/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(OrderViewModel viewModel)
    {
        if (ModelState.IsValid && viewModel.OrderDetails != null && viewModel.OrderDetails.Any())
        {
            var order = new Order
            {
                OrderDate = DateTime.Now,
                AgentID = viewModel.AgentID
            };

            order.OrderDetails = new List<OrderDetail>();
            foreach (var detail in viewModel.OrderDetails)
            {
                order.OrderDetails.Add(new OrderDetail
                {
                    ItemID = detail.ItemID,
                    Quantity = detail.Quantity,
                    UnitAmount = detail.UnitAmount
                });
            }

            db.Orders.Add(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.AgentID = new SelectList(db.Agents, "AgentID", "AgentName");
        ViewBag.Items = new SelectList(db.Items, "ItemID", "ItemName");
        return View(viewModel);
    }

    // GET: Orders/Details/5
    public ActionResult Details(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        var order = db.Orders
            .Include(o => o.Agent)
            .Include(o => o.OrderDetails.Select(od => od.Item))
            .FirstOrDefault(o => o.OrderID == id);

        if (order == null)
        {
            return HttpNotFound();
        }

        return View(order);
    }

    // GET: Orders/Delete/5
    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        var order = db.Orders
            .Include(o => o.Agent)
            .Include(o => o.OrderDetails)
            .FirstOrDefault(o => o.OrderID == id);

        if (order == null)
        {
            return HttpNotFound();
        }

        return View(order);
    }

    // POST: Orders/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        var order = db.Orders
            .Include(o => o.OrderDetails)
            .FirstOrDefault(o => o.OrderID == id);

        if (order != null)
        {
            db.OrderDetails.RemoveRange(order.OrderDetails);
            db.Orders.Remove(order);
            db.SaveChanges();
        }

        return RedirectToAction("Index");
    }

    // API endpoint to get item price
    [HttpGet]
    public JsonResult GetItemPrice(int itemId)
    {
        var item = db.Items.Find(itemId);
        if (item != null)
        {
            return Json(new { price = item.Price }, JsonRequestBehavior.AllowGet);
        }
        return Json(null);
    }

    public ActionResult Print(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        var order = db.Orders
            .Include(o => o.Agent)
            .Include(o => o.OrderDetails.Select(od => od.Item))
            .FirstOrDefault(o => o.OrderID == id);

        if (order == null)
        {
            return HttpNotFound();
        }

        return View(order);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            db.Dispose();
        }
        base.Dispose(disposing);
    }
}
