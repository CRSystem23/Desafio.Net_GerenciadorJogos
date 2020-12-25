using System.ComponentModel;

namespace GerenciadorJogos.Generico.Enumerados
{
    public class Enumerado
    {
        public enum Provider
        {
            [Description("Sql Server")]
            MSSQL = 1,
            [Description("MySql")]
            MYSQL = 2,
        }
    }
}
