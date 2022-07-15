﻿
using App.Domain.Core.BaseData.Contarcts.Repositories;
using App.Domain.Core.BaseData.Dtos;
using App.Infrastructures.Database.SqlServer.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Database.Repos.Ef.BaseData;
public class BrandQueryRepository : IBrandQueryRepository
{
    private readonly AppDbContext _context;

    public BrandQueryRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<BrandDto>> GetAll()
    {
        return await _context.Brands.AsNoTracking().Select(p => new BrandDto()
        {
            Id = p.Id,
            Name = p.Name,
            DisplayOrder = p.DisplayOrder,
            CreationDate = p.CreationDate,
            IsDeleted = p.IsDeleted,
        }).ToListAsync();
    }

    public BrandDto? Get(int id)
    {
        return _context.Brands.Where(p => p.Id == id).Select(p => new BrandDto()
        {
            Id = p.Id,
            DisplayOrder = p.DisplayOrder,
            CreationDate = p.CreationDate,
            Name = p.Name,
            IsDeleted = p.IsDeleted,
        }).FirstOrDefault();
    }

    public BrandDto? Get(string name)
    {
        return _context.Brands.Where(p => p.Name == name).Select(p => new BrandDto()
        {
            Id = p.Id,
            DisplayOrder = p.DisplayOrder,
            CreationDate = p.CreationDate,
            Name = p.Name,
            IsDeleted = p.IsDeleted,
        }).SingleOrDefault();
    }

    public async Task<List<BrandDto>?> GetBrands(string? name, int? id, CancellationToken cancellationToken)
    {
        var result = await _context.Brands.AsNoTracking()
            .Where(x => name == null || x.Name == name)
            .Where(x => id == null || x.Id == id)
            .Select(p => new BrandDto()
            {
                Id = p.Id,
                DisplayOrder = p.DisplayOrder,
                CreationDate = p.CreationDate,
                Name = p.Name,
                IsDeleted = p.IsDeleted
            }).ToListAsync(cancellationToken);
        return result;
    }
}
