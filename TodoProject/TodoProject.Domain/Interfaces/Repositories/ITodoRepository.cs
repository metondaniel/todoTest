﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoProject.Domain.Entities;

namespace TodoProject.Domain.Interfaces.Repositories
{
    public interface ITodoRepository
    {
        Task<TodoItem> GetByIdAsync( Guid id );
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task AddAsync( TodoItem item );
        Task UpdateAsync( TodoItem item );
        Task DeleteAsync( TodoItem item );
    }
}
