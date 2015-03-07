using System.Collections.Generic;

namespace Data.Searches
{
    public interface IEntitySearch<T> where T : class
    {
        IList<T> Search(string q);
    }

    public class SearchElements
    {
        public const string Email = "email";
    }

    public class SearchKeyWords
    {
        public new const string Equals = "=";
        public const string Contains = "cont";
        public const string GreaterThan = "gt";
        public const string LessThan = "lt";

        public const string Include = "include";

        public const string OrderBy = "orderby";
        public const string Descending = "desc";
        public const string Ascending = "asc";

        public const string Take = "take";

        public const string Skip = "skip";
    }

    public class SearchSyntax
    {
        public const string StatementSeperator = ",";
        public const string Seperator = "_";
    }
}
