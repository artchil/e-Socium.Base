using System;
using System.Collections;
using System.Collections.Generic;

namespace eSocium
{
    // Interface members are always public
    // Interface can't have fields, but may have properties
    // IEquatable(Of T) should be implemented for any type that might be stored in a generic collection.
    // If you implement IEquatable(Of T), you should also override the base class implementations of Object.Equals(Object) and GetHashCode so that their behavior is consistent with that of the IEquatable(Of T).Equals method.
    // The following guidelines are for implementing a value type: 
    // Consider overriding Equals to gain increased performance over that provided by the default implementation of Equals on ValueType. 
    // If you override Equals and the language supports operator overloading, you must overload the equality operator for your value type. 
    // The following guidelines are for implementing a reference type: 
    // Consider overriding Equals on a reference type if the semantics of the type are based on the fact that the type represents some value(s). 
    // Most reference types must not overload the equality operator, even if they override Equals. However, if you are implementing a reference type that is intended to have value semantics, such as a complex number type, you must override the equality operator. 
    // Interfaces cannot contain operators

    /// <summary>
    /// Represents a pair of elements of different types.
    /// </summary>
    /// <typeparam name="T1">type of the first element</typeparam>
    /// <typeparam name="T2">type of the second element</typeparam>
    public struct Pair<T1, T2> : IEquatable<Pair<T1, T2>>
    {
        public T1 First;
        public T2 Second;

        public Pair(T1 t1, T2 t2)
        {
            First = t1;
            Second = t2;
        }

        public override int GetHashCode()
        {
            return First.GetHashCode() ^ Second.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Pair<T1, T2>))
                return false;
            Pair<T1, T2> rhs = (Pair<T1, T2>)obj;
            return First.Equals(rhs.First) && Second.Equals(rhs.Second);
        }

        public bool Equals(Pair<T1, T2> other)
        {
            return First.Equals(other.First) && Second.Equals(other.Second);
        }

        public static bool operator == (Pair<T1, T2> lhs, Pair<T1, T2> rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Pair<T1, T2> lhs, Pair<T1, T2> rhs)
        {
            return !lhs.Equals(rhs);
        }

        public override string ToString()
        {
            return First.ToString()+","+Second.ToString();
        }
    }

    // may be used for List.Sort and alike
    public class PairLexicographicalComparer<T1,T2> : Comparer<Pair<T1,T2>>
        where T1 : IComparable<T1>
        where T2 : IComparable<T2>
    {
        public override int Compare(Pair<T1, T2> x, Pair<T1, T2> y)
        {
            int result = x.First.CompareTo(y.First);
            if (result == 0)
                return x.Second.CompareTo(y.Second);
            return result;
        }
    }

    namespace PollData
    {
        public interface IRespondent : IEquatable<IRespondent>
        {
            int Id { get; }
            string Name { get; }
            string AtId(int QuestionId);
            string At(int QuestionIndex);
        }

        public interface IQuestion : IEquatable<IQuestion>
        {
            int Id { get; }
            string Wording { get; }
            string AtId(int RespondentId);
            string At(int RespondentIndex);
        }

        public interface IPoll
        {
            string ShortName { get; }
            string LongName { get; }
            string Description { get; }
            DateTime PollDate { get; }
            List<IQuestion> Questions { get; }
            List<IRespondent> Respondents { get; }
            string AtId(int QuestionId, int RespondentId);
            string At(int QuestionIndex, int RespondentIndex);
        }
    }

    namespace NLP
    {
        /// <summary>
        /// Represents a part of speech.
        /// </summary>
        public interface IPOS : IEquatable<IPOS>
        {
        }

        /// <summary>
        /// Represents a paradigm.
        /// There may be different POS in a paradigm.
        /// </summary>
        public interface IParadigm : IEquatable<IParadigm>
        {
            List<IForm> Forms { get; }
            IForm BaseForm { get; }
        }

        /// <summary>
        /// Represents a form.
        /// </summary>
        public interface IForm : IEquatable<IForm>
        {
            IParadigm Paradigm { get; }
            IPOS POS { get; }
            string Prefix { get; }
            string Suffix { get; }
            string InflectStem(string stem);
            string InflectLemma(ILemma lemma);
        }

        /// <summary>
        /// Represents a lemma.
        /// There may be different POS in a lemma.
        /// </summary>
        public interface ILemma : IEquatable<ILemma>
        {
            /// <summary>
            /// Gets the stem of the lemma.
            /// </summary>
            string Stem { get; }
            /// <summary>
            /// Gets the base form of the lemma.
            /// </summary>
            string BaseForm { get; }
            /// <summary>
            /// Gets the paradigm of the lemma.
            /// </summary>
            IParadigm Paradigm { get; }
        }

        /// <summary>
        /// Defines methods to split a text into words.
        /// </summary>
        public interface IWordTokenizer
        {
            /// <summary>
            /// Splits an arbitrary text into words.
            /// </summary>
            /// <param name="text">The input text to be split.</param>
            /// <returns>The list of words.</returns>
            List<string> DivideTextIntoWords(string text);
            List<string> DivideIncoherentTextIntoWords(string text);
            List<string> DivideCoherentTextIntoWords(string text);
            List<string> DivideSentenceIntoWords(string text);
        }

        /// <summary>
        /// Defines methods to split a text into sentences.
        /// </summary>
        public interface ISentenceSegmenter
        {
            List<string> DivideCoherentTextIntoSentences(string text);
        }

        /// <summary>
        /// Defines methods to tag words in a text.
        /// </summary>
        public interface ILFTagger
        {
            /// <summary>
            /// Transforms an arbitrary text into a sequence of lemmas.
            /// </summary>
            /// <param name="text">The input text to be normalized.</param>
            /// <returns>The list of list of lemmas.</returns>
            List<List<Pair<ILemma, IForm>>> TransformTextToLF(string text);
            List<List<Pair<ILemma, IForm>>> TransformIncoherentTextToLF(string text);
            List<List<Pair<ILemma, IForm>>> TransformCoherentTextToLF(string text);
            List<List<Pair<ILemma, IForm>>> TransformSentenceToLF(string text);
            List<Pair<ILemma, IForm>> TransformWordToLF(string word);
        }

        /// <summary>
        /// Defines methods to normalize words.
        /// </summary>
        public interface IWordLemmatizer
        {
            /// <summary>
            /// Returns all the possible lemmas.
            /// </summary>
            /// <param name="word">A word.</param>
            /// <returns>An unordered list of all the possible lemmas.</returns>
            List<ILemma> LemmatizeWord(string word);
            /// <summary>
            /// Returns the base form of the lemma.
            /// </summary>
            /// <param name="lemma">A lemma.</param>
            /// <returns>A word in the base form.</returns>
            string GetBaseForm(ILemma lemma);
        }

        public static class LanguageTest
        {
            // \p{L} - letters (alpha characters)
            // \p{Ll} - lower-case letters
            // \p{Lu} - upper-case letters
            // \p{Lt} - title-case letters
            // \p{N} - digits 0-9 and numeric characters (special Unicode characters U2160-217F, not alpha characters)
            // \p{Nl} - lower-case numeric characters
            // \p{Nd} - digits 0-9
            // \p{Z}
            // \p{IsBoxDrawing}
            // \p{IsGreek}
            // \p{IsCyrillic}
            // \p{IsBasicLatin}
            // \p{Lo}\p{Pc}

            public static readonly System.Text.RegularExpressions.Regex CyrillicCharRegex = new System.Text.RegularExpressions.Regex(@"\p{IsCyrillic}");
            public static readonly System.Text.RegularExpressions.Regex BasicLatinCharRegex = new System.Text.RegularExpressions.Regex(@"\p{IsBasicLatin}");

            public static bool HasCyrillicChars(string text)
            {
                return CyrillicCharRegex.IsMatch(text);
            }
            public static bool HasBasicLatinChars(string text)
            {
                return BasicLatinCharRegex.IsMatch(text);
            }
        }
    }
}
