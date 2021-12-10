using CsvHelper;
using System.Globalization;

namespace GrpcServer.Util
{
    // csv temp class
    public class ItemTable
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Type { get; set; }
        public int Count { get; set; }
    }


    public class TablePaser
    {
        public TablePaser()
        {
        }

        public bool Load()
        {
            // TODO: 임시 테이블 구조를 객체 형태로 파싱
            // 쿼리 수준으로 사용하기 좋은 클래스 제공 필요
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
