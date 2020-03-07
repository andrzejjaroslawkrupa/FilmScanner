﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmScanner.Data;
using FilmScanner.Models;
using System.Collections.ObjectModel;

namespace FilmScanner.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }

        #region Users

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.ID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var tableName = "films_" + user.ID.ToString();

            using (var context = new UserContext())
            {
                await context.Database.ExecuteSqlRawAsync((string)$"CREATE TABLE {tableName} (ID int, ExternalID varchar(255), CreatedAt datetime2(7))");
            }

            return CreatedAtAction("GetUser", new { id = user.ID }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }

        #endregion

        #region Films

        // GET: api/Users/1/Films
        [HttpGet("{id}/films")]
        public async Task<ActionResult<IEnumerable<Film>>> GetFilms()
        {
            //return await _context.Films.ToListAsync();

            //select* from Films
            //CREATE TABLE Films_1(ID int, ExternalID varchar(255), CreatedAt datetime2(7));

            List<Film> films;

            using (var context = new UserContext())
            {
                films = await context.Films.FromSqlRaw("SELECT * FROM Films").ToListAsync();
            }

            return films;
        }


        //// POST: api/Users/1/
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPost("{id}/films")]
        //public async Task<ActionResult<Film>> PostFilm(int id, Film film)
        //{
        //    User user;

        //    //try
        //    //{
        //    //    user = await GetUserById(id);
        //    //}
        //    //catch (InvalidOperationException)
        //    //{
        //    //    return NotFound("Specified user could not be found");
        //    //}

        //    //if (user == null)
        //    //    user.Films = new Collection<Film>();

        //    //if (user.Films.Any(p => ArePeriodsInSameDay(p, period)))
        //    //{
        //    //    ModelState.AddModelError("WorkTimeExists", "WorkTime for selected period already exists");
        //    //    return BadRequest(ModelState);
        //    //}

        //    _context.Films.Add(film);
        //    await _context.SaveChangesAsync();

        //    //return CreatedAtAction("GetUser", new { id = user.ID }, user);
        //    return Ok();

        //}

        #endregion
    }
}