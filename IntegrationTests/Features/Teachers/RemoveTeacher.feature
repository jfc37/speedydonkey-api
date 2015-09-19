Feature: RemoveTeacher
	In order to set an example
	As an admin
	I want fire a teacher

@teacher @delete @golden_path
Scenario: Demote a teacher
	Given an existing user is a teacher
	When teacher is removed
	Then user is removed from the list of teachers
	But user still exists