using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AgroPi.Dal.Entities
{
    /// <summary>
    /// Felhasználók osztály
    /// </summary>
    public class User : IdentityUser<int>
    {
        // <value>
        /// Metrikus(true) vagy angolszász(false) mértékegységben jelenjenek meg az adatok, alapértelmezetten metrikus(true)
        /// </value>
        /// <example>
        /// true
        /// </example>
        [Required]
        public bool? ImperialOrMetric { get; set; }

    }
}
