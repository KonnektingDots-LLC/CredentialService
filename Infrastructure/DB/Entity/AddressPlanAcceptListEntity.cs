using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace cred_system_back_end_app.Infrastructure.DB.Entity
{
    public class AddressPlanAcceptListEntity:RecordHistory
    {
        public int Id { get; set; }

        public string Name { get; set; }        

    }
}
