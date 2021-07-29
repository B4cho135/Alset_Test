using Models.Queries;
using Models.Users;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SDK.Resources
{
    public interface IUserResource
    {
        /// <summary>
        /// Gets all users from the database
        /// </summary>
        [Get("/api/Users")]
        Task<ApiResponse<List<User>>> GetAll();

        /// <summary>
        /// gets specific user using Id from database
        /// </summary>
        [Get("/api/Users/{Id}")]
        Task<ApiResponse<User>> Get(Guid Id);


        /// <summary>
        /// adds new User To database
        /// </summary>
        [Post("/api/Users")]
        Task Add(User user);


        /// <summary>
        /// Updates specific user in database
        /// </summary>
        [Put("/api/Users/{Id}")]
        Task Update(Guid Id, User user);


        /// <summary>
        /// Deletes from database (Soft Delete)
        /// </summary>
        [Delete("/api/Users/{Id}")]
        Task Delete(Guid Id);
    }
}
