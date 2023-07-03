using System;

public interface IBaseEditableRepository<T> : IBaseRepository<T>
{
    Task Create(T obj);
    Task Update(T obj);
}


