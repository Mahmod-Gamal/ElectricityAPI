namespace ElectricityAPI.Models
{
    public class Result
    {
        public int GovID { get; set; }

        public string GovName { get; set; }

        public int CityID { get; set; }

        public string CityName { get; set; }

        public int AreaID {  get; set; }

        public string AreaName { get; set; }

        public int HourFrom { get; set; }

        public int MinuteFrom { get; set; }

        public int HourTo { get; set;}
        public int MinuteTo { get; set; }
    }

    public class PagedResult
    {
        public IEnumerable<Result> Result { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
    }
}
