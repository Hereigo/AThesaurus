using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesaurus
{
    /* 
     * Esteemed experts,
     * my name is Andrew Plakhtiy.
     * I could improve application if I can change 
     * parameters and return type in IThesaurus interface methods. 
     * Thank you for your time!
     */

    public class Thesaurus : IThesaurus
    {
        SqLiteWork db = new SqLiteWork();

        public void AddSynonyms(IEnumerable<string> synonyms)
        {
            WordSynonims existedWord = null;

            foreach (var syn in synonyms)
            {
                if (existedWord == null)
                {
                    existedWord = db.GetOneIfExists(syn);
                }
            }

            // if exists then update else create new.
            if (existedWord != null)
            {
                var newSynCollection = existedWord.Synonims.Split(' ').ToList();
                // TODO:
                // check for duplicates!
                // TODO:
                newSynCollection.AddRange(synonyms);
                newSynCollection.Sort();
                // TODO:
                // escaping with spaces must be refactored!
                // TODO:
                string newSynonims = " " + String.Join(" ", newSynCollection.ToArray()) + " ";
                db.UpdateSynonymsFor(existedWord.Id, newSynonims);
            }
            else
            {
                // TODO:
                // check for description!
                string newSynonyms = " " + String.Join(" ", synonyms.ToArray()) + " ";
                db.InsertNewSynonyms(newSynonyms);
            }

        }


        public IEnumerable<string> GetSynonyms(string word)
        {
            var oneWord = db.GetOneIfExists(word);
            if (oneWord != null)
            {
                return oneWord.Synonims.Split(' ').ToList<string>();
            }
            return new List<string>();
        }


        public IEnumerable<string> GetWords()
        {
            // TODO:
            // Must be filtered in case of thousands records.
            var manyWords = db.GetMany();

            List<string> all = new List<string>();

            foreach (var item in manyWords)
            {
                foreach (var syn in item.Synonims.Split(' '))
                {
                    all.Add(syn);
                }
            }
            all.Sort();
            return all;
        }
    }
}