namespace BLL.Model.Book
{
    public class BookFilters
    {
        public string[]? Genres { get; set; }

        public GenreSelectLogic GenreSelectLogic { get; set; } = GenreSelectLogic.And;

        public StatusFilter Status { get; set; } = StatusFilter.All;

        public OrderByFilter OrderBy { get; set; } = OrderByFilter.ChapterCount;

        public int? Rating { get; set; }

        public bool isLowerThanRating { get; set; }
    }

    public enum GenreSelectLogic
    {
        And,
        Or
    }

    public enum StatusFilter
    {
        All,
        Completed,
        Ongoing
    }

    public enum OrderByFilter
    {
        ChapterCount,
        Title,
        ReviewCount,
        BookmarkCount,
        RatingScore
    }
}
