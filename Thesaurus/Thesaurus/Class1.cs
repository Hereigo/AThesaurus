using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO:
// you shall be able to describe your code and the different
// decisions you made about functionality in the two tasks below.Any reflections, deficiencies, or
// limitations shall also be described. You may use any framework you want.
// Preferably, the solutions should have a personal touch and reflect your mindset regarding good code
// quality.
// 1)  Focus on implementing the IThesaurus interface specified below and then use it in a simple
// 2)  Using WPF, show a MessageBox on button click according to the picture below.Write unit tests
// that verifies this code.

namespace Thesaurus
{
    using System.Collections.Generic;

    namespace Thesaurus
    {
        /// <summary> 
        /// Represents a thesaurus. 
        /// </summary> 
        public interface IThesaurus
        {
            /// <summary> 
            /// Adds the given synonyms to the thesaurus. 
            /// </summary> 
            /// <param name="synonyms">The synonyms to add.</param> 
            void AddSynonyms(IEnumerable<string> synonyms);

            /// <summary> 
            /// Gets the synonyms for a given word. 
            /// </summary> 
            /// <param name="word">The word to return the synonyms for.</param> 
            /// <returns> 
            /// A <see cref="IEnumerable{String}"/> of synonyms. 
            /// </returns> 
            IEnumerable<string> GetSynonyms(string word);

            /// <summary> 
            /// Gets all words from the thesaurus. 
            /// </summary> 
            /// <returns> 
            /// An <see cref="IEnumerable{String}"/> containing all the words in 
            /// the thesaurus. 
            /// </returns> 
            IEnumerable<string> GetWords();
        }
    }
}
