namespace Movies.Common.UnitTests.Exceptions;

public class ValidationExceptionTests
{
    [Test]
    public void DefaultConstructorCreatesAnEmptyErrorDictionary()
    {
        var actual = new ValidationException().Errors;

        actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
    }

    [Test]
    public void SingleValidationFailureCreatesASingleElementErrorDictionary()
    {
        var failures = new List<ValidationFailure> {new("Email", "Is not a valid email address.")};

        var actual = new ValidationException(failures).Errors;
        
        actual.Keys.Should().BeEquivalentTo("Email");
        actual["Email"].Should().BeEquivalentTo("Is not a valid email address.");
    }

    [Test]
    public void MultipleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
    {
        var failures = new List<ValidationFailure>
        {
            new("Email", "Is not a valid email address."),
            
            new("Password", "Must have at least one uppercase ('A'-'Z')."),
            new("Password", "Must have at least one lowercase ('a'-'z')."),
            new("Password", "Must have at least one digit ('0'-'9')."),
            new("Password", "Must have at least one non alphanumeric character."),
            new("Password", "Must be at least 6 characters."),
            new("Password", "Must be a maximum of 16 characters.")
        };

        var actual = new ValidationException(failures).Errors;

        actual.Keys.Should().BeEquivalentTo("Email", "Password");
        
        actual["Email"].Should().BeEquivalentTo("Is not a valid email address.");

        actual["Password"].Should().BeEquivalentTo(
            "Must have at least one uppercase ('A'-'Z').",
            "Must have at least one lowercase ('a'-'z').",
            "Must have at least one digit ('0'-'9').",
            "Must have at least one non alphanumeric character.",
            "Must be at least 6 characters.",
            "Must be a maximum of 16 characters."
        );
    }
}