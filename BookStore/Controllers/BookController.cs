using BookStore.Models;
using BookStore.Models.Repository;
using BookStore.ViewModels;

using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
    
        public IBookStoreRepository<Book> BookRepository { get; }
        public IBookStoreRepository<Author> AuthorRepository { get; }

        public BookController(IBookStoreRepository<Book> bookRepository, IBookStoreRepository<Author> AuthorRepository, IWebHostEnvironment hostingEnvironment
        )
        {
            BookRepository = bookRepository;
            this.AuthorRepository = AuthorRepository;
            _hostingEnvironment = hostingEnvironment;

        }
        // GET: BookController

        public ActionResult Index()
        {
            var books = BookRepository.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var book = BookRepository.Find(id);
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var book = new BookAuthorViewModel
            {
                Authors = FillSelect()
            };
            return View(book);
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "You have to fill all required field");
                var book = new BookAuthorViewModel
                {
                    Authors = FillSelect()
                };
                return View(book);
            }
            try
            {
                if (model.AuthorId == -1)
                {

                    ModelState.AddModelError("Authors", "Please Select Author ");
                    var vbook = new BookAuthorViewModel
                    {
                        Authors = FillSelect()
                    };
                    return View(vbook);
                }
             
                if (model.clientFile != null)
                {
                    var stream = new MemoryStream();
                    model.clientFile.CopyTo(stream);
                    model.ProfilePicture = stream.ToArray();
                }

                        var author = AuthorRepository.Find(model.AuthorId);

                Book book = new Book
                {
                    Id = model.BookId,
                    Title = model.Title,
                    Description = model.Description,
                    Author = author,
                    ProfilePicture = model.ProfilePicture
                };
                        BookRepository.Add(book);    
                
                 return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = BookRepository.Find(id);
            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;
            var viewModel = new BookAuthorViewModel
            {
                Title = book.Title,
                BookId = book.Id,
                Description = book.Description,
                AuthorId = authorId,
                Authors = AuthorRepository.List().ToList(),
                ProfilePicture=book.ProfilePicture
       
            };

            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel viewmodel)
        {
            try
            {
    
                var author = AuthorRepository.Find(viewmodel.AuthorId);

                if (viewmodel.clientFile != null)
                {
                    var strem = new MemoryStream();
                    viewmodel.clientFile.CopyTo(strem);
                    viewmodel.ProfilePicture = strem.ToArray();
                }
                var book = new Book
                {
                    Id = viewmodel.BookId,
                    Title = viewmodel.Title,
                    Description = viewmodel.Description,
                    Author = author,
                    ProfilePicture = viewmodel.ProfilePicture
                };
                BookRepository.Update(viewmodel.BookId, book);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = BookRepository.Find(id);
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    BookRepository.Delete(id);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Search(string term)
        {
            var res = BookRepository.Search(term);
            return View(nameof(Index), res);
        }
        List<Author> FillSelect()
        {
            var authors = AuthorRepository.List().ToList();
            authors.Insert(0, new Author { Id = -1, FullName = "--- Please select  an author ---" });
            return authors;
        }
      
    }
}
