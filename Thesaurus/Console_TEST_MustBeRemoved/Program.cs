using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesaurus;

namespace Console_TEST_MustBeRemoved
{
    class Program
    {
        static void Main(string[] args)
        {
            IThesaurus the = new Thesaurus.Thesaurus();

            string pattern = "city";

            Console.WriteLine("Insert NEW : aaa, bbb");

            the.AddSynonyms(new List<string> { "aaa", "bbb" });

            Console.WriteLine("looking for =" + pattern + "= \n");
            foreach (var item in the.GetSynonyms(pattern))
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("\n\n SELECT ALL : \n");
            foreach (var item in the.GetWords())
            {
                Console.WriteLine(item);
            }
        }
    }
}
