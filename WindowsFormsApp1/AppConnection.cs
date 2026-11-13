using System.ComponentModel;   //  чтобы узнать DesignTime/RunTime
using System.Configuration;

namespace WindowsFormsApp1
{
    public static class AppConnection
    {
        private static string _connectionString;

        // ► строка «по умолчанию» (любая валидная, но безопасная —
        //   localdb не требует пароля и не ломает прод.)
        private const string _fallback =
    @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=testReg;Integrated Security=True";

        public static string ConnectionString
        {
            get
            {
                // 1) уже задана вручную? — отдаём
                if (!string.IsNullOrWhiteSpace(_connectionString))
                    return _connectionString;

                // 2) пробуем прочитать из app.config
                var cfg = ConfigurationManager.AppSettings["DefaultConnection"];
                if (!string.IsNullOrWhiteSpace(cfg))
                    return cfg;

                // 3) последний шанс: отдаём «заглушку»
                return _fallback;
            }
            set => _connectionString = value;
        }

        /// <summary>Есть ли рабочая строка подключения (не fallback).</summary>
        public static bool IsConnectionStringSet
            => !string.IsNullOrWhiteSpace(_connectionString);
    }
}