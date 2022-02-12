using System.Linq;
using System.Web.Mvc;
using EducationSystem.Models;

namespace EducationSystem.Helpers
{
    /// <summary>
    ///     Создаёт HTML разметку в виде упорядоченного списка.
    /// </summary>
    public static class ListHelper
    {
        /// <summary>
        ///     Создаёт список заданий предмета со ссылками на задания.
        ///     Subject.
        /// </summary>
        /// <param name="html"> Экземпляр страницы.</param>
        /// <param name="subjCode"> Код предмета, по которому формируется список. </param>
        /// <param name="htmlAttributes"> Атрибуты. </param>
        /// <returns> MvcHtmlString </returns>
        public static MvcHtmlString CreateSubjectList(int subjCode, object htmlAttributes = null)
        {
            var db = new EducationSystemDB();
            var items = db.SubjectTask.Where(c => c.SubjectCode == subjCode).ToArray();
            var ul = new TagBuilder("ul");
            foreach (var item in items)
            {
                var li = new TagBuilder("li");
                var a = new TagBuilder("a");
                a.AddCssClass("link"); // в теге <a> делаем ссылку на задания
                a.MergeAttribute("href", $"/Task/Index?subjTaskCode={item.SubjectTaskCode}");
                a.SetInnerText($"Задание {item.Number}");
                li.InnerHtml += a.ToString();
                ul.InnerHtml += li.ToString();
                db.Dispose();
            }

            ul.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            return MvcHtmlString.Create(ul.ToString());
        }

        /// <summary>
        ///     Создаёт ссылку для выбора задания в Task.
        /// </summary>
        /// <param name="arrayNumber"> Номер задания (очерёдность в списке)</param>
        /// <param name="subjTaskCode"> Код номера задания </param>
        /// <returns> MvcHtmlString. </returns>
        public static MvcHtmlString CreateLink(int arrayNumber, int subjTaskCode)
        {
            var db = new EducationSystemDB();
            //Ищем код задания.
            var TaskList = from item in db.Task
                where item.SubjectTaskCode == subjTaskCode
                orderby item.TaskCode descending
                select item;
            var TaskCode = TaskList.ToList()[--arrayNumber].SubjectTaskCode;
            var a = new TagBuilder("a");
            a.AddCssClass("link"); // в теге <a> делаем ссылку на задание(task).
            a.MergeAttribute("href", $"/Solution/Index?TaskCode={TaskCode}");
            a.SetInnerText($"{++arrayNumber}");
            return MvcHtmlString.Create(a.ToString());
        }

        /// <summary>
        ///     Создаёт ссылку для SolutionTest, переход на обучение.
        /// </summary>
        /// <param name="arrayNumber"> Номер задания (очерёдность в списке).</param>
        /// <param name="TaskCode"> Код задания.</param>
        /// <returns></returns>
        public static MvcHtmlString CreateSolutionLink(int arrayNumber, int TaskCode)
        {
            var db = new EducationSystemDB();
            //Ищем нужный нам SolutionCode.
            var SolutionCodeList = db.Solution.Where(x => x.TaskCode == TaskCode).ToList();
            var solutionCode = SolutionCodeList[--arrayNumber].SolutionCode;
            TagBuilder a = new TagBuilder("a");
            a.AddCssClass("link"); // в теге <a> делаем ссылку на задание(solution)
            a.MergeAttribute("href", $"/Solution/SolutionTest?SolutionCode={solutionCode}");
            a.SetInnerText($"{++arrayNumber}");
            return MvcHtmlString.Create(a.ToString());
        }
    }
}