namespace NightStudyDotNetCore.SnakeWebApi.Models
{
    public class SnakeModel
    {
        public int Id { get; set; }
        public string MMName { get; set; }
        public string EngName { get; set; }
        public string Detail { get; set; }
        public string IsPoison { get; set; }
        public string IsDanger{get; set; }
        public string ImageUrl{get; set; }

        public class SnakeViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Detail { get; set; }
            public string IsPoison { get; set; }
            public string PhotoUrl { get; set; }
        }
    }
}