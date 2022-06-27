using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNet6.Model
{
    public record UserWeb
    {
        [Required]
        public string Name { get; init; }

        public string Password { get; init; }

        /// <summary>
        /// E-mail. Должен быть уникальным.
        /// </summary>
        public string? Email { get; init; }
    }
}
