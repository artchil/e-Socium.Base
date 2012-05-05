using System;
using System.Collections.Generic;
using System.Text;

namespace eSocium.NLP
{
    public class SnowballLemma : ILemma
    {
        public string Stem { get; private set; }

        public SnowballLemma(string stem)
        {
            Stem = stem;
        }

        public IParadigm Paradigm
        {
            get { return null; }
        }

        public bool Equals(ILemma other)
        {
            return (other is SnowballLemma) && this.Stem == other.Stem;
        }

        public override bool Equals(object obj)
        {
            return (obj is SnowballLemma) && this.Stem == ((SnowballLemma)obj).Stem;
        }

        public override int GetHashCode()
        {
            return Stem.GetHashCode();
        }

        public override string ToString()
        {
            return Stem;
        }

        public string BaseForm
        {
            get { throw new NotImplementedException(); }
        }
    }

    public class SnowballStemmer : IWordLemmatizer
    {
        public static readonly Iveonik.Stemmers.RussianStemmer IveonikRussianStemmer = new Iveonik.Stemmers.RussianStemmer();
        public static readonly Iveonik.Stemmers.EnglishStemmer IveonikEnglishStemmer = new Iveonik.Stemmers.EnglishStemmer();

        public IList<ILemma> LemmatiseWord(string word)
        {
            string arg = word.ToLowerInvariant().Trim();
            List<SnowballLemma> result = new List<SnowballLemma>();
            if (LanguageTest.HasCyrillicChars(arg))
            {
                string s = IveonikRussianStemmer.Stem(arg);
                result.Add(new SnowballLemma(s));
            }
            else if (LanguageTest.HasBasicLatinChars(arg))
            {
                string s = IveonikEnglishStemmer.Stem(arg);
                result.Add(new SnowballLemma(s));
            }
            return (IList<ILemma>)result;
        }

        public string GetBaseForm(ILemma lemma)
        {
            throw new NotImplementedException();
        }
    }

}
