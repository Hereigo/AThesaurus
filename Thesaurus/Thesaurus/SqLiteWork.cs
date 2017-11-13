using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesaurus
{
    internal class SqLiteWork : IDbWorker
    {
        private const string dbStorage = "thesaurus.db";
        private const string synonymNounsTable = "SynonymNouns";
        private const string synonymsDelimiter = "_";

        public SqLiteWork()
        {
            InitDatabase();
        }

        // TODO:
        // too much code-duplicates!
        // must be refactored!
        // i am sorry. I had not enough time.
        // TODO:

        public List<WordSynonims> GetMany()
        {
            List<WordSynonims> synonyms = new List<WordSynonims>();

            string selectCmd = "SELECT * FROM " + synonymNounsTable + "";

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + dbStorage + "; Version=3;"))
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    SQLiteCommand command = conn.CreateCommand();
                    command.CommandText = selectCmd;

                    SQLiteDataReader rdr = command.ExecuteReader();

                    while (rdr.Read())
                    {
                        int id = rdr.GetInt32(0);
                        if (id != 0)
                        {
                            synonyms.Add(new WordSynonims
                            {
                                Id = id,
                                Description = rdr.GetString(1),
                                Synonims = rdr.GetString(2)
                            });
                        }
                    }
                }
                conn.Close();
            }
            return synonyms;
        }


        internal void InsertNewSynonyms(string newSynonims)
        {
            string insertCmd = "INSERT INTO " + synonymNounsTable + " (description, synonyms) VALUES ('No description yet!', '" + newSynonims + "')";

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + dbStorage + "; Version=3;"))
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    SQLiteCommand command = conn.CreateCommand();
                    command.CommandText = insertCmd;
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }


        internal void UpdateSynonymsFor(int id, string newSynonyms)
        {
            string updateCmd = "UPDATE " + synonymNounsTable + " SET synonyms=:NewSyn WHERE id=:Id";

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + dbStorage + "; Version=3;"))
            {
                conn.Open();

                if (conn.State == ConnectionState.Open)
                {
                    SQLiteCommand command = conn.CreateCommand();
                    command.CommandText = updateCmd;
                    command.Parameters.Add("Id", DbType.Int32).Value = id;
                    command.Parameters.Add("NewSyn", DbType.String).Value = newSynonyms;
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }


        public WordSynonims GetOneIfExists(string word)
        {
            WordSynonims synonyms = null;

            string selectCmd = "SELECT * FROM " + synonymNounsTable +
                " WHERE synonyms LIKE '% " + word + " %'";

            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + dbStorage + "; Version=3;"))
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    SQLiteCommand command = conn.CreateCommand();
                    command.CommandText = selectCmd;

                    SQLiteDataReader rdr = command.ExecuteReader();

                    while (rdr.Read() && synonyms == null)
                    {
                        int id = rdr.GetInt32(0);
                        if (id != 0)
                        {
                            synonyms = new WordSynonims
                            {
                                Id = id,
                                Description = rdr.GetString(1),
                                Synonims = rdr.GetString(2)
                            };
                        }
                    }
                }
                conn.Close();
            }
            return synonyms;
        }


        // Lunch while start application.
        private void InitDatabase()
        {
            if (!File.Exists(dbStorage))
            {
                // Launching few examples for empty database.
                string createCmd = "CREATE TABLE " + synonymNounsTable +
                              " (" +
                              "[id] integer PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                              "[description] text(250) NOT NULL, " +
                              "[synonyms] text(10000) NOT NULL" +
                              ");" +
                              "INSERT INTO " + synonymNounsTable +
                              " (description, synonyms) VALUES " +
                              "('a vehicle for traveling through the air that has fixed wings for lift', " +
                              "' airplane aeroplane plane '), " +
                              "('a large human settlement with systems for housing, transportation, sanitation, utilities, and communication', " +
                              "' city metropolis town '), " +
                              "('a mixture of clay, sand and organic matter present on the surface of the Earth and serving as substrate for plant growth and micro-organisms development', " +
                              "' earth ground land soil ')";

                using (SQLiteConnection conn =
                    new SQLiteConnection("Data Source=" + dbStorage + "; Version=3;"))
                {
                    conn.Open();

                    if (conn.State == ConnectionState.Open)
                    {
                        SQLiteCommand command = conn.CreateCommand();
                        command.CommandText = createCmd;
                        command.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
        }
    }
}