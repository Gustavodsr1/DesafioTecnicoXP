using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GestaoPortfolio.Domain.Models
{
    public abstract class BaseEntity
    {
        public object GetId()
        {
            PropertyInfo chave = GetType()
                .GetProperties()
                .First(x => x.GetCustomAttributes(true).Any(x => x.GetType() == typeof(KeyAttribute)));

            return chave.GetValue(this, null);
        }
    }
}
