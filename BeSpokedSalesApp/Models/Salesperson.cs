
using System;
using System.Collections.Generic;

namespace BeSpokedSalesApp.Models;

public class Salesperson
{
    public int SalespersonId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Address { get; set; }

    public string Phone { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly? TerminationDate { get; set; }

    public int? ManagerId { get; set; }

    public List<Salesperson> InverseManager { get; set; } = new List<Salesperson>();

    public Salesperson? Manager { get; set; }

    public List<Sale> Sales { get; set; } = new List<Sale>();
}

public class SalespersonListModel
{
    public IEnumerable<Salesperson> Salespersons { get; set; }
}

public class SalespersonEditModel
{
    public Salesperson Salesperson { get; set; }
    public List<Salesperson> Managers { get; set; }
}