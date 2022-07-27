using System;
using System.Collections.Generic;
using System.IO;

namespace WebApplication2.___Repository___
{
    public class MyRepository
    {

        public void AddEmail(string email, string filename = "emails.txt")
        {
            using (StreamWriter sw = File.AppendText(filename))
            {
                sw.WriteLine(email);
            }
        }
        public List<string> GetAllEmails(string filename = "emails.txt")
        {
            List<string> res = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    var file = reader.ReadToEnd();
                    var split = file.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    res.AddRange(split);
                }
            }
            catch (FileNotFoundException)
            {
                res.Add("File not found");
            }

            return res;
        }
    }
}
