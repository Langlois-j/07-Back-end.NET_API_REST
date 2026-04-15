using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace P7CreateRestApi.Tests.Helpers
{
    public class FakeRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly List<T> _store = new();
        private int _nextId = 1;

        public Task<List<T>> FindAll() => Task.FromResult(_store.ToList());

        public Task<T?> FindById(int id) =>
            Task.FromResult(_store.FirstOrDefault(e => e.Id == id));

        public Task<T> Add(T entity)
        {
            entity.Id = _nextId++;
            _store.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<T?> Update(int id, T entity)
        {
            var existing = _store.FirstOrDefault(e => e.Id == id);
            if (existing == null) return Task.FromResult<T?>(null);
            _store.Remove(existing);
            entity.Id = id;
            _store.Add(entity);
            return Task.FromResult<T?>(entity);
        }

        public Task<bool> Delete(int id)
        {
            var existing = _store.FirstOrDefault(e => e.Id == id);
            if (existing == null) return Task.FromResult(false);
            _store.Remove(existing);
            return Task.FromResult(true);
        }
    }
}
