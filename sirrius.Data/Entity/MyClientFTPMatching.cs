using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Data.Entity
{
    public class MyClientFTPMatching : BaseEntity, IEntity<int>
    {
        //Eşleştirme alanı
        public string MatchingWord { get; set; }

        public byte OperationTypeId { get; set; }
        public byte TransactionTypeId { get; set; }

        //Cari hesap kodu - Açıklama(Desc) İçinde
        public int MyClientAccountId { get; set; }
        public MyClientAccount MyClientAccount { get; set; }
        [NotMapped]
        public string ClientAccountStr { get; set; }
        [NotMapped]
        public string ClientAccountDescStr { get; set; }

        //Muhasebe kodu - Açıklama(Desc) İçinde
        public int MyClientAccountingCodeId { get; set; }
        public MyClientAccountingCode MyClientAccountingCode { get; set; }
        [NotMapped]
        public string ClientAccountCodeStr { get; set; }
        [NotMapped]
        public string ClientAccountCodeDescStr { get; set; }
        //banka işlem kodu
        public int OperationCodeId { get; set; }
        public OperationCode OperationCode { get; set; }

        [NotMapped]
        public string OperationCodeStr { get; set; }
    }
}
