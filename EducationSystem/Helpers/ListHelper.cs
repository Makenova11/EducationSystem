using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EducationSystem.Models;

namespace EducationSystem.Helpers
{
    /// <summary>
    /// Создаёт HTML разметку в виде упорядоченного списка.
    /// </summary>
    public static class ListHelper
    {
        /// <summary>
        /// Создаёт список заданий предмета со ссылками на задания.
        /// </summary>
        /// <param name="html"> Экземпляр страницы.</param>
        /// <param name="subjCode"> Код предмета, по которому формируется список. </param>
        /// <param name="htmlAttributes"> Атрибуты. </param>
        /// <returns> MvcHtmlString </returns>
        public static MvcHtmlString CreateList( int subjCode, object htmlAttributes = null)
        {
            EducationSystemDB db = new EducationSystemDB();
            SubjectTask[] items = db.SubjectTask.Where(c => c.SubjectCode == subjCode).ToArray();
            TagBuilder ul = new TagBuilder("ul");
            foreach (var item in items)
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder a = new TagBuilder("a");
                a.AddCssClass("link");// в теге <a> делаем ссылку на задания
                a.MergeAttribute("href", $"/Task/Index?subjTaskCode={item.SubjectTaskCode}");
                a.SetInnerText($"Задание {item.Number}");
                li.InnerHtml += a.ToString();
                ul.InnerHtml += li.ToString();
            }
            ul.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            return MvcHtmlString.Create(ul.ToString());
        }
    }
}