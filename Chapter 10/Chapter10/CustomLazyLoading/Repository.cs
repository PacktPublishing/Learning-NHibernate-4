using NHibernate;

namespace Chapter10.CustomLazyLoading
{
    public class Repository : IRepository<Employee>
    {
        private readonly ISession session;

        public Repository(ISession session)
        {
            this.session = session;
        }

        public Employee GetById(int id)
        {
            var staffMember = session.Get<StaffMember>(id);
            var employee = new Employee
            {
                Firstname = staffMember.Firstname,
                Lastname = staffMember.Lastname,
                //Initialize other properties here
            };
            return employee;
        }
    }
}