using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MajsterChef.Data;
using MajsterChef.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace MajsterChef.Pages.Przepisy
{
    public class DetailsModel : PageModel
    {
        private readonly PrzepisContext _context;
        [ViewData]
        public int EntryID { get; set; }
        public DetailsModel(PrzepisContext context)
        {
            _context = context;
        }
        public string UserName(string name)
        {
            if (!String.IsNullOrEmpty(name))
                return name.Split('@')[0];
            else return "Anonymous";
        }
        public int likes, dislikes, score;
        public string ReturnUrl;
        [BindProperty]
        public Przepis Przepis { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("Index");
            }

            Przepis = await _context.Przepis.FirstOrDefaultAsync(m => m.ID == id);
            EntryID = Przepis.ID;
            likes = Przepis.Likes;
            dislikes = Przepis.Dislikes;
            score = Przepis.Score;
            ReturnUrl = "Przepisy/Details?id=" + EntryID;
            if (Przepis == null)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }

        public int Score()
        {
            return score;
        }
        public async Task<IActionResult> OnPostLike()
        {
            BtnLike_Click();
            await _context.SaveChangesAsync();
            // all  done
            return RedirectToPage("Index");
        }
        public async Task<IActionResult> OnPostDislike()
        {
            BtnDislike_Click();
            await _context.SaveChangesAsync();
            // all  done
            return RedirectToPage("Index");
        }
        public async Task<IActionResult> OnPostFavourite()
        {
            BtnFavourite_Click();
            await _context.SaveChangesAsync();
            // all  done
            return RedirectToPage("Index");
        }
        private void BtnFavourite_Click()
        {
            using SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PrzepisyDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            using SqlCommand command = con.CreateCommand();
            con.Open();

            //pobieranie ID obecnie przeglądanego przepisu
            var result = new string(Request.QueryString.ToString().Where(c => char.IsDigit(c)).ToArray());
            int resultint = Convert.ToInt32(result);
            command.Parameters.AddWithValue("@idprzepis", resultint);
            command.Parameters.AddWithValue("@iduser", User.Identity.Name);

            //favIQ == wszystkie polubienia wszystkich userów
            IQueryable<Favourites> favIQ = from f in _context.Favourites select f;

            //pp == wszystkie przepisy
            IQueryable<Przepis> pp = from p in _context.Przepis select p;
            bool any = favIQ.Any(u => u.Id_usera == User.Identity.Name && u.PrzepisID == resultint);

            //ppp == obecnie przeglądany przepis
            Przepis ppp = pp.FirstOrDefault(pp => pp.ID == resultint);
            if(!any)
            {
                //tutaj zrobić dodawanie do ulubionych
                //command.CommandText = "INSERT INTO dbo.Favourites (Id_usera, PrzepisID) VALUES (@iduser, @idprzepis)";
                //command.ExecuteNonQuery();
                Favourites fav = new Favourites
                {
                    Id_usera = User.Identity.Name,
                    PrzepisID = ppp.ID
                };
                _context.Favourites.Add(fav);
                _context.SaveChanges();
            }
            con.Close();
        }
        private void BtnLike_Click()
        {
            using SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PrzepisyDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            using SqlCommand command = con.CreateCommand();
            
            con.Open();
            var result = new string(Request.QueryString.ToString().Where(c => char.IsDigit(c)).ToArray());
            int resultint = Convert.ToInt32(result);
            command.Parameters.AddWithValue("@id", resultint);
            IQueryable<Oceny> przepisyIQ = from s in _context.Oceny
                                           select s;

            //Sprawdzenie, czy dany przepis został przez usera oceniony na +
            bool naplus, naminus, nanic;
            naplus = przepisyIQ.Any(u => u.Id_usera == User.Identity.Name && u.Id_wpisu == resultint && u.Czy_ocenil == '+');
            naminus = przepisyIQ.Any(u => u.Id_usera == User.Identity.Name && u.Id_wpisu == resultint && u.Czy_ocenil == '-');
            nanic = przepisyIQ.Any(u => u.Id_usera == User.Identity.Name && u.Id_wpisu == resultint && u.Czy_ocenil == ' ');
            command.Parameters.AddWithValue("@user", User.Identity.Name);
            if (!naplus)
            {
                //przejście z - na 0
                if(naminus)
                {
                    command.CommandText = "UPDATE dbo.Przepis SET Dislikes = Dislikes - 1 Where ID = @id";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE dbo.Przepis SET Score = Score + 1 Where ID = @id";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE dbo.Oceny SET Czy_ocenil = ' ' Where Id_wpisu = @id AND Id_usera = @user";
                    command.ExecuteNonQuery();
                }
                //przejście z 0 na +
                else if(nanic)
                {
                    command.CommandText = "UPDATE dbo.Przepis SET Likes = Likes + 1 Where ID = @id";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE dbo.Przepis SET Score = Score + 1 Where ID = @id";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE dbo.Oceny SET Czy_ocenil = '+' Where Id_wpisu = @id AND Id_usera = @user";
                    command.ExecuteNonQuery(); 
                }
                else
                { 
                    command.CommandText = "UPDATE dbo.Przepis SET Likes = Likes + 1 Where ID = @id";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE dbo.Przepis SET Score = Score + 1 Where ID = @id";
                    command.ExecuteNonQuery();
                    Oceny oc = new Oceny
                        {
                            Id_usera = User.Identity.Name,
                            Id_wpisu = resultint,
                            Czy_ocenil = '+'
                        };
                        _context.Oceny.Add(oc);
                        _context.SaveChanges();
                }
                
            }
            con.Close();

        }
        private void BtnDislike_Click()
        {

            using SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PrzepisyDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            using SqlCommand command = con.CreateCommand();
            
            con.Open();
            var result = new string(Request.QueryString.ToString().Where(c => char.IsDigit(c)).ToArray());
            int resultint = Convert.ToInt32(result);
            command.Parameters.AddWithValue("@id", resultint);
            IQueryable<Oceny> przepisyIQ = from s in _context.Oceny
                                             select s;
            bool naplus, naminus, nanic;
            naplus = przepisyIQ.Any(u => u.Id_usera == User.Identity.Name && u.Id_wpisu == resultint && u.Czy_ocenil == '+');
            naminus = przepisyIQ.Any(u => u.Id_usera == User.Identity.Name && u.Id_wpisu == resultint && u.Czy_ocenil == '-');
            nanic = przepisyIQ.Any(u => u.Id_usera == User.Identity.Name && u.Id_wpisu == resultint && (u.Czy_ocenil == '0' || u.Czy_ocenil==' '));
            command.Parameters.AddWithValue("@user", User.Identity.Name);

            //teraz na minus
            if (!naminus)
            {
                //przejście z + na 0
                if (naplus)
                {
                    command.CommandText = "UPDATE dbo.Przepis SET Likes = Likes - 1 Where ID = @id";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE dbo.Przepis SET Score = Score - 1 Where ID = @id";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE dbo.Oceny SET Czy_ocenil = ' ' Where Id_wpisu = @id AND Id_usera = @user";
                    command.ExecuteNonQuery();
                }
                //przejście z 0 na -
                else if (nanic)
                {
                    command.CommandText = "UPDATE dbo.Przepis SET Dislikes = Dislikes + 1 Where ID = @id";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE dbo.Przepis SET Score = Score - 1 Where ID = @id";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE dbo.Oceny SET Czy_ocenil = '-' Where Id_wpisu = @id AND Id_usera = @user";
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.CommandText = "UPDATE dbo.Przepis SET Dislikes = Dislikes + 1 Where ID = @id";
                    command.ExecuteNonQuery();
                    command.CommandText = "UPDATE dbo.Przepis SET Score = Score - 1 Where ID = @id";
                    command.ExecuteNonQuery();
                    Oceny oc = new Oceny
                    {
                        Id_usera = User.Identity.Name,
                        Id_wpisu = resultint,
                        Czy_ocenil = '-'
                    };
                    _context.Oceny.Add(oc);
                    _context.SaveChanges();
                }

            }
            con.Close();
            
        }
    }
}
