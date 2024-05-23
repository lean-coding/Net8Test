using BlazorTest.Database;
using BlazorTest.Model;
using Dapper;

namespace BlazorTest.Service
{
    public class ShortURL
    {
        /// <summary>
        /// 新增短網址
        /// </summary>
        /// <param name="parameter">參數</param>
        /// <returns></returns>
        public bool Create(ref M_URL mURL)
        {
            try
            {
                var sql = @"
INSERT INTO M_URL 
(
    URL,
    ShortURL
) 
VALUES 
(
    @URL,
    @ShortURL
);
        
SELECT last_insert_rowid(); ";

                using (var conn = SQLiteHelper.dbConnection())
                {
                    mURL.SN = conn.QuerySingle<int>(sql, mURL);
                    if (mURL.ShortURL == null)
                    {
                        mURL.ShortURL = GetShortCode(mURL.SN);
                        Update(mURL);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        /// <summary>
        /// 更新短網址
        /// </summary>
        /// <param name="mURL"></param>
        /// <returns></returns>
        public bool Update(M_URL mURL)
        {
            try
            {
                var sql = @"
UPDATE M_URL
SET URL = @URL,
      ShortURL = @ShortURL
WHERE SN = @SN; ";

                using (var conn = SQLiteHelper.dbConnection())
                {
                    conn.Execute(sql, mURL);
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        public string GetShortCode(int SN)
        {
            return "S" + SN.ToString("D7");
        }

    }
}
