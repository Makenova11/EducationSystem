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
        /// Создаёт список заданий предмета.
        /// </summary>
        /// <param name="html"> Экземпляр страницы.</param>
        /// <param name="subjCode"> Код предмета, по которому формируется список. </param>
        /// <param name="htmlAttributes"> Атрибуты. </param>
        /// <returns> MvcHtmlString </returns>
        public static MvcHtmlString CreateList(/*this HtmlHelper html,*/ int subjCode, object htmlAttributes = null)
        {
            EducationSystemDB db = new EducationSystemDB();
            SubjectTask[] items = db.SubjectTask.Where(c => c.SubjectCode == subjCode).ToArray();
            TagBuilder ul = new TagBuilder("ul");
            foreach (var item in items)
            {
                TagBuilder li = new TagBuilder("li");
                li.SetInnerText($"Задание {item.Number}");
                //ul.AddCssClass("list-inside");
                ul.InnerHtml += li.ToString();
            }
            ul.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            return MvcHtmlString.Create(ul.ToString());
        }
    }
}