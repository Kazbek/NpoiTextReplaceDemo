using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;

namespace NpoiTextReplaceDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Dictionary<string, string> replacers = new Dictionary<string, string>
            {
                {"$FINALQUALIFYINGWORK_QUESTION_1_ASKING_SHORT$", "Asking1" },
                {"$FINALQUALIFYINGWORK_QUESTION_1_QUESTION$", "Question1" },
                {"$FINALQUALIFYINGWORK_QUESTION_2_ASKING_SHORT$", "Asking2" },
                {"$FINALQUALIFYINGWORK_QUESTION_2_QUESTION$", "Question2" },

            };

            XWPFDocument doc;

            MemoryStream stream = new MemoryStream();
            using (Stream fileStream = File.Open(@"C:\TestTemplate.docx", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                doc = new XWPFDocument(fileStream);
                fileStream.Close();
            }

            foreach (var p in doc.Paragraphs)
            {
                if (p.Text == null)
                    continue;

                foreach (var pair in replacers)
                    if (p.Text.Contains(pair.Key))
                        p.ReplaceText(pair.Key, pair.Value);

            }

            doc.Write(stream);

            using (var fileStream = File.Create("C:\\Output\\result.docx"))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(fileStream);
            }


        }
    }
}
