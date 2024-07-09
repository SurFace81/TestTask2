namespace WebApplication1.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class UserFood
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int FoodId { get; set; }
        public DateTime Date { get; set; }
    }

}