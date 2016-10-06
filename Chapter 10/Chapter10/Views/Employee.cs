using System;

namespace Chapter10.Views
{
public class Employee
{
    public virtual int Id { get; set; }
    public virtual string Firstname { get; set; }
    public virtual string Lastname { get; set; }
    public virtual DateTime DateOfJoining { get; set; }
    public virtual string AddressLine1 { get; set; }
    public virtual string AddressLine2 { get; set; }
     
}
}