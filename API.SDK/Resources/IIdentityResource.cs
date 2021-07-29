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
    public interface IIdentityResource
    {
        /// <summary>
        /// Gets all users from the database
        /// </summary>
        [Get("/api/Identities")]
        Task<ApiResponse<List<User>>> GetAll();


        /// <summary>
        /// gets specific user using Id from database
        /// </summary>
        [Get("/api/Identities/{Id}")]
        Task<User> Get(Guid Id);


        /// <summary>
        /// adds new User To database
        /// </summary>
        [Post("/api/Identities")]
        Task Add(User user);


        /// <summary>
        /// Updates specific user in database
        /// </summary>
        [Put("/api/Identities")]
        Task Update(User user);


        /// <summary>
        /// Deletes from database (Soft Delete)
        /// </summary>
        [Delete("/api/Identities/{Id}")]
        Task Delete(Guid Id);
    }
}
