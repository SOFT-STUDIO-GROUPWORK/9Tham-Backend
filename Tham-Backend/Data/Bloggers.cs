﻿using System.ComponentModel.DataAnnotations;
using Tham_Backend.Models;

namespace Tham_Backend.Data;

public class Bloggers
{
    public int Id { get; set; }

    [Required] [StringLength(200)] public virtual string FirstName { get; set; } = string.Empty;

    [Required] [StringLength(200)] public virtual string LastName { get; set; } = string.Empty;

    [Required] [StringLength(150)] public virtual string NickName { get; set; } = string.Empty;

    [Required] [StringLength(256), EmailAddress] public virtual string Email { get; set; } = string.Empty;

    public UserRoles Role { get; set; }

    public bool IsBanned { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
    public string BannerUrl { get; set; } = string.Empty;

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }

    public List<Articles> Articles { get; set; }//for FK on Articles table
    public List<Comments> Comments { get; set; }//for FK on Comments table
    public List<Likes> Likes { get; set; }//for FK on Likes table
}