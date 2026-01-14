namespace SixteenClothing.App.ViewModels.Pagination
{
    public class PaginationViewModel<T> where T : class
    {
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public PaginationViewModel(List<T> items, int count, int pageIndex, int pageSize)
        {
            Items = items;
            PageIndex = pageIndex;
            TotalPages = ((int)Math.Ceiling(count / (double)pageSize));
        }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
    }
}
