using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;

namespace LR4_ASP
{
    public class LibraryController : Controller
    {
        private readonly string booksFilePath = "Data/books.json";
        private readonly string usersFilePath = "Data/users.json";

        [Route("Library")]
        public IActionResult Library()
        {
            return Content("Вас вітає бібліотека 401 -_-");
        }

        [Route("Library/Books")]
        public IActionResult Books()
        {
            var books = ReadBooksFromJson();
            return Json(books);
        }

        [Route("Library/Profile/{id?}")]
        public IActionResult Profile(int? id)
        {
            List<User> users = ReadUsersFromJson();

            if (id.HasValue && id >= 0 && id <= 5)
            {
                var user = users.Find(u => u.Id == id);
                if (user != null)
                    return Json(user);
                else
                    return NotFound();
            }
            else
            {
                return Content("Інформація про поточного користувача: Zachinyev");
            }
        }

        private List<Book> ReadBooksFromJson()
        {
            var json = System.IO.File.ReadAllText(booksFilePath);
            return JsonConvert.DeserializeObject<List<Book>>(json);
        }

        private List<User> ReadUsersFromJson()
        {
            var json = System.IO.File.ReadAllText(usersFilePath);
            return JsonConvert.DeserializeObject<List<User>>(json);
        }
    }

    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
