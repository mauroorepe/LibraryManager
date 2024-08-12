namespace ConsoleApp1
{
    public class Response
    {
        public int Status { get; set; }
        public string? Message { get; set; } //? en el tipo es declararlo nulleable
        public Book? Book { get; set; }
        public User? User { get; set; }
    }
}