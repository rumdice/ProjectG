using CsvHelper;
using GrpcServer.Common;
using System.Globalization;

namespace GrpcServer.Tables
{
    public class TablePaser : Singleton<TablePaser>  
    {
        public TablePaser()
        {
        }

        public static bool Load()
        {
            // TODO: 임시 테이블 구조를 객체 형태로 파싱
            // 쿼리 수준으로 사용하기 좋은 클래스 제공 필요 (ex LINQ)
            var tablePath = "../Table/test.csv";
            using (var reader = new StreamReader(tablePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<ItemTable>();

            }
            return true;
        }

    }
}
