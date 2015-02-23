using Models;

namespace Actions
{
    public class CreatePerson : IAction<Person>
    {
        public CreatePerson(Person person)
        {
            ActionAgainst = person;
        }

        public Person ActionAgainst { get; set; }
    }
}
