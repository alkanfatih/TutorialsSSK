using BlogProject.Core.DomainModels.BaseModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Core.DomainModels.Models;

    public class AppUser : IdentityUser, IBaseEntity
    {
	private string firstName;
	private string normalizedFirstName;
	private string lastName;
	private string normalizedLastName;

	public string NormalizedLastName
	{
		get { return normalizedLastName; }
		set { normalizedLastName = lastName.ToUpper(); }
	}

	public string LastName
	{
		get { return lastName; }
		set { lastName = value; }
	}

	public string NormalizedFirstName
	{
		get { return normalizedFirstName; }
		set { normalizedFirstName = firstName.ToUpper(); }
	}

	public string FirstName
	{
		get { return firstName; }
		set { firstName = value; }
	}

	[NotMapped]
	public string FullName { get { return firstName + " " + lastName; } }

	//Navigation Prop
	public virtual ICollection<Article> Articles { get; set; }
	public virtual ICollection<Comment> Comments { get; set; }

}
