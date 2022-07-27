using HtmlAgilityPack;
using System;
using System.Text.RegularExpressions;
using WebApplication2.IServices;

namespace WebApplication2.Services
{
    public class CourseService : ICourseService

    {
        private const string StartPageLink = "https://www.coinbase.com/ru/converter/btc/uah";
        /// <summary>
        /// Gets bitcoin course
        /// </summary>
        /// <returns>Amount of UAH equivalent to 1 bitcoin</returns>
        public int GetBitcoinToUAH()
        {
            //Весь метод чудово працював під час дебагу, але при запуску у докері отримуємо сторінку з капчею, яку я поки не знаю як обійти
            //Такий проксі-сервер як flaresolverr не підтримував мою архітектуру(
            //Інші методи отримання даних з сайту, як наприклад WebClient дають помилку 403, навевно також з цієї причини
            //нестачі прав доступу, або сторінка розглядає наш запит як бота, тому підсовує капчу
            try
            {
                string input = null;
                var htmlDoc = new HtmlWeb().Load(StartPageLink);
                var rows = htmlDoc.DocumentNode.SelectNodes("//h2[@class='cds-typographyResets-t1xhpuq2 cds-title2-t37r1y cds-foregroundMuted-f1vw1sy6 cds-transition-txjiwsi cds-start-s1muvu8a cds-2-_1xqs9y8 cds-1-_18ml2at']");
                foreach (var cell in rows)
                { input += cell.InnerText; }
                int res = ParseCourse(input);
                return res;
            }
            catch (Exception)
            { return 70000; }

        }



        private int ParseCourse(string s)
        {
            int course;
            Regex rx = new Regex("=(.*?)₴");
            var res = rx.Match(s).Groups[1].Value;
            res = Regex.Replace(res, @"\s+", "");
            course = Convert.ToInt32(Math.Round(Convert.ToDouble(res)));
            return course;

        }
    }
}
