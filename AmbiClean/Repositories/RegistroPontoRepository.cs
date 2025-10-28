using AmbiClean.Models;
using MySql.Data.MySqlClient;

namespace AmbiClean.Repositories
{
    class RegistroPontoRepository
    {
        private readonly Database.Database _db;

        public RegistroPontoRepository()
        {
            _db = Database.Database.Instance;
        }

        public bool Inserir(RegistroPonto registroPonto)
        {
            try
            {
                using var connection = _db.GetConnection();
                string query = @"INSERT INTO Local 
                                 (TipoRegistro, ReferenciaId, RegistradoEm, Dispositivo)
                                 VALUES (@tipoRegistro, @referenciaId, @registradoEm, @dispositivo)";
                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@tipoRegistro", registroPonto.TipoRegistro);
                cmd.Parameters.AddWithValue("@referenciaId", registroPonto.ReferenciaId);
                cmd.Parameters.AddWithValue("@registradoEm", registroPonto.RegistradoEm);
                cmd.Parameters.AddWithValue("@dispositivo", registroPonto.Dispositivo);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir Registro Ponto: {ex.Message}");
                return false;
            }
        }

        public List<RegistroPonto> BuscarTodos()
        {
            var registroPonto = new List<RegistroPonto>();

            using var conn = _db.GetConnection();

            var sql = "SELECT TipoRegistro, ReferenciaId, RegistradoEm, Dispositivo FROM RegistroPonto";

            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                registroPonto.Add(new RegistroPonto
                {
                    TipoRegistro = reader.GetString(0),
                    ReferenciaId = reader.GetInt32(1),
                    RegistradoEm = reader.GetDateTime(2),
                    Dispositivo = reader.GetString(3)
                });
            }
            return registroPonto;
        }

        public RegistroPonto BuscarPorId(int id)
        {
            using var conn = _db.GetConnection();

            var sql = "SELECT TipoRegistro, ReferenciaId, RegistradoEm, Dispositivo FROM RegistroPonto WHERE id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new RegistroPonto
                {
                    TipoRegistro = reader.GetString(0),
                    ReferenciaId = reader.GetInt32(1),
                    RegistradoEm = reader.GetDateTime(2),
                    Dispositivo = reader.GetString(3)
                };
            }

            return null;
        }

        public void Atualizar(RegistroPonto registroPonto)
        {
            using var conn = _db.GetConnection();

            var sql = @"UPDATE Usuario SET 
                        TipoRegistro = @tipoRegistro,
                        ReferenciaId = @referenciaId,
                        RegistradoEm = @registradoEm,
                        Dispositivo = @dispositivo";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@tipoRegistro", registroPonto.TipoRegistro);
            cmd.Parameters.AddWithValue("@referenciaId", registroPonto.ReferenciaId);
            cmd.Parameters.AddWithValue("@registradoEm", registroPonto.RegistradoEm);
            cmd.Parameters.AddWithValue("@dispositivo", registroPonto.Dispositivo);

            cmd.ExecuteNonQuery();
        }

        public void Excluir(int id)
        {
            using var conn = _db.GetConnection();

            var sql = "DELETE FROM Checklist WHERE id = @id";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
