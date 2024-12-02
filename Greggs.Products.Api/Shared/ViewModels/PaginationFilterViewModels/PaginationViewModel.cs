using Greggs.Products.Api.Shared.Exceptions;

namespace Greggs.Products.Api.Shared.ViewModels.PaginationFilterViewModels
{
    public class PaginationParameters
    {
        private int _pageStart;
        private int _pageSize;

        public int PageStart
        {
            get => _pageStart;
            set
            {
                if (value < 0)
                    throw new ParameterException("PageStart must be non-negative.");
                _pageStart = value;
            }
        }

        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value <= 0)
                    throw new ParameterException("PageSize must be greater than zero.");
                _pageSize = value;
            }
        }

        public bool OrderBy { get; set; } = true; // if true == orderby, if false == orderbydescending
        public string OrderByField { get; set; }
    }

}
