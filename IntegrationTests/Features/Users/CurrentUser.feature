Feature: CurrentUser

@user @currentuser @get
Scenario: Get current user
	When the current user is retrieved
	Then the user's details can be retrieved