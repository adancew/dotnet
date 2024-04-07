using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppForRazorDemo.ViewModels;

namespace WebAppForRazorDemo.DataContext;

    public interface IArticlesContext
    {
        List<ArticleViewModel> GetArticles();
        ArticleViewModel GetArticle(int id);
        void AddArticle(ArticleViewModel article);
        void RemoveArticle(int id);
        void UpdateArticle(ArticleViewModel article);

    }

