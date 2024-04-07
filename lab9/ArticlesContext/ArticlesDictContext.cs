using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppForRazorDemo.ViewModels;

namespace WebAppForRazorDemo.DataContext;

    public class ArticlesDictContext : IArticlesContext
    {
        Dictionary<int, ArticleViewModel> articles = new Dictionary<int, ArticleViewModel>() {
            { 0, new ArticleViewModel(0,"Eggs", 10.96f, Category.Food, new DateTime(2023, 12, 12)) },
            { 1,  new ArticleViewModel(1,"Socks", 12.50f, Category.Clothing, new DateTime(2025, 12, 24)) }
        };
       

        public void AddArticle(ArticleViewModel article)
        {
            int nextNumber=articles.Max(s => s.Key)+1;
            article.Id = nextNumber;
            articles.Add(nextNumber, article);
        }

        public ArticleViewModel GetArticle(int id)
        {
            return articles.Values.FirstOrDefault(s => s.Id == id);
        }

        public List<ArticleViewModel> GetArticles()
        {
            return articles.Values.ToList(); // or make a clone of list of students
        }

       
        public void RemoveArticle(int id)
        {
            ArticleViewModel articleToRemove = articles.Values.FirstOrDefault(s => s.Id == id);
            if(articleToRemove != null)
            articles.Remove(articleToRemove.Id);
        }

        public void UpdateArticle(ArticleViewModel person)
        {
            ArticleViewModel articleToUpdate = articles.Values.FirstOrDefault(s => s.Id == person.Id);

            articles[articleToUpdate.Id] = 
                articles.Values.Select(s => (s.Id == person.Id) ? person : s).ToList()[0];
        }
    }

