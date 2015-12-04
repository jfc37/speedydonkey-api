Feature: UpdateUserNames

@user @update @golden_path
Scenario: Update a users first and last name
	Given a user exists
	And the current user needs to change their name
	When the current user changes their name
	Then the request is successful
	And the current users name is changed

@user @update @validation_errors
Scenario: Update a users name with missing first name
	Given a user exists
	And the current user needs to change their name
	But the current user leaves the first name empty
	When the current user changes their name
	Then the request is unsuccessful
	And the current users name is unchanged