﻿using API.Contexts;
using API.Models;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data;

public class GeneralRepository<Entity, Key> : IRepositories<Entity, Key> where Entity : class
{
    private readonly MyContext _context;
    private readonly DbSet<Entity> _entity;

    public GeneralRepository(MyContext context)
    {
        _context = context;
        _entity = context.Set<Entity>();
    }
    public int Delete(Key id)
    {
        var data = _entity.Find(id);
        if (data == null)
        {
            return 0;
        }
        _entity.Remove(data);
        var result = _context.SaveChanges();
        return result;
    }

    public IEnumerable<Entity> Get()
    {
        return _entity.ToList();
    }

    public Entity Get(Key id)
    {
        return _entity.Find(id);
    }

    public int Insert(Entity entity)
    {
        _entity.Add(entity);
        var result = _context.SaveChanges();
        return result;
    }

    public int Update(Entity entity)
    {
        _entity.Entry(entity).State = EntityState.Modified;

        var result = _context.SaveChanges();
        return result;
    }

}
