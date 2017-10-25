using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesaurus
{
    interface IDbWorker
    {
        WordSynonims GetOneIfExists(string word);

        List<WordSynonims> GetMany();

    }
}
