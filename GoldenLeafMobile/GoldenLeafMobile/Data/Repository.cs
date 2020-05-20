using GoldenLeafMobile.Models;
using SQLite;
using System.Collections.Generic;

namespace GoldenLeafMobile.Data
{
    public class Repository<T> where T : BaseModel, new()
    {
        private readonly SQLiteConnection _connection;
        private List<T> _list;
        public List<T> List
        {
            get { return _connection.Table<T>().ToList(); }
            private set { _list = value; }
        }

        public Repository(SQLiteConnection connection)
        {
            _connection = connection;
            _connection.CreateTable<T>();
        }

        public void Save(T entity)
        {
            if (_connection.Find<T>(entity.Id) == null)
            {
                _connection.Insert(entity);
            }
            else
            {
                _connection.Update(entity);
            }
        }
    }
}
