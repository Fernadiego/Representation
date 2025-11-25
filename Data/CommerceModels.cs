using System.ComponentModel.DataAnnotations;

namespace BlazorVentas.Data;

public class Company
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;
}

public class Supplier
{
    public int Id { get; set; }
    public int CompanyId { get; set; }

    [Required, StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [StringLength(80)]
    public string ContactName { get; set; } = string.Empty;

    [Phone]
    public string? Phone { get; set; }

    [EmailAddress]
    public string? Email { get; set; }
}

public class Vendor
{
    public int Id { get; set; }
    public int CompanyId { get; set; }

    [Required, StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [Range(0, 100)]
    public decimal CommissionRate { get; set; } = 5;
}

public class Customer
{
    public int Id { get; set; }
    public int CompanyId { get; set; }

    [Required, StringLength(80)]
    public string Name { get; set; } = string.Empty;

    [EmailAddress]
    public string? Email { get; set; }

    [Phone]
    public string? Phone { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public int CompanyId { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(30)]
    public string Sku { get; set; } = string.Empty;

    [Range(0, 999999)]
    public decimal UnitPrice { get; set; } = 1;

    [Range(0, 999999)]
    public int Stock { get; set; } = 0;

    [Required]
    public int SupplierId { get; set; }

    public Supplier? Supplier { get; set; }
}

public class SaleLine
{
    [Required]
    public int ProductId { get; set; }

    public Product? Product { get; set; }

    [Range(1, 9999)]
    public int Quantity { get; set; } = 1;

    [Range(0, 999999)]
    public decimal UnitPrice { get; set; }

    public decimal LineTotal => Quantity * UnitPrice;
}

public class Sale
{
    public int Id { get; set; }
    public int CompanyId { get; set; }

    public DateTime Date { get; set; } = DateTime.Today;

    [Required]
    public int CustomerId { get; set; }

    public Customer? Customer { get; set; }

    [Required]
    public int VendorId { get; set; }

    public Vendor? Vendor { get; set; }

    public List<SaleLine> Lines { get; set; } = new();

    public decimal Total => Lines.Sum(l => l.LineTotal);
}

public class CommerceSnapshot
{
    public decimal InventoryValue { get; set; }
    public decimal MonthlySales { get; set; }
    public int ActiveVendors { get; set; }
    public int CustomerCount { get; set; }
    public decimal AverageTicket { get; set; }
}

