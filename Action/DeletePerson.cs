using Models;

namespace Actions
{
    public class DeletePerson : IAction<Person>
    {
        public DeletePerson(Person person)
        {
            ActionAgainst = person;
        }

        public Person ActionAgainst { get; set; }
    }
}
