﻿using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Models
{
    public class BaseViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
    }
}