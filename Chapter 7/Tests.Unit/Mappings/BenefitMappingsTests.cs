using System;
using Domain;
using NUnit.Framework;

namespace Tests.Unit.Mappings
{
    [TestFixture]
    public class BenefitMappingsTests : MappingTests
    {
        [Test]
        public void MapsPrimitiveProperties()
        {
            object id = 0;
            using (var transaction = Session.BeginTransaction())
            {
                id = Session.Save(new Benefit
                {
                    Name = "Benefit 1",
                    Description = "Benefit 1 description"
                });
                transaction.Commit();
            }

            Session.Clear();

            using (var transaction = Session.BeginTransaction())
            {
                var benefit = Session.Get<Benefit>(id);
                Assert.That(benefit.Name, Is.EqualTo("Benefit 1"));
                Assert.That(benefit.Description, Is.EqualTo("Benefit 1 description"));
                transaction.Commit();
            }
        }

        [Test]
        public void MapsEmployeeAssociation()
        {
            object id = 0;
            using (var transaction = Session.BeginTransaction())
            {
                var employee = new Employee
                {
                    EmployeeNumber = "123456789"
                };
                Session.Save(employee);

                id = Session.Save(new Benefit
                {
                    Name = "Benefit 1",
                    Description = "Benefit 1 description",
                    Employee = employee
                });
                transaction.Commit();
            }

            Session.Clear();

            using (var transaction = Session.BeginTransaction())
            {
                var benefit = Session.Get<Benefit>(id);
                Assert.That(benefit.Employee, Is.Not.Null);
                Assert.That(benefit.Employee.EmployeeNumber, Is.EqualTo("123456789"));
                transaction.Commit();
            }
        }

        [Test]
        public void MapsSkillsEnhancementAllowance()
        {
            object id = 0;
            using (var transaction = Session.BeginTransaction())
            {
                id = Session.Save(new SkillsEnhancementAllowance
                {
                    Name = "Skill Enhacement Allowance",
                    Description = "Allowance for employees so that their skill enhancement trainings are paid for",
                    Entitlement = 1000,
                    RemainingEntitlement = 250
                });
                transaction.Commit();
            }

            Session.Clear();

            using (var transaction = Session.BeginTransaction())
            {
                var benefit = Session.Get<Benefit>(id);

                var skillsEnhancementAllowance = benefit as SkillsEnhancementAllowance;
                Assert.That(skillsEnhancementAllowance, Is.Not.Null);

                if (skillsEnhancementAllowance != null)
                {
                    Assert.That(skillsEnhancementAllowance.Name, Is.EqualTo("Skill Enhacement Allowance"));
                    Assert.That(skillsEnhancementAllowance.Description, Is.EqualTo("Allowance for employees so that their skill enhancement trainings are paid for"));
                    Assert.That(skillsEnhancementAllowance.Entitlement, Is.EqualTo(1000));
                    Assert.That(skillsEnhancementAllowance.RemainingEntitlement, Is.EqualTo(250));
                }
                transaction.Commit();
            }
        }

        [Test]
        public void MapsSeasonTicketLoan()
        {
            object id = 0;
            using (var transaction = Session.BeginTransaction())
            {
                id = Session.Save(new SeasonTicketLoan
                {
                    Amount = 1416,
                    MonthlyInstalment = 118,
                    StartDate = new DateTime(2014, 4, 25),
                    EndDate = new DateTime(2015, 3, 25)
                });
                transaction.Commit();
            }

            Session.Clear();

            using (var transaction = Session.BeginTransaction())
            {
                var benefit = Session.Get<Benefit>(id);

                var seasonTicketLoan = benefit as SeasonTicketLoan;
                Assert.That(seasonTicketLoan, Is.Not.Null);

                if(seasonTicketLoan != null)
                {
                    Assert.That(seasonTicketLoan.Amount, Is.EqualTo(1416));
                    Assert.That(seasonTicketLoan.MonthlyInstalment, Is.EqualTo(118));
                    Assert.That(seasonTicketLoan.StartDate.Year, Is.EqualTo(2014));
                    Assert.That(seasonTicketLoan.StartDate.Month, Is.EqualTo(4));
                    Assert.That(seasonTicketLoan.StartDate.Day, Is.EqualTo(25));
                    Assert.That(seasonTicketLoan.EndDate.Year, Is.EqualTo(2015));
                    Assert.That(seasonTicketLoan.EndDate.Month, Is.EqualTo(3));
                    Assert.That(seasonTicketLoan.EndDate.Day, Is.EqualTo(25));
                }
                transaction.Commit();
            }
        }

        [Test]
        public void MapsLeave()
        {
            object id = 0;
            using (var transaction = Session.BeginTransaction())
            {
                id = Session.Save(new Leave
                {
                    AvailableEntitlement = 30,
                    RemainingEntitlement = 15,
                    Type = LeaveType.Paid
                });
                transaction.Commit();
            }

            Session.Clear();

            using (var transaction = Session.BeginTransaction())
            {
                var benefit = Session.Get<Benefit>(id);
                var leave = benefit as Leave;

                if (leave != null)
                {
                    Assert.That(leave.AvailableEntitlement, Is.EqualTo(30));
                    Assert.That(leave.RemainingEntitlement, Is.EqualTo(15));
                    Assert.That(leave.Type, Is.EqualTo(LeaveType.Paid));
                }
                transaction.Commit();
            }
        }
    }
}

