using DatingApp.Data;
using DatingApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly DataContext _Context;
        public UsersController(DataContext context) 
        { 
            _Context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() 
        {  
            return await _Context.Users.ToListAsync(); ;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUserById(int id)
        {
            return await _Context.Users.FindAsync(id); ;
        }

        [HttpPost] // HTTP POST endpoint to add a new user
        public async Task<ActionResult<AppUser>> AddUser(AppUser user)
        {
            _Context.Users.Add(user); // Add the new user to the context
            await _Context.SaveChangesAsync(); // Save changes to the database

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }
    }
}
