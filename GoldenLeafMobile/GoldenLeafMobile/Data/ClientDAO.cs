using GoldenLeafMobile.Models;
using SQLite;
using System.Collections.Generic;

namespace GoldenLeafMobile.Data
{
    class ClientDAO
    {
        private readonly SQLiteConnection _connection;
        private List<Client> _list;
        public List<Client> List
        {
            get { return _connection.Table<Client>().ToList(); }
            private set { _list = value; }
        }

        public ClientDAO(SQLiteConnection connection)
        {
            _connection = connection;
            _connection.CreateTable<Client>();
        }

        public void Save(Client client)
        {
            if (_connection.Find<Client>(client.Id) == null)
            {
                _connection.Insert(client);
            }
            else
            {
                _connection.Update(client);
            }
        }
    }
}
