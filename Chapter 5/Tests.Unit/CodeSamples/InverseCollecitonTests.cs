using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Domain;
using NUnit.Framework;
using Tests.Unit.Mappings;

namespace Tests.Unit.CodeSamples
{

    [TestFixture]
    public class InverseCollecitonTests : MappingTests
    {
        [Test]
        public void OneToManyAssociationWithOneSideAsOwner()
        {

            object id = 0;
            using (var transaction = Session.BeginTransaction())
            {
                var skillsEnhancementAllowance = new SkillsEnhancementAllowance
                {
                    Entitlement = 1000, RemainingEntitlement = 250
                };
                var employee = new Employee
                {
                    EmployeeNumber = "123456789",
                    Benefits = new HashSet<Benefit>
                    {
                        skillsEnhancementAllowance
                    }
                };
                skillsEnhancementAllowance.Employee = employee;

                id = Session.Save(employee);
                transaction.Commit();
            }

            Session.Clear();

            using (var transaction = Session.BeginTransaction())
            {
                var employee = Session.Get<Employee>(id);

                Assert.That(employee.Benefits.Count, Is.EqualTo(1));
                
                var skillsEnhancementAllowance = employee.Benefits.OfType<SkillsEnhancementAllowance>().FirstOrDefault();
                Assert.That(skillsEnhancementAllowance, Is.Not.Null);
                if (skillsEnhancementAllowance != null)
                {
                    Assert.That(skillsEnhancementAllowance.Employee.EmployeeNumber, Is.EqualTo("123456789"));
                }

                transaction.Commit();
            }
        }

        [Test]
        public void OneToManyAssociationWithManySideAsOwner()
        {
            object id = 0;
            using (var transaction = Session.BeginTransaction())
            {
                var employee = new Employee
                {
                    EmployeeNumber = "123456789",
                };
                var skillsEnhancementAllowance = new SkillsEnhancementAllowance
                {
                    Entitlement = 1000,
                    RemainingEntitlement = 250,
                    Employee = employee
                };
                employee.Benefits.Add(skillsEnhancementAllowance);
                id = Session.Save(skillsEnhancementAllowance);
                transaction.Commit();
            }

            Session.Clear();

            using (var transaction = Session.BeginTransaction())
            {
                var skillsEnhancementAllowance = Session.Get<SkillsEnhancementAllowance>(id);
                Assert.That(skillsEnhancementAllowance, Is.Not.Null);
                if (skillsEnhancementAllowance != null)
                {
                    Assert.That(skillsEnhancementAllowance.Employee.EmployeeNumber, Is.EqualTo("123456789"));
                }

                transaction.Commit();
            }
        }

        [Test]
        public void ManyToManyAssociationWithCommunitySideAsOwner()
        {

            object id = 0;
            using (var transaction = Session.BeginTransaction())
            {
                var employee = new Employee
                {
                    EmployeeNumber = "123456789"
                };
                var community = new Community
                {
                    Name = "Community 1",
                    Members = new HashSet<Employee> { employee }
                };

                id = Session.Save(community);
                transaction.Commit();
            }

            Session.Clear();

            using (var transaction = Session.BeginTransaction())
            {
                var community = Session.Get<Community>(id);

                Assert.That(community.Members.Count, Is.EqualTo(1));

                var employee = community.Members.FirstOrDefault();
                Assert.That(employee, Is.Not.Null);
                if (employee != null)
                {
                    Assert.That(employee.EmployeeNumber, Is.EqualTo("123456789"));
                }

                transaction.Commit();
            }
        }

        [Test]
        public void ManyToManyAssociationWithEmployeeSideAsOwner()
        {
            object id = 0;
            using (var transaction = Session.BeginTransaction())
            {
                var community = new Community
                {
                    Name = "Community 1"
                };
                var employee = new Employee
                {
                    EmployeeNumber = "123456789",
                    Communities = new HashSet<Community>{community}
                };

                id = Session.Save(employee);
                transaction.Commit();
            }

            Session.Clear();

            using (var transaction = Session.BeginTransaction())
            {
                var employee = Session.Get<Employee>(id);

                Assert.That(employee.Communities.Count, Is.EqualTo(1));

                var community = employee.Communities.FirstOrDefault();
                Assert.That(community, Is.Not.Null);
                if (community != null)
                {
                    Assert.That(community.Members.FirstOrDefault(), Is.Not.Null);
                }

                transaction.Commit();
            }
        }
    }
}