using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using AmbiClean.Database;
using AmbiClean.Models;
using System.Data;

namespace AmbiClean.Repositories
{
    public class UsuarioRepository
    {
        private readonly Database.Database _db;

        public UsuarioRepository()
        {
            _db = Database.Database.Instance;
        }

        public bool Inserir(Usuario usuario)
        {
            try
            {
                using var connection = _db.GetConnection();
                string query = @"INSERT INTO Usuario 
                                 (NomeCompleto, Email, Senha, TurnoId, Telefone, Ativo, CargoId)
                                 VALUES (@nome, @email, @senha, @turno, @telefone, @ativo, @cargo)";
                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@nome", usuario.NomeCompleto);
                cmd.Parameters.AddWithValue("@email", usuario.Email);
                cmd.Parameters.AddWithValue("@senha", usuario.Senha);
                cmd.Parameters.AddWithValue("@turno", usuario.TurnoId);
                cmd.Parameters.AddWithValue("@telefone", usuario.Telefone);
                cmd.Parameters.AddWithValue("@ativo", usuario.Ativo);
                cmd.Parameters.AddWithValue("@cargo", usuario.CargoId);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir usuário: {ex.Message}");
                return false;
            }
        }

        public List<Usuario> BuscarTodos()
        {
            var usuarios = new List<Usuario>();

            using var conn = _db.GetConnection();

            var sql = "SELECT Id, NomeCompleto, Email, Senha, TurnoId, Telefone, Ativo, CargoId FROM Usuario";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                usuarios.Add(new Usuario
                {
                    Id = reader.GetInt32(0),
                    NomeCompleto = reader.GetString(1),
                    Email = reader.GetString(2),
                    Senha = reader.GetString(3),
                    TurnoId = reader.GetInt32(4),
                    Telefone = reader.GetString(5),
                    Ativo = reader.GetBoolean(6),
                    CargoId = reader.GetInt32(7)
                });
            }

            return usuarios;
        }

        public Usuario BuscarPorId(int id)
        {
            using var conn = _db.GetConnection();

            var sql = "SELECT Id, NomeCompleto, Email, Senha, TurnoId, Telefone, Ativo, CargoId FROM Usuario WHERE id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Usuario
                {
                    Id = reader.GetInt32(0),
                    NomeCompleto = reader.GetString(1),
                    Email = reader.GetString(2),
                    Senha = reader.GetString(3),
                    TurnoId = reader.GetInt32(4),
                    Telefone = reader.GetString(5),
                    Ativo = reader.GetBoolean(6),
                    CargoId = reader.GetInt32(7)
                };
            }

            return null;
        }

        public void Atualizar(Usuario usuario)
        {
            using var conn = _db.GetConnection();

            var sql = @"UPDATE Usuario SET 
                        NomeCompleto = @nome,
                        Email = @email,
                        Senha = @senha,
                        TurnoId = @turnoId,
                        Telefone = @telefone,
                        Ativo = @ativo,
                        CargoId = @cargoId
                        WHERE id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", usuario.Id);
            cmd.Parameters.AddWithValue("@nome", usuario.NomeCompleto);
            cmd.Parameters.AddWithValue("@email", usuario.Email);
            cmd.Parameters.AddWithValue("@senha", usuario.Senha);
            cmd.Parameters.AddWithValue("@turnoId", usuario.TurnoId);
            cmd.Parameters.AddWithValue("@telefone", usuario.Telefone);
            cmd.Parameters.AddWithValue("@ativo", usuario.Ativo);
            cmd.Parameters.AddWithValue("@cargoId", usuario.CargoId);

            cmd.ExecuteNonQuery();
        }

        public void Excluir(int id)
        {
            using var conn = _db.GetConnection();

            var sql = "DELETE FROM Usuario WHERE id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}

