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
     * I assumed that any word can be in a single group of synonyms only.
     * I could improve application if I can change parameters and return type in IThesaurus interface methods.
     * I beleive I can write much better code, but I have not enough time this month.
     * Thank you for your time!
     */

    public class Thesaurus : IThesaurus
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        // to find not allowed characters and symbols in the incoming words.
        Regex regExp = new Regex("[a-zA-Z0-9]{1,}");

        // TODO :
        // to define allowed symbols and to extend regular expressions pattern.

        string tempCollectionDelimiter = " ";

        SqLiteWork db = new SqLiteWork();

        public void AddSynonyms(IEnumerable<string> synonyms)
        {
            try
            {
                if (synonyms.Count() > 0)
                {
                    // Checking for empty collection and not allowed symbols.

                    string collectAsString = string.Join(tempCollectionDelimiter, synonyms);

                    // TODO:
                    // what to do if synonyms collection contains a word with not allowed symbols?
                    if (!string.IsNullOrWhiteSpace(collectAsString) && regExp.IsMatch(collectAsString))
                    {
                        WordSynonims existedWord = db.GetOneIfExists(synonyms);

                        // if exists then update else create new.
                        if (existedWord != null)
                        {
                            var newSynCollection = existedWord.Synonims.Split(' ').ToList();

                            newSynCollection.AddRange(synonyms);
                            newSynCollection = newSynCollection.Distinct().ToList();
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
            if (!string.IsNullOrWhiteSpace(word))
            {
                var oneWord = db.GetOneIfExists(new List<string> { word.Trim() });

                if (oneWord != null)
                {
                    return oneWord.Synonims.Split(' ').ToList<string>();
                }
            }

            return new List<string>();
        }


        public IEnumerable<string> GetWords()
        {
            // TODO:
            // Must be filtered or paging in case of thousands records.

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