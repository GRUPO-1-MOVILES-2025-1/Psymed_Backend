using System;
using System.Collections.Generic;

namespace psymed_platform.IAM.Domain.Model.Aggregates
{
    public class User
    {
        public string Id { get; private set; }
        public string Username { get; private set; }
        public string Name { get; set; }
        public string LastName { get; set; } 
        public DateTime? BirthDate { get; set; }
        public string Email { get; private set; }
        public string Gender { get; set; }
        public string Phone { get; set; } 
        public string Ubication { get; set; }
        public string PasswordHash { get; private set; }
        public List<string> Roles { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastLogin { get; private set; }

        public User(string username, string email, string passwordHash, string name = null, string lastName = null, DateTime? birthDate = null, string gender = null, string phone = null, string ubication = null)
    {
        Id = Guid.NewGuid().ToString();
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        Name = name;
        LastName = lastName;
        BirthDate = birthDate ?? default;
        Gender = gender;
        Phone = phone;
        Ubication = ubication;
        Roles = new List<string>();
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }

        public void AddRole(string role)
        {
            if (!Roles.Contains(role))
                Roles.Add(role);
        }

        public void RemoveRole(string role)
        {
            Roles.Remove(role);
        }

        public void UpdateLastLogin()
        {
            LastLogin = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        public void Activate()
        {
            IsActive = true;
        }
    }
}