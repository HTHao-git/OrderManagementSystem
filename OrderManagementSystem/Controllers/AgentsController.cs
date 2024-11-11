using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using OrderManagementSystem.Controllers;
using OrderManagementSystem.DAL;
using OrderManagementSystem.Models;

public class AgentsController : BaseController
{
    private OrderManagementContext db = new OrderManagementContext();

    // GET: Agents
    public ActionResult Index()
    {
        return View(db.Agents.ToList());
    }

    // GET: Agents/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: Agents/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "AgentName,Address")] Agent agent)
    {
        if (ModelState.IsValid)
        {
            db.Agents.Add(agent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(agent);
    }

    // GET: Agents/Edit/5
    public ActionResult Edit(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Agent agent = db.Agents.Find(id);
        if (agent == null)
        {
            return HttpNotFound();
        }
        return View(agent);
    }

    // POST: Agents/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "AgentID,AgentName,Address")] Agent agent)
    {
        if (ModelState.IsValid)
        {
            db.Entry(agent).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(agent);
    }

    // GET: Agents/Delete/5
    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Agent agent = db.Agents.Find(id);
        if (agent == null)
        {
            return HttpNotFound();
        }
        return View(agent);
    }

    // POST: Agents/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        Agent agent = db.Agents.Find(id);
        db.Agents.Remove(agent);
        db.SaveChanges();
        return RedirectToAction("Index");
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