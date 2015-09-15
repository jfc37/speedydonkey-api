Feature: CreateTeacher
	In order to allow teachers to perform their duties
	As an admin
	I want promote a student to a teacher

@teacher @create @golden_path
Scenario: Make a user a teacher
	Given a user exists
	When user is set up as a teacher
	Then user is added to the list of teachers

@teacher @create @validation_error
Scenario: Try to set up an existing teacher as a teacher
	Given an existing user is a teacher
	When user is attempted to be set up as a teacher
	Then validation errors are returned
	And user is still on the list of teachers

@teacher @create @golden_path
Scenario: Make a seasoned user a teacher
	And a block exists
	Given a user exists
	And the user has a valid clip pass
	And the user enrols in the block
	When user is set up as a teacher
	Then user is added to the list of teachers
	And the user has a pass
	And the user is enroled in the block
	And the user's details can be retrieved