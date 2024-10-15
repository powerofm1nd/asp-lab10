using System.ComponentModel.DataAnnotations;

public class RegistrationDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        DateTime date = (DateTime)value;

        if (date.Date <= DateTime.Now.Date)
        {
            ErrorMessage = "The date must be later than today.";
            return false;
        }

        if (date.Date.DayOfWeek == DayOfWeek.Saturday || date.Date.DayOfWeek == DayOfWeek.Sunday)
        {
            ErrorMessage = "The date must not fall on a weekend.";
            return false;
        }

        return true;
    }
}

public class CourseDateValidationAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        RegistrationModel registrationModel = (RegistrationModel)value;

        if (registrationModel.Course.Equals( RegistrationModel.Courses.Basics) && registrationModel.DesirableDate.DayOfWeek == DayOfWeek.Monday)
        {
            ErrorMessage = "We can't provide you with the Basics course on Mondays.";
            return false;
        }

        return true;
    }
}
[CourseDateValidation]
public class RegistrationModel
{
    public enum Courses
    {
        JavaScript,
        CSharp,
        Java,
        Python,
        Base,
        Basics
    }
    
    [Required (ErrorMessage = "You should enter first name")]
    [DataType(DataType.Text)]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Line length must be between 3 and 50 characters.")]
    public string FirstName { get; set;}    
    
    [Required (ErrorMessage = "You should enter last name")]
    [DataType(DataType.Text)]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Line length must be between 3 and 50 characters.")]
    public string LastName { get; set;}    
    
    [Required (ErrorMessage = "You should enter email")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set;}
    
    [Required (ErrorMessage = "You should enter the date of the visit")]
    [DataType(DataType.Date)]
    [RegistrationDate]
    public DateTime DesirableDate { get; set;}

    public Courses Course { get; set;}
    
    public RegistrationModel()
    {
        DesirableDate = DateTime.Now;
    }
}