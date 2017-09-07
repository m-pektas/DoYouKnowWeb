
    namespace DoYouKnowWeb.Database
    {
        using System;
        using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
        using System.Linq;

        public class DoYouKnowDBLastVersion : DbContext
        {
            public DoYouKnowDBLastVersion()
                : base("name=DoYouKnowDBLastVersion")
            {
            }

            public virtual DbSet<UserLogin> UserLogin { get; set; }
            public virtual DbSet<MyUser> MyUser { get; set; }
            public virtual DbSet<Group> Group { get; set; }
            public virtual DbSet<Event> Event { get; set; }
            public virtual DbSet<Location> Location { get; set; }
        }

        public class MyUser
        {
            [Key]
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime Birthday { get; set; }
            public byte[] Image { get; set; }
            public ICollection<int> FollowedList { get; set; }
            public ICollection<int> FollowerList { get; set; }
            public string Email { get; set; }
            public ICollection<int> Groups { get; set; }
            public string Status { get; set; } // sýradan katýlýmcý , önemli katýlýmcý
            public string Title { get; set; } // öðrenci vb..
            public int UserLoginId { get; set; }
            public DateTime TimeToCome { get; set; }//etkinliðe katýlacaðý zaman
        }

        public class UserLogin
        {
            [Key]
            public int Id { get; set; }
            public string userName { get; set; }
            public string password { get; set; }
            public int UserId { get; set; }
        }

        public class Group
        {
            [Key]
            public int Id { get; set; }
            public string Name { get; set; }
            public byte[] Image { get; set; }
            public ICollection<int> UserList { get; set; }
            public ICollection<int> MyEventList { get; set; }
        }

        public class Event
        {
            [Key]
            public int Id { get; set; }
            public string Subject { get; set; }
            DateTime Time { get; set; }
            public int MyGroupId { get; set; }
            public int MainLocationId { get; set; }
            public ICollection<int> OtherLocationList { get; set; }
        }

        public class Location
        {
            [Key]
            public int Id { get; set; }
            public string Name { get; set; }
            public string Adress { get; set; }
            public int MyEventId { get; set; }
            public int MyGroupId { get; set; }
        }


    }