using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        Logger logger = LogManager.GetCurrentClassLogger();

        // to find not allowed characters and symbols in the incoming words.
        Regex regExp = new Regex("[a-zA-Z0-9]{1,}");
        // TODO :
        // may be should to extend regular expressions pattern.

        string collectiondelimiter = " ";

        SqLiteWork db = new SqLiteWork();

        public void AddSynonyms(IEnumerable<string> synonyms)
        {
            try
            {
                if (synonyms.Count() > 0)
                {
                    string collectAsString = string.Join(collectiondelimiter, synonyms);

                    // check for empty collection and not allowed symbols.
                    // TODO:
                    // what to do if synonyms collection contains a word with not allowed symbols?
                    if (!string.IsNullOrWhiteSpace(collectAsString) && regExp.IsMatch(collectAsString))
                    {
                        WordSynonims existedWord = db.GetOneIfExists(synonyms);

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
                            db.UpdateSynonymsForId(existedWord.Id, newSynonims);
                        }
                        else
                        {
                            // TODO:
                            // check for description!
                            string newSynonyms = " " + String.Join(" ", synonyms.ToArray()) + " ";
                            db.InsertNewSynonyms(newSynonyms.Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"AddSynonyms to db : {ex.Message}");
            }
        }


        public IEnumerable<string> GetSynonyms(string word)
        {
            var oneWord = db.GetOneIfExists(new List<string> { word });

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