using System;
using System.Collections.Generic;
using Domain;

namespace Tests.Unit.QueryTests
{
    public class QueryTest
    {
        protected InMemoryDatabase Database;

        public QueryTest()
        {
            Database = new InMemoryDatabase();
            Database.Initialize();

            var johnSmith = new Employee
            {
                Firstname = "John",
                Lastname = "Smith",
                DateOfJoining = new DateTime(2014, 5, 5),
                ResidentialAddress = new Address
                {
                    AddressLine1 = "123 Planet place",
                    AddressLine2 = "12 Gomez street",
                    City = "London",
                    Postcode = "SW7 4FG",
                    Country = "United Kingdom"
                }
            };

            johnSmith.AddCommunity(new Community
            {
                Name = "NHibernate Beginners"
            });
            johnSmith.AddCommunity(new Community
            {
                Name = "London bikers"
            });
            johnSmith.AddBenefit(new SeasonTicketLoan
            {
                Amount = 1320,
                MonthlyInstalment = 110
            });
            johnSmith.AddBenefit(new Leave
            {
                AvailableEntitlement = 12,
                RemainingEntitlement = 2
            });

            var hillaryGamble = new Employee
            {
                Firstname = "hillary",
                Lastname = "Gamble",
                DateOfJoining = new DateTime(2013, 5, 5),
                ResidentialAddress = new Address
                {
                    AddressLine1 = "102 Oxygen",
                    AddressLine2 = "34 Western Gateway",
                    City = "London",
                    Postcode = "NW5 7AC",
                    Country = "United Kingdom"
                }
            };

            hillaryGamble.AddCommunity(new Community
            {
                Name = "NHibernate experts"
            });

            hillaryGamble.AddBenefit(new SeasonTicketLoan
            {
                Amount = 600,
                MonthlyInstalment = 50
            });



            var smithPearson = new Employee
            {
                Firstname = "Smith",
                Lastname = "Pearson",
                DateOfJoining = new DateTime(2013, 5, 5),
                ResidentialAddress = new Address
                {
                    AddressLine1 = "102 Oxygen",
                    AddressLine2 = "34 Western Gateway",
                    City = "London",
                    Postcode = "NW5 7AC",
                    Country = "United Kingdom"
                }
            };

            smithPearson.AddCommunity(new Community
            {
                Name = "NHibernate experts"
            });

            smithPearson.AddBenefit(new SkillsEnhancementAllowance
            {
                Entitlement = 1000
            });

            Database.SeedUsing(new List<Employee>
            {
                johnSmith,
                hillaryGamble,
                smithPearson
            });
        }
    }
}