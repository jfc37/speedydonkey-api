Feature: DeleteUser

@user @delete @golden_path
Scenario: Delete a user
	Given a user exists
	When the user is deleted
	Then the users details can not be retrieved
