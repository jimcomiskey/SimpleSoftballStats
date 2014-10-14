using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleSoftballStats.Model;
using SimpleSoftballStats.DataLayer;

namespace SimpleSoftballStats.Controllers
{
    public class RosterEntriesController : Controller
    {
        private SoftballContext _softballContext;

        public RosterEntriesController()
        {
            _softballContext = new SoftballContext();
        }

        // GET: RosterEntries
        public ActionResult Index()
        {
            var rosterEntries = _softballContext.RosterEntries.Include(r => r.Player).Include(r => r.Team);
            return View(rosterEntries.ToList());
        }

        // GET: RosterEntries/Details/5
        public ActionResult Details(int? PlayerId, int? TeamId)
        {
            if (PlayerId == null || TeamId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RosterEntry rosterEntry = _softballContext.RosterEntries.Find(PlayerId, TeamId);
            if (rosterEntry == null)
            {
                return HttpNotFound();
            }
            return View(rosterEntry);
        }

        public ActionResult AddPlayerToTeam(int TeamId)
        {
            ViewBag.PlayerId = new SelectList(_softballContext.Players, "PlayerId", "FirstName");
            ViewBag.TeamId = TeamId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult AddPlayerToTeam([Bind(Include = "TeamId,PlayerId")] RosterEntry rosterEntry)
        {
            try
                {
                    _softballContext.RosterEntries.Add(rosterEntry);
                    _softballContext.SaveChanges();
                    return RedirectToAction("Details", "Teams", new { Id = rosterEntry.TeamId });
                }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.ToString());
            }

            ViewBag.PlayerId = new SelectList(_softballContext.Players, "PlayerId", "FirstName", rosterEntry.PlayerId);
            ViewBag.TeamId = new SelectList(_softballContext.Teams, "TeamId", "TeamName", rosterEntry.TeamId);
            return View(rosterEntry);
        }

        // GET: RosterEntries/Create
        public ActionResult Create()
        {
            ViewBag.PlayerId = new SelectList(_softballContext.Players, "PlayerId", "FirstName");
            ViewBag.TeamId = new SelectList(_softballContext.Teams, "TeamId", "TeamName");
            return View();
        }        

        // POST: RosterEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamId,PlayerId")] RosterEntry rosterEntry)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _softballContext.RosterEntries.Add(rosterEntry);
                    _softballContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.ToString());
                }                
            }

            ViewBag.PlayerId = new SelectList(_softballContext.Players, "PlayerId", "FirstName", rosterEntry.PlayerId);
            ViewBag.TeamId = new SelectList(_softballContext.Teams, "TeamId", "TeamName", rosterEntry.TeamId);
            return View(rosterEntry);
        }

        // GET: RosterEntries/Edit/5
        public ActionResult Edit(int? TeamId, int? PlayerId)
        {
            if (PlayerId == null || TeamId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RosterEntry rosterEntry = _softballContext.RosterEntries.Find(TeamId, PlayerId);
            if (rosterEntry == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlayerId = new SelectList(_softballContext.Players, "PlayerId", "FirstName", rosterEntry.PlayerId);
            ViewBag.TeamId = new SelectList(_softballContext.Teams, "TeamId", "TeamName", rosterEntry.TeamId);
            return View(rosterEntry);
        }

        // POST: RosterEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamId,PlayerId")] RosterEntry rosterEntry)
        {
            if (ModelState.IsValid)
            {
                _softballContext.Entry(rosterEntry).State = EntityState.Modified;
                _softballContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlayerId = new SelectList(_softballContext.Players, "PlayerId", "FirstName", rosterEntry.PlayerId);
            ViewBag.TeamId = new SelectList(_softballContext.Teams, "TeamId", "TeamName", rosterEntry.TeamId);
            return View(rosterEntry);
        }

        // GET: RosterEntries/Delete/5
        public ActionResult Delete(int? TeamId, int? PlayerId)
        {
            if (PlayerId == null || TeamId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RosterEntry rosterEntry = _softballContext.RosterEntries.Find(TeamId, PlayerId);
            if (rosterEntry == null)
            {
                return HttpNotFound();
            }
            return View(rosterEntry);
        }

        // POST: RosterEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int TeamId, int PlayerId)
        {
            RosterEntry rosterEntry = _softballContext.RosterEntries.Find(TeamId, PlayerId);
            _softballContext.RosterEntries.Remove(rosterEntry);
            _softballContext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _softballContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
