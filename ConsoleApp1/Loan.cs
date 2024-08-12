namespace ConsoleApp1
{
    public class Loan
    {
        public User User { get; set; }
        public Book Book { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public Loan (User user, Book book, DateTime loanDate)
        {
            User = user;
            Book = book;
            LoanDate = loanDate;
        }
    }
}
