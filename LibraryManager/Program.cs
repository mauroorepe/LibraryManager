using LibraryManager;

internal class Program
{
    private static void Main(string[] args)
    {
        Library library = new Library();
        bool on = true;

        while (on)
        {
            Console.Clear();
            Console.Title = "Library Management";
            Console.WriteLine(Constants.Welcome);
            Console.WriteLine();
            Console.WriteLine(Constants.Options);
            Console.WriteLine(Constants.Separator);
            var input = Console.ReadLine();
            var action = 0;
            if (int.TryParse(input, out action))
            {
                switch (action)
                {
                    case 1:
                        Console.WriteLine(Constants.Separator);
                        Console.WriteLine(Constants.UserOptions);
                        Console.WriteLine(Constants.Separator);
                        input = Console.ReadLine();
                        Console.WriteLine(Constants.Separator);

                        if (int.TryParse(input, out action))
                        {
                            switch (action)
                            {
                                case 1:
                                    //Add user logic
                                    int repeat;
                                    do
                                    {
                                        var response = ValidateUser();

                                        if (response.Status == 200 && response.User != null)
                                        {
                                            library.AddUser(response.User);
                                        }
                                        else
                                            Console.WriteLine(response.Message);

                                        Console.WriteLine(Constants.Separator);
                                        Console.WriteLine(Constants.AddUser);

                                        Console.WriteLine(Constants.YesNo);
                                        Console.WriteLine(Constants.Separator);
                                        input = Console.ReadLine();
                                        int.TryParse(input, out repeat);
                                        Console.WriteLine(Constants.Separator);

                                    } while (repeat == 1);

                                    break;
                                case 2:
                                    //Search user by ID logic
                                    Console.WriteLine(Constants.IdSearch);

                                    string? id = Console.ReadLine();

                                    PrintUsers(id!);
                                    Console.ReadKey();

                                    break;
                                case 3:
                                    //Lend Book logic
                                    Console.WriteLine(Constants.RequestUserId);

                                    input = Console.ReadLine();

                                    int.TryParse(input, out var userId);

                                    Console.WriteLine(Constants.RequestISBN);

                                    input = Console.ReadLine();

                                    int.TryParse(input, out var isbn);

                                    var bookResponse = BookSearch(isbn);

                                    var UserResponse = UserSearch(userId);

                                    var Available = ValidateLoan(bookResponse, UserResponse);

                                    if (bookResponse.Status == 200 && UserResponse.Status == 200 && Available.Status == 200)
                                    {
                                        Loan loan = new Loan(UserResponse.User!, bookResponse.Book!, DateTime.Today);
                                        library.AddLoan(loan);
                                        Console.WriteLine(Constants.Lended);
                                        Console.WriteLine(Constants.Separator);
                                        Console.ReadKey();
                                    }
                                    else
                                    {
                                        Console.WriteLine(Constants.Separator);
                                        Console.WriteLine(Constants.CantLend);
                                        Console.ReadKey();
                                    }



                                    break;
                                case 4:
                                    //log return logic
                                    Console.WriteLine(Constants.RequestISBN);

                                    input = Console.ReadLine();

                                    int.TryParse(input, out isbn);

                                    bookResponse = BookSearch(isbn);

                                    if (bookResponse.Status == 200 && library.Loans.Any(l => l.Book == bookResponse.Book && l.ReturnDate == null))
                                    {
                                        foreach (var loan in library.Loans)
                                        {
                                            if (loan.Book.ISBN == isbn)
                                            {
                                                loan.ReturnDate = DateTime.Today;
                                                Console.WriteLine(Constants.Separator);
                                                Console.WriteLine(Constants.Returned);
                                                Console.ReadKey();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine(Constants.Separator);
                                        Console.WriteLine(Constants.CantReturn);
                                        Console.ReadKey();
                                    }


                                    break;
                                case 5:
                                    //Loan history

                                    PrintLoans();
                                    Console.ReadKey();

                                    break;
                                case 6:

                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine(Constants.TypeError, "Options", "Number");
                            Console.ReadKey();

                        }

                        break;
                    case 2:
                        Console.WriteLine(Constants.Separator);
                        Console.WriteLine(Constants.BookOptions);
                        Console.WriteLine(Constants.Separator);
                        input = Console.ReadLine();
                        Console.WriteLine(Constants.Separator);
                        if (int.TryParse(input, out action))
                        {
                            switch (action)
                            {

                                case 1:
                                    //Agregar Libro
                                    int repeat;
                                    do
                                    {
                                        var response = ValidateBook();

                                        if (response.Status == 200)
                                        {
                                            library.AddBook(response.Book!);
                                            Console.WriteLine(Constants.BookAdded);
                                            Console.WriteLine(Constants.Separator);
                                        }
                                        else
                                        {
                                            Console.WriteLine(Constants.Separator);
                                            Console.WriteLine(response.Message);
                                            Console.WriteLine(Constants.Separator);
                                            Console.ReadKey();
                                        }

                                        Console.WriteLine(Constants.AddBook);

                                        Console.WriteLine(Constants.YesNo);
                                        Console.WriteLine(Constants.Separator);
                                        input = Console.ReadLine();
                                        int.TryParse(input, out repeat);
                                        Console.WriteLine(Constants.Separator);

                                    } while (repeat == 1);

                                    break;
                                case 2:
                                    //Buscar por titulo
                                    Console.WriteLine(Constants.TitleSearch);

                                    var title = Console.ReadLine();
                                    Console.WriteLine(Constants.Separator);

                                    PrintBooks(title!);
                                    Console.WriteLine(Constants.Separator);
                                    Console.ReadKey();

                                    break;
                                case 3:
                                    //Listar libros
                                    PrintBooks(string.Empty);
                                    Console.ReadKey();
                                    break;
                                case 4:
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine(Constants.TypeError, "Options", "Number");
                            Console.ReadKey();
                        }

                        break;
                }
            }
            else
            {
                Console.WriteLine(Constants.TypeError, "Options", "Number");
                Console.ReadKey();
            }

        }

        Response ValidateBook()
        {
            Console.WriteLine(Constants.RequestTitle);

            var title = Console.ReadLine();

            if (string.IsNullOrEmpty(title))
            {
                return new Response()
                {
                    Message = string.Format(Constants.EmptyField, "Title"),
                    Status = 400
                };
            }

            Console.WriteLine(Constants.RequestAuthor);

            var author = Console.ReadLine();

            if (string.IsNullOrEmpty(author))
            {
                return new Response()
                {
                    Message = string.Format(Constants.Error404, "Author"),
                    Status = 400
                };

            }

            Console.WriteLine(Constants.RequestISBN);

            var isbnInput = Console.ReadLine();

            if (!int.TryParse(isbnInput, out int isbn))
            {
                return new Response()
                {
                    Message = string.Format(Constants.TypeError, "ISBN", "Number"),
                    Status = 400
                };
            }

            if (library.Exists(isbn))
            {
                return new Response()
                {
                    Message = string.Format(Constants.AlreadyExists, "book", "library", "number"),
                    Status = 400
                };
            }

            Console.WriteLine(Constants.RequestYearPublished);

            var yearInput = Console.ReadLine();

            if (!int.TryParse(yearInput, out int publishedYear) || publishedYear < 0 || publishedYear > DateTime.Now.Year)
            {
                return new Response()
                {
                    Message = "Year of publication must be a Number and a valid year",
                    Status = 400
                };
            }

            return new Response()
            {
                Book = new Book() { Title = title, Author = author, ISBN = isbn, Published = publishedYear },
                Status = 200
            };
        }
        Response ValidateUser()
        {
            Console.WriteLine(Constants.RequestUserName);

            var name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
                return new Response()
                {
                    Message = string.Format(Constants.EmptyField, "Name"),
                    Status = 400
                };
            }

            Console.WriteLine(Constants.Separator);
            Console.WriteLine(Constants.RequestUserId);

            var id = Console.ReadLine();

            if (!int.TryParse(id, out int userId))//validar tambien si existe el usuario
            {
                return new Response()
                {
                    Message = string.Format(Constants.TypeError, "ID", "Number"),
                    Status = 400
                };
            }

            if (library.Users.Any(u => u.UserId == userId))
            {
                return new Response()
                {
                    Message = Constants.ExistingUser,
                    Status = 400,
                };
            }

            return new Response()
            {
                User = new User() { UserName = name, UserId = userId },
                Status = 200
            };
        }
        Response ValidateLoan(Response book, Response user)
        {
            if (!library.Loans.Any(b => b.Book == book.Book) || !library.Loans.Any(l => l.Book == book.Book && l.ReturnDate == null))//Por que si niego la segunda condicion entra igual al if
            {
                return new Response()
                {
                    Status = 200
                };
            }
            return new Response()
            {
                Status = 400
            };
        }
        Response BookSearch(int isbn)
        {
            //agregar validacion para ver si el libro es parte de un registro de prestamo ACTIVO
            //library.Loans.Any(l => (l.Book == book && l.ReturnDate == null));
            foreach (var book in library.Books)
            {
                if (book.ISBN == isbn)
                {
                    return new Response()
                    {
                        Status = 200,
                        Book = book
                    };
                }
            }

            return new Response()
            {
                Status = 404,
                Message = string.Format(Constants.Error404, "Book")
            };
        }
        Response UserSearch(int id)
        {
            foreach (var user in library.Users)
            {
                if (user.UserId == id)
                    return new Response()
                    {
                        Status = 200,
                        User = user
                    };
            }
            return new Response()
            {
                Message = string.Format(Constants.Error404, "User"),
                Status = 404
            };
        }
        void PrintBooks(string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                foreach (Book book in library.Books)
                {
                    Console.WriteLine(Constants.BookInfoText, book.Title, book.Author, book.ISBN, book.Published);
                }
                if (library.Books.Count < 1)
                {
                    Console.WriteLine(Constants.NoBookError);
                }
            }
            else
            {
                foreach (var book in library.Books)
                {
                    if (book.Title!.Contains(filter, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine(Constants.BookInfoText, book.Title, book.Author, book.ISBN, book.Published);
                    }
                }
                if (library.Books.Count < 1)
                {
                    Console.WriteLine(Constants.NoBookError);
                }
                //var book = library.Books.FirstOrDefault(x=> x.Equals(filter));
            }
        }//Metodo redundante?
        void PrintUsers(string userId)
        {

            if (string.IsNullOrEmpty(userId))
            {
                foreach (var user in library.Users)
                {
                    Console.WriteLine(Constants.UserInfoText, user.UserName, user.UserId);
                }
                if (library.Users.Count < 1)
                {
                    Console.WriteLine(Constants.NoUserError);
                    Console.ReadKey();
                }
            }
            else
            {
                foreach (var user in library.Users)
                {
                    if (user.UserId == int.Parse(userId))
                    {
                        Console.WriteLine(Constants.UserInfoText, user.UserName, user.UserId);
                    }
                }

                if (library.Users.Count < 1)
                {
                    Console.WriteLine(Constants.NoUserError);
                    Console.ReadKey();
                }
            }
        }
        void PrintLoans()
        {
            foreach (var loan in library.Loans)
            {
                Console.WriteLine(Constants.Separator);
                Console.WriteLine(Constants.LoanInfoText, loan.Book.Title, loan.Book.Author, loan.Book.ISBN, loan.Book.Published, loan.User.UserName, loan.User.UserId, loan.LoanDate, loan.ReturnDate);
            }
        }
    }
}