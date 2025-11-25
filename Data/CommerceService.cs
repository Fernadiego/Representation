using System.Linq;

namespace BlazorVentas.Data;

public class CommerceService
{
    private readonly object _gate = new();

    private readonly List<Company> _companies = new();
    private readonly Dictionary<int, TenantData> _tenants = new();

    public CommerceService()
    {
        Seed();
    }

    #region Companies

    public Task<List<Company>> GetCompaniesAsync()
    {
        lock (_gate)
        {
            return Task.FromResult(_companies.Select(CloneCompany).ToList());
        }
    }

    #endregion

    #region Suppliers

    public Task<List<Supplier>> GetSuppliersAsync(int companyId)
    {
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            return Task.FromResult(tenant.Suppliers.Select(CloneSupplier).ToList());
        }
    }

    public Task SaveSupplierAsync(int companyId, Supplier supplier)
    {
        ArgumentNullException.ThrowIfNull(supplier);

        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            if (supplier.Id == 0)
            {
                supplier.Id = tenant.SupplierSeq++;
                supplier.CompanyId = companyId;
                tenant.Suppliers.Add(CloneSupplier(supplier));
            }
            else
            {
                var existing = tenant.Suppliers.FirstOrDefault(s => s.Id == supplier.Id) ?? throw new InvalidOperationException("Proveedor no encontrado");
                existing.Name = supplier.Name;
                existing.ContactName = supplier.ContactName;
                existing.Phone = supplier.Phone;
                existing.Email = supplier.Email;
            }
        }

        return Task.CompletedTask;
    }

    public Task DeleteSupplierAsync(int companyId, int supplierId)
    {
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            tenant.Suppliers.RemoveAll(s => s.Id == supplierId);
            foreach (var product in tenant.Products.Where(p => p.SupplierId == supplierId))
            {
                product.SupplierId = 0;
            }
        }

        return Task.CompletedTask;
    }

    #endregion

    #region Vendors

    public Task<List<Vendor>> GetVendorsAsync(int companyId)
    {
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            return Task.FromResult(tenant.Vendors.Select(CloneVendor).ToList());
        }
    }

    public Task SaveVendorAsync(int companyId, Vendor vendor)
    {
        ArgumentNullException.ThrowIfNull(vendor);

        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            if (vendor.Id == 0)
            {
                vendor.Id = tenant.VendorSeq++;
                vendor.CompanyId = companyId;
                tenant.Vendors.Add(CloneVendor(vendor));
            }
            else
            {
                var existing = tenant.Vendors.FirstOrDefault(v => v.Id == vendor.Id) ?? throw new InvalidOperationException("Vendedor no encontrado");
                existing.Name = vendor.Name;
                existing.CommissionRate = vendor.CommissionRate;
            }
        }

        return Task.CompletedTask;
    }

    public Task DeleteVendorAsync(int companyId, int vendorId)
    {
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            tenant.Vendors.RemoveAll(v => v.Id == vendorId);
        }

        return Task.CompletedTask;
    }

    #endregion

    #region Customers

    public Task<List<Customer>> GetCustomersAsync(int companyId)
    {
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            return Task.FromResult(tenant.Customers.Select(CloneCustomer).ToList());
        }
    }

    public Task SaveCustomerAsync(int companyId, Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);

        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            if (customer.Id == 0)
            {
                customer.Id = tenant.CustomerSeq++;
                customer.CompanyId = companyId;
                tenant.Customers.Add(CloneCustomer(customer));
            }
            else
            {
                var existing = tenant.Customers.FirstOrDefault(c => c.Id == customer.Id) ?? throw new InvalidOperationException("Cliente no encontrado");
                existing.Name = customer.Name;
                existing.Email = customer.Email;
                existing.Phone = customer.Phone;
            }
        }

        return Task.CompletedTask;
    }

    public Task DeleteCustomerAsync(int companyId, int customerId)
    {
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            tenant.Customers.RemoveAll(c => c.Id == customerId);
        }

        return Task.CompletedTask;
    }

    #endregion

    #region Products

    public Task<List<Product>> GetProductsAsync(int companyId)
    {
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            return Task.FromResult(tenant.Products.Select(p => CloneProduct(tenant, p)).ToList());
        }
    }

    public Task SaveProductAsync(int companyId, Product product)
    {
        ArgumentNullException.ThrowIfNull(product);

        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            if (product.Id == 0)
            {
                product.Id = tenant.ProductSeq++;
                product.CompanyId = companyId;
                tenant.Products.Add(CopyProduct(product));
            }
            else
            {
                var existing = tenant.Products.FirstOrDefault(p => p.Id == product.Id) ?? throw new InvalidOperationException("Producto no encontrado");
                existing.Name = product.Name;
                existing.Sku = product.Sku;
                existing.UnitPrice = product.UnitPrice;
                existing.Stock = product.Stock;
                existing.SupplierId = product.SupplierId;
            }
        }

        return Task.CompletedTask;
    }

    public Task DeleteProductAsync(int companyId, int productId)
    {
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            tenant.Products.RemoveAll(p => p.Id == productId);
        }

        return Task.CompletedTask;
    }

    #endregion

    #region Sales

    public Task<List<Sale>> GetSalesAsync(int companyId)
    {
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            return Task.FromResult(tenant.Sales.Select(s => CloneSale(tenant, s)).ToList());
        }
    }

    public Task<Sale> CreateSaleAsync(int companyId, Sale sale)
    {
        ArgumentNullException.ThrowIfNull(sale);
        if (!sale.Lines.Any())
        {
            throw new InvalidOperationException("La venta requiere al menos un detalle.");
        }

        lock (_gate)
        {
            var tenant = GetTenant(companyId);

            sale.Id = tenant.SaleSeq++;
            sale.CompanyId = companyId;

            foreach (var line in sale.Lines)
            {
                if (line.UnitPrice == 0)
                {
                    var product = tenant.Products.FirstOrDefault(p => p.Id == line.ProductId) ?? throw new InvalidOperationException("Producto no encontrado");
                    line.UnitPrice = product.UnitPrice;
                }

                var stockProduct = tenant.Products.FirstOrDefault(p => p.Id == line.ProductId);
                if (stockProduct is not null)
                {
                    stockProduct.Stock = Math.Max(0, stockProduct.Stock - line.Quantity);
                }
            }

            tenant.Sales.Add(CopySale(sale));
            return Task.FromResult(CloneSale(tenant, sale));
        }
    }

    #endregion

    public Task<CommerceSnapshot> GetSnapshotAsync(int companyId)
    {
        lock (_gate)
        {
            var tenant = GetTenant(companyId);
            var monthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var monthlySales = tenant.Sales.Where(s => s.Date >= monthStart).Sum(s => s.Total);
            var avgTicket = tenant.Sales.Any() ? Math.Round(tenant.Sales.Average(s => s.Total), 2) : 0m;

            return Task.FromResult(new CommerceSnapshot
            {
                InventoryValue = tenant.Products.Sum(p => p.UnitPrice * p.Stock),
                MonthlySales = monthlySales,
                ActiveVendors = tenant.Vendors.Count,
                CustomerCount = tenant.Customers.Count,
                AverageTicket = avgTicket
            });
        }
    }

    private TenantData GetTenant(int companyId)
    {
        if (!_tenants.TryGetValue(companyId, out var tenant))
        {
            throw new InvalidOperationException("Empresa no encontrada");
        }

        return tenant;
    }

    private static Company CloneCompany(Company company) => new()
    {
        Id = company.Id,
        Name = company.Name
    };

    private static Supplier CloneSupplier(Supplier supplier) => new()
    {
        Id = supplier.Id,
        CompanyId = supplier.CompanyId,
        Name = supplier.Name,
        ContactName = supplier.ContactName,
        Phone = supplier.Phone,
        Email = supplier.Email
    };

    private static Vendor CloneVendor(Vendor vendor) => new()
    {
        Id = vendor.Id,
        CompanyId = vendor.CompanyId,
        Name = vendor.Name,
        CommissionRate = vendor.CommissionRate
    };

    private static Customer CloneCustomer(Customer customer) => new()
    {
        Id = customer.Id,
        CompanyId = customer.CompanyId,
        Name = customer.Name,
        Email = customer.Email,
        Phone = customer.Phone
    };

    private static Product CopyProduct(Product product) => new()
    {
        Id = product.Id,
        CompanyId = product.CompanyId,
        Name = product.Name,
        Sku = product.Sku,
        UnitPrice = product.UnitPrice,
        Stock = product.Stock,
        SupplierId = product.SupplierId
    };

    private Product CloneProduct(TenantData tenant, Product product)
    {
        var clone = CopyProduct(product);
        var supplier = tenant.Suppliers.FirstOrDefault(s => s.Id == product.SupplierId);
        clone.Supplier = supplier is not null ? CloneSupplier(supplier) : null;
        return clone;
    }

    private static Sale CopySale(Sale sale) => new()
    {
        Id = sale.Id,
        CompanyId = sale.CompanyId,
        Date = sale.Date,
        CustomerId = sale.CustomerId,
        VendorId = sale.VendorId,
        Lines = sale.Lines.Select(line => new SaleLine
        {
            ProductId = line.ProductId,
            Quantity = line.Quantity,
            UnitPrice = line.UnitPrice
        }).ToList()
    };

    private Sale CloneSale(TenantData tenant, Sale sale)
    {
        return new Sale
        {
            Id = sale.Id,
            CompanyId = sale.CompanyId,
            Date = sale.Date,
            CustomerId = sale.CustomerId,
            Customer = tenant.Customers.FirstOrDefault(c => c.Id == sale.CustomerId) is { } customer ? CloneCustomer(customer) : null,
            VendorId = sale.VendorId,
            Vendor = tenant.Vendors.FirstOrDefault(v => v.Id == sale.VendorId) is { } vendor ? CloneVendor(vendor) : null,
            Lines = sale.Lines.Select(line =>
            {
                var product = tenant.Products.FirstOrDefault(p => p.Id == line.ProductId);
                return new SaleLine
                {
                    ProductId = line.ProductId,
                    Product = product is not null ? CloneProduct(tenant, product) : null,
                    Quantity = line.Quantity,
                    UnitPrice = line.UnitPrice
                };
            }).ToList()
        };
    }

    private void Seed()
    {
        var company1 = new Company { Id = 1, Name = "Cafeterías Regionales" };
        var company2 = new Company { Id = 2, Name = "Distribuciones Express" };

        _companies.Add(company1);
        _companies.Add(company2);
        _tenants[company1.Id] = new TenantData();
        _tenants[company2.Id] = new TenantData();

        SeedCompanyOne(company1.Id);
        SeedCompanyTwo(company2.Id);
    }

    private void SeedCompanyOne(int companyId)
    {
        var tenant = _tenants[companyId];

        var supplier1 = new Supplier { Name = "Tech Importaciones", ContactName = "Laura Díaz", Email = "contacto@techimport.com", Phone = "555-1000" };
        var supplier2 = new Supplier { Name = "Café del Sur", ContactName = "Ana Morales", Email = "hola@cafedelsur.com", Phone = "555-3000" };
        AddSupplier(tenant, companyId, supplier1);
        AddSupplier(tenant, companyId, supplier2);

        var vendor1 = new Vendor { Name = "María Hernández", CommissionRate = 3 };
        var vendor2 = new Vendor { Name = "Jorge Ramírez", CommissionRate = 4 };
        AddVendor(tenant, companyId, vendor1);
        AddVendor(tenant, companyId, vendor2);

        var customer1 = new Customer { Name = "Café Central", Email = "compras@cafecentral.com", Phone = "555-2211" };
        var customer2 = new Customer { Name = "Hotel Miramar", Email = "proveedores@miramar.com", Phone = "555-4422" };
        AddCustomer(tenant, companyId, customer1);
        AddCustomer(tenant, companyId, customer2);

        var product1 = new Product { Name = "Café en grano 1kg", Sku = "CF-001", UnitPrice = 18.5m, Stock = 50, SupplierId = 1 };
        var product2 = new Product { Name = "Taza térmica", Sku = "AC-010", UnitPrice = 12.0m, Stock = 120, SupplierId = 1 };
        AddProduct(tenant, companyId, product1);
        AddProduct(tenant, companyId, product2);

        var sale = new Sale
        {
            Date = DateTime.Today.AddDays(-3),
            CustomerId = 1,
            VendorId = 1,
            Lines =
            {
                new SaleLine { ProductId = 1, Quantity = 5, UnitPrice = 18.5m },
                new SaleLine { ProductId = 2, Quantity = 10, UnitPrice = 12m }
            }
        };

        AddSale(tenant, companyId, sale);
    }

    private void SeedCompanyTwo(int companyId)
    {
        var tenant = _tenants[companyId];

        var supplier1 = new Supplier { Name = "Distribuciones Norte", ContactName = "Carlos López", Email = "ventas@dnorte.com", Phone = "555-2000" };
        var supplier2 = new Supplier { Name = "Logística Este", ContactName = "Paula Medina", Email = "soporte@logeste.com", Phone = "555-4500" };
        AddSupplier(tenant, companyId, supplier1);
        AddSupplier(tenant, companyId, supplier2);

        var vendor1 = new Vendor { Name = "Lucía Santos", CommissionRate = 5 };
        var vendor2 = new Vendor { Name = "Tomás Vega", CommissionRate = 4 };
        AddVendor(tenant, companyId, vendor1);
        AddVendor(tenant, companyId, vendor2);

        var customer1 = new Customer { Name = "Retail Norte", Email = "retail@norte.com", Phone = "555-7788" };
        var customer2 = new Customer { Name = "Tiendas Express", Email = "contacto@express.com", Phone = "555-6633" };
        AddCustomer(tenant, companyId, customer1);
        AddCustomer(tenant, companyId, customer2);

        var product1 = new Product { Name = "Kit barista", Sku = "KT-100", UnitPrice = 85m, Stock = 20, SupplierId = 1 };
        var product2 = new Product { Name = "Café molido 500g", Sku = "CF-200", UnitPrice = 9.5m, Stock = 90, SupplierId = 2 };
        AddProduct(tenant, companyId, product1);
        AddProduct(tenant, companyId, product2);

        var sale = new Sale
        {
            Date = DateTime.Today.AddDays(-1),
            CustomerId = 2,
            VendorId = 1,
            Lines =
            {
                new SaleLine { ProductId = 2, Quantity = 15, UnitPrice = 9.5m }
            }
        };

        AddSale(tenant, companyId, sale);
    }

    private void AddSupplier(TenantData tenant, int companyId, Supplier supplier)
    {
        supplier.Id = tenant.SupplierSeq++;
        supplier.CompanyId = companyId;
        tenant.Suppliers.Add(CloneSupplier(supplier));
    }

    private void AddVendor(TenantData tenant, int companyId, Vendor vendor)
    {
        vendor.Id = tenant.VendorSeq++;
        vendor.CompanyId = companyId;
        tenant.Vendors.Add(CloneVendor(vendor));
    }

    private void AddCustomer(TenantData tenant, int companyId, Customer customer)
    {
        customer.Id = tenant.CustomerSeq++;
        customer.CompanyId = companyId;
        tenant.Customers.Add(CloneCustomer(customer));
    }

    private void AddProduct(TenantData tenant, int companyId, Product product)
    {
        product.Id = tenant.ProductSeq++;
        product.CompanyId = companyId;
        tenant.Products.Add(CopyProduct(product));
    }

    private void AddSale(TenantData tenant, int companyId, Sale sale)
    {
        sale.Id = tenant.SaleSeq++;
        sale.CompanyId = companyId;
        tenant.Sales.Add(CopySale(sale));
    }

    private class TenantData
    {
        public List<Supplier> Suppliers { get; } = new();
        public List<Vendor> Vendors { get; } = new();
        public List<Customer> Customers { get; } = new();
        public List<Product> Products { get; } = new();
        public List<Sale> Sales { get; } = new();

        public int SupplierSeq { get; set; } = 1;
        public int VendorSeq { get; set; } = 1;
        public int CustomerSeq { get; set; } = 1;
        public int ProductSeq { get; set; } = 1;
        public int SaleSeq { get; set; } = 1;
    }
}

