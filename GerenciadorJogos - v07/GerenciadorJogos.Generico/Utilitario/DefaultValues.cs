using System;

namespace GerenciadorJogos.Generico.Utilitario
{
    public static class DefaultValues
    {
        #region "   Retorna Int        "
        public static int DefaultInteger(this object objValor)
        {
            try
            {
                if (objValor == null || objValor is DBNull || string.IsNullOrEmpty(objValor.ToString()))
                {
                    return 0;
                }

                return Convert.ToInt32(objValor);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "   Retorna Decimal    "
        public static decimal DefaultDecimal(this object objValor)
        {
            try
            {
                if (objValor == null || objValor is DBNull || string.IsNullOrEmpty(objValor.ToString()))
                {
                    return Convert.ToDecimal(0.00);
                }

                return Convert.ToDecimal(objValor);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "   Retorna String     "
        public static string DefaultString(this object objValor)
        {
            try
            {
                if (objValor == null || objValor is DBNull || string.IsNullOrEmpty(objValor.ToString()))
                {
                    return "";
                }

                return objValor.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "   Retorna Boolean    "
        public static bool DefaultBoolean(this object objValor)
        {
            try
            {
                if (objValor == null || objValor is DBNull || string.IsNullOrEmpty(objValor.ToString()))
                {
                    return false;
                }

                return Convert.ToBoolean(objValor);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "   Retorna DateTime   "
        public static DateTime DefaultDateTime(this object objValor)
        {
            try
            {
                if (objValor == null || objValor is DBNull || string.IsNullOrEmpty(objValor.ToString()))
                {
                    return Convert.ToDateTime("# 01/01/1900 #");
                }

                return Convert.ToDateTime(objValor);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
