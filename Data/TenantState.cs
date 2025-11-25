using System;

namespace BlazorVentas.Data;

public class TenantState
{
    private Company? _currentCompany;

    public Company? CurrentCompany => _currentCompany;
    public int? CurrentCompanyId => _currentCompany?.Id;

    public event Action<Company>? OnTenantChanged;

    public void SetCompany(Company company)
    {
        _currentCompany = company;
        OnTenantChanged?.Invoke(company);
    }
}

