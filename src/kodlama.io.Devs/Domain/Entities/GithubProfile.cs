using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GithubProfile : Entity
    {
        public int DeveloperId { get; set; }
        public string ProfileUrl { get; set; }
        public virtual Developer? Developer { get; set; }

        public GithubProfile()
        {

        }

        public GithubProfile(int id, int developerId, string profileUrl) : this()
        {
            Id = id;
            DeveloperId = developerId;
            ProfileUrl = profileUrl;
        }
           
    }
}
