namespace ConsoleApp1
{
    public class Library
    {
        public List<Book> Books = new List<Book>();

        public List<User> Users = new List<User>();

        public List<Loan> Loans = new List<Loan>();

        public void AddBook(Book book)
        {
            Books.Add(book);
        }
        public void AddUser(User user)
        {
                Users.Add(user);
        }
        public void AddLoan(Loan loan)
        {
            Loans.Add(loan);
        }
        public bool Exists(int isbn)
        {
            foreach (var book in Books)
            {
                if (book.ISBN == isbn)
                {
                    return true;

                }

            }
            return false;
        }
    }
}