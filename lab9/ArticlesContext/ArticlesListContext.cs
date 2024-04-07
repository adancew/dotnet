using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppForRazorDemo.ViewModels;

namespace WebAppForRazorDemo.DataContext;

    public class ArticlesListContext : IArticlesContext
    {
        List<ArticleViewModel> articles = new List<ArticleViewModel>() {
            new ArticleViewModel(0,"Eggs", 10.96f, Category.Food, new DateTime(2023, 12, 12)),
            new ArticleViewModel(1,"Socks", 12.50f, Category.Clothing, new DateTime(2025, 12, 24))
        };
       

        public void AddArticle(ArticleViewModel article)
        {
            int nextNumber=articles.Max(s => s.Id)+1;
            article.Id = nextNumber;
            articles.Add(article);
        }

        public ArticleViewModel GetArticle(int id)
        {
            return articles.FirstOrDefault(s => s.Id == id);
        }

        public List<ArticleViewModel> GetArticles()
        {
            return articles;
        }

        public void RemoveArticle(int id)
        {
            ArticleViewModel articleToRemove = articles.FirstOrDefault(s => s.Id == id);
            if(articleToRemove != null)
            articles.Remove(articleToRemove);
        }

        public void UpdateArticle(ArticleViewModel person)
        {
            ArticleViewModel articleToUpdate = articles.FirstOrDefault(s => s.Id == person.Id);
            articles = articles.Select(s => (s.Id == person.Id) ? person:s).ToList();
        }
    }

