using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using NUnit.Framework;

namespace Tests.Unit.Mappings
{
    [TestFixture]
    public class EmployeeAssociationMappingsTests : MappingTests
    {
        [Test]
        public void MapsBenefits()
        {

            object id = 0;
            using (var transaction = Session.BeginTransaction())
            {
                id = Session.Save(new Employee
                {
                    EmployeeNumber = "123456789",
                    Benefits = new HashSet<Benefit>
                    {
                        new SkillsEnhancementAllowance
                        {
                            Entitlement = 1000,
                            RemainingEntitlement = 250
                        },
                        new SeasonTicketLoan
                        {
                            Amount = 1416,
                            MonthlyInstalment = 118,
                            StartDate = new DateTime(2014, 4, 25),
                            EndDate = new DateTime(2015, 3, 25)
                        },
                        new Leave
                        {
                            AvailableEntitlement = 30,
                            RemainingEntitlement = 15,
                            Type = LeaveType.Paid
                        }
                    }
                });
                transaction.Commit();
            }

            Session.Clear();

            using (var transaction = Session.BeginTransaction())
            {
                var employee = Session.Get<Employee>(id);

                Assert.That(employee.Benefits.Count, Is.EqualTo(3));

                var seasonTicketLoan = employee.Benefits.FirstOrDefault(b => b.GetType() == typeof (SeasonTicketLoan));
                Assert.That(seasonTicketLoan, Is.Not.Null);
                if (seasonTicketLoan != null) {
                    Assert.That(seasonTicketLoan.Employee.EmployeeNumber, Is.EqualTo("123456789"));
                }

                var skillsEnhancementAllowance = employee.Benefits.FirstOrDefault(b => b.GetType() == typeof(SkillsEnhancementAllowance));
                Assert.That(skillsEnhancementAllowance, Is.Not.Null);
                if (skillsEnhancementAllowance != null)
                {
                    Assert.That(skillsEnhancementAllowance.Employee.EmployeeNumber, Is.EqualTo("123456789"));
                }

                var leave = employee.Benefits.FirstOrDefault(b => b.GetType() == typeof(Leave));
                Assert.That(leave, Is.Not.Null);
                if (leave != null)
                {
                    Assert.That(leave.Employee.EmployeeNumber, Is.EqualTo("123456789"));
                }

                transaction.Commit();
            }
        }

        [Test]
        public void MapsBenefitsForConcreteMapping()
        {
            object id = 0;
            using (var transaction = Session.BeginTransaction())
            {
                var skillsEnhancementAllowance = new SkillsEnhancementAllowance
                {
                    Entitlement = 1000,
                    RemainingEntitlement = 250
                };
                var seasonTicketLoan = new SeasonTicketLoan
                {
                    Amount = 1416,
                    MonthlyInstalment = 118,
                    StartDate = new DateTime(2014, 4, 25),
                    EndDate = new DateTime(2015, 3, 25)
                };
                var leave = new Leave
                {
                    AvailableEntitlement = 30,
                    RemainingEntitlement = 15,
                    Type = LeaveType.Paid
                };
                var employee = new Employee
                {
                    EmployeeNumber = "123456789",
                    Benefits = new HashSet<Benefit>
                    {
                        skillsEnhancementAllowance,
                        seasonTicketLoan,
                        leave
                    }
                };
                skillsEnhancementAllowance.Employee = employee;
                seasonTicketLoan.Employee = employee;
                leave.Employee = employee;

                id = Session.Save(employee);
                transaction.Commit();
            }

            Session.Clear();

            using (var transaction = Session.BeginTransaction())
            {
                var employee = Session.Get<Employee>(id);

                Assert.That(employee.Benefits.Count, Is.EqualTo(3));

                var seasonTicketLoan = employee.Benefits.FirstOrDefault(b => b.GetType() == typeof(SeasonTicketLoan));
                Assert.That(seasonTicketLoan, Is.Not.Null);
                if (seasonTicketLoan != null)
                {
                    Assert.That(seasonTicketLoan.Employee.EmployeeNumber, Is.EqualTo("123456789"));
                }

                var skillsEnhancementAllowance = employee.Benefits.FirstOrDefault(b => b.GetType() == typeof(SkillsEnhancementAllowance));
                Assert.That(skillsEnhancementAllowance, Is.Not.Null);
                if (skillsEnhancementAllowance != null)
                {
                    Assert.That(skillsEnhancementAllowance.Employee.EmployeeNumber, Is.EqualTo("123456789"));
                }

                var leave = employee.Benefits.FirstOrDefault(b => b.GetType() == typeof(Leave));
                Assert.That(leave, Is.Not.Null);
                if (leave != null)
                {
                    Assert.That(leave.Employee.EmployeeNumber, Is.EqualTo("123456789"));
                }

                transaction.Commit();
            }
        }

        [Test]
        public void MapsCommunities()
        {
            object id = 0;
            using (var transaction = Session.BeginTransaction())
            {
                id = Session.Save(new Employee
                {
                    EmployeeNumber = "123456789",
                    Communities = new HashSet<Community>
                    {
                        new Community
                        {
                            Name = "Community 1"
                        },
                        new Community
                        {
                            Name = "Community 2"
                        }
                    }
                });
                transaction.Commit();
            }

            Session.Clear();

            using (var transaction = Session.BeginTransaction())
            {
                var employee = Session.Get<Employee>(id);
                Assert.That(employee.Communities.Count, Is.EqualTo(2));
                Assert.That(employee.Communities.First().Members.First().EmployeeNumber, Is.EqualTo("123456789"));
                transaction.Commit();
            }
        }
    }
}