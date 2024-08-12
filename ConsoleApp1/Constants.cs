namespace ConsoleApp1
{
    internal class Constants
    {
        public const string Separator = "======================================";

        public const string RequestTitle = "Please enter the book title: ";
        public const string RequestAuthor = "Please enter the book´s author";
        public const string RequestISBN = "Please enter the book´s ISBN code";
        public const string RequestYearPublished = "Please enter the book´s publication year";
        public const string RequestUserId = "Please enter the user ID";
        public const string RequestUserName = "Please enter the user name: ";

        public const string Welcome = "Welcome, please select an option to continue:";
        public const string Options = "1- User Management. \r\n2- Book Management.";
        public const string UserOptions = "1- Add User. \r\n2- Search user by ID. \r\n3- Lend Book. \r\n4- Log book return. \r\n5- Loan history. \r\n\r\n6- Exit.";
        public const string BookOptions = "1- Add Book. \r\n2- Search by book title.\r\n3- List all books. \r\n\r\n4- Exit.";
        public const string YesNo = "1- Yes. \r\n2- No.";

        public const string AddBook = "Do you wish to add another book?";
        public const string AddUser = "Do you wish to add another user?";
        public const string TitleSearch = "Please enter the title of the book you are looking for:";
        public const string IdSearch = "Please enter the ID of the user you are looking for";

        public const string Lended = "Book lended succesfully!";
        public const string Returned = "Book returned succesfully!";
        public const string UserAdded = "User added succesfully";
        public const string BookAdded = "Book added succesfully";

        public const string Error = "Error";
        public const string CantLend = "This book is not available at the moment";
        public const string CantReturn = "You cant return a book that is not lended";
        public const string AlreadyExists = "This {0} already exists in the {1}. Please enter a valid {2} ";
        public const string TypeError = "{0} field must be a {1}";
        public const string EmptyField = "{0} field can not be empty";
        public const string Error404 = "{0} not found";
        public const string NoBookError = "There are no books with the introduced parameters";
        public const string NoUserError = "There are no users with the introduced parameters";
        public const string ExistingUser = "This user already exists";
        public const string NullUser = "User can not be null";

        public const string BookInfoText = "Title: {0} || Author: {1} || ISBN: {2} || Published in year: {3}";
        public const string UserInfoText = "User: {0} || User ID: {1}";
        public const string LoanInfoText = "Book Title: {0} || Book Author: {1} || ISBN: {2} || Year of publication: {3}\r\nUser Name: {4} || User ID: {5} \r\nBorrowed on: {6} \r\nReturned: {7} ";
    }
}
