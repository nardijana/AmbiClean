using MySql.Data.MySqlClient;
using System;
using AmbiClean.Database;
using AmbiClean.Models;
using System.Collections.Generic;

namespace AmbiClean.Repositories
{
    public class CargoRepository
    {
        private readonly Database.Database _db;

        public CargoRepository()
        {
            _db = Database.Database.Instance;
        }

        public bool Inserir(Cargo cargo)
        {
            try
            {
                using var conn = _db.GetConnection();

                var sql = "INSERT INTO Cargo (nome, descricao) VALUES (@nome, @descricao)";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nome", cargo.Nome);
                cmd.Parameters.AddWithValue("@descricao", cargo.Descricao);

               return cmd.ExecuteNonQuery() > 0;

            }
            catch (Exception ca)
            {
                Console.WriteLine($"Erro ao inserir cargo: {ca.Message}");
                return false;
            }
            
        }

        public List<Cargo> BuscarTodos()
        {
            var cargos = new List<Cargo>();

            using var conn = _db.GetConnection();

            var sql = "SELECT id, nome, descricao FROM Cargo";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                cargos.Add(new Cargo
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Descricao = reader.GetString(2)
                });
            }

            return cargos;
        }

        public Cargo BuscarPorId(int id)
        {
            using var conn = _db.GetConnection();

            var sql = "SELECT id, nome, descricao FROM Cargo WHERE id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Cargo
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    Descricao = reader.GetString(2)
                };
            }

            return null;
        }

        public void Atualizar(Cargo cargo)
        {
            using var conn = _db.GetConnection();

            var sql = @"UPDATE Cargo SET 
                        Nome = @nome,
                        Descricao = @descricao";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nome", cargo.Nome);
            cmd.Parameters.AddWithValue("@descricao", cargo.Descricao);

            cmd.ExecuteNonQuery();
        }

        public void Excluir(int id)
        {
            using var conn = _db.GetConnection();

            var sql = "DELETE FROM Cargo WHERE id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
    
