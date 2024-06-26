﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppForRazorDemo.DataContext;
using WebAppForRazorDemo.ViewModels;

namespace lista9.Controllers
{

    public class ArticleController : Controller
    {
        // remeber the context for an action
        private IArticlesContext _articlesContext;

        // injection of IDataContext 
        public ArticleController(IArticlesContext articlesContext)
        {
            this._articlesContext = articlesContext;
        }


        // GET: ArticleController
        public ActionResult Index()
        {
            return View(_articlesContext.GetArticles());
        }

        // GET: ArticleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ArticleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArticleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleViewModel article)
        {
            try
            {
                if (ModelState.IsValid)                     // added
                    _articlesContext.AddArticle(article);  // added
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArticleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_articlesContext.GetArticle(id));
        }

        // POST: ArticleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ArticleViewModel article)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    article.Id = id; // added
                    _articlesContext.UpdateArticle(article); //added
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArticleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_articlesContext.GetArticle(id));
        }

        // POST: ArticleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ArticleViewModel article)
        {
            try
            {
                _articlesContext.RemoveArticle(id); // zmiana
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
